using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UIPaint : MonoBehaviour
{
    public RawImage drawArea;        // 그림을 표시할 UI 오브젝트
    public Color drawColor = Color.black;
    public int textureWidth = 32;
    public int textureHeight = 32;

    private Texture2D tex;
    private RectTransform rectTransform;

    void Start()
    {
        // 1. 텍스처 생성
        tex = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
        ClearTexture(Color.white);

        // 2. RawImage에 연결
        drawArea.texture = tex;
        rectTransform = drawArea.rectTransform;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 localPoint;
            // 3. 마우스가 RawImage 위에 있는지 확인
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out localPoint))
            {
                // RectTransform의 좌표를 텍스처 좌표로 변환
                float pivotX = rectTransform.pivot.x;
                float pivotY = rectTransform.pivot.y;

                float x = (localPoint.x + rectTransform.rect.width * pivotX);
                float y = (localPoint.y + rectTransform.rect.height * pivotY);

                int texX = Mathf.RoundToInt((x / rectTransform.rect.width) * textureWidth);
                int texY = Mathf.RoundToInt((y / rectTransform.rect.height) * textureHeight);

                // 4. 픽셀 칠하기
                if (texX >= 0 && texX < textureWidth && texY >= 0 && texY < textureHeight)
                {
                    tex.SetPixel(texX, texY, drawColor);
                    tex.Apply();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveToPNG();
        }
    }

    void ClearTexture(Color color)
    {
        Color[] pixels = new Color[textureWidth * textureHeight];
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = color;

        tex.SetPixels(pixels);
        tex.Apply();
    }

    void SaveToPNG()
    {
        byte[] bytes = tex.EncodeToPNG();
        string path = Path.Combine(Application.persistentDataPath, "drawing_ui.png");
        File.WriteAllBytes(path, bytes);
        Debug.Log($"✅ 그림 저장 완료: {path}");
    }
}
