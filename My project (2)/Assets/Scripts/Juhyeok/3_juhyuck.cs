using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFromPathOrUrl : MonoBehaviour
{
    [Header("파일 경로 또는 URL 입력")]
    [Tooltip("예) C:/Users/you/Pictures/a.png  또는  https://example.com/a.png")]
    public string imagePathOrUrl;

    [Header("옵션")]
    public int pixelsPerUnit = 32;
    public bool pointFilter = true;

    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (!sr) sr = gameObject.AddComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (!string.IsNullOrEmpty(imagePathOrUrl))
            StartCoroutine(LoadAndShow(imagePathOrUrl));
    }

    // UI(InputField 등)에서 호출해도 됨
    public void SetPathAndLoad(string pathOrUrl)
    {
        imagePathOrUrl = pathOrUrl;
        StartCoroutine(LoadAndShow(imagePathOrUrl));
    }

    IEnumerator LoadAndShow(string pathOrUrl)
    {
        // URL 스킴 없고, 실제 로컬 파일이면 file:// 붙여주기
        string url = pathOrUrl;
        bool hasScheme = url.StartsWith("http://") || url.StartsWith("https://") || url.StartsWith("file://");
        if (!hasScheme && File.Exists(pathOrUrl))
        {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            url = "file:///" + pathOrUrl.Replace("\\", "/");
#else
            url = "file://" + pathOrUrl;
#endif
        }

        using (var req = UnityWebRequestTexture.GetTexture(url))
        {
            yield return req.SendWebRequest();

#if UNITY_2020_2_OR_NEWER
            if (req.result != UnityWebRequest.Result.Success)
#else
            if (req.isNetworkError || req.isHttpError)
#endif
            { Debug.LogError($"로드 실패: {req.error}\n입력: {pathOrUrl}\n변환된 URL: {url}"); yield break; }

            var tex = DownloadHandlerTexture.GetContent(req);
            if (pointFilter) tex.filterMode = FilterMode.Point;
            tex.wrapMode = TextureWrapMode.Clamp;

            var rect = new Rect(0, 0, tex.width, tex.height);
            var sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f), Mathf.Max(1, pixelsPerUnit));
            sr.sprite = sprite;
        }
    }
}
