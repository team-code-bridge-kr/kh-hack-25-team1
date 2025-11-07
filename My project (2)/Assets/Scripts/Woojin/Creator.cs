using UnityEngine;
using System.IO;

public class CharacterSpriteLoader : MonoBehaviour
{
    void Start()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, "Charactors");
        if (!Directory.Exists(folderPath))
        {
            Debug.LogWarning("⚠️ Charactors 폴더가 없습니다!");
            return;
        }

        string[] files = Directory.GetFiles(folderPath, "*.png");
        float offsetX = 0f;

        foreach (string file in files)
        {
            byte[] pngData = File.ReadAllBytes(file);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(pngData);

            Sprite sprite = Sprite.Create(
                tex,
                new Rect(0, 0, tex.width, tex.height),
                new Vector2(0.5f, 0.5f)
            );

            // 오브젝트 생성
            GameObject go = new GameObject(Path.GetFileNameWithoutExtension(file));
            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = sprite;

            // 화면에 보기 좋게 정렬
            go.transform.position = new Vector3(offsetX, 0, 0);
            offsetX += 1.5f;

            Debug.Log($"✅ Sprite 생성 완료: {file}");
        }
    }
}
