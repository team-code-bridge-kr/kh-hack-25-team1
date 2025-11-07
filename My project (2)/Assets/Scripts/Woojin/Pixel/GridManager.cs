using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PixelGridManager : MonoBehaviour
{
    public GameObject pixelPrefab; // PixelButton 프리팹
    public Transform gridParent;   // GridLayoutGroup이 붙은 오브젝트
    public Color drawColor = Color.black;
    public int gridSize = 32;
    public int drawpoint;

    private PixelButton[,] pixels;

    void Start()
    {
        pixels = new PixelButton[gridSize, gridSize];
        CreateGrid();
    }

    void CreateGrid()
    {
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                GameObject pixel = Instantiate(pixelPrefab, gridParent);
                PixelButton pb = pixel.GetComponent<PixelButton>();
                pb.Init(this);
                pixels[x, y] = pb;
            }
        }
    }

    // 저장: 픽셀 색을 이미지로 내보내기
    public void SaveToPNG()
{
    Texture2D tex = new Texture2D(gridSize, gridSize, TextureFormat.RGBA32, false);
    for (int y = 0; y < gridSize; y++)
    {
        for (int x = 0; x < gridSize; x++)
        {
            tex.SetPixel(x, gridSize - 1 - y, pixels[x, y].GetColor());
        }
    }
    tex.Apply();

    // ✅ Charactors 폴더 경로 생성 (없으면 자동 생성)
    string folderPath = Path.Combine(Application.persistentDataPath, "Charactors");
    if (!Directory.Exists(folderPath))
        Directory.CreateDirectory(folderPath);

    // ✅ 파일 이름 자동 증가: Image(1), Image(2), ...
    int index = 1;
    string filePath;
    do
    {
        filePath = Path.Combine(folderPath, $"Image({index}).png");
        index++;
    } while (File.Exists(filePath)); // 중복 방지

    // ✅ PNG 저장
    byte[] bytes = tex.EncodeToPNG();
    File.WriteAllBytes(filePath, bytes);

    Debug.Log($"✅ 픽셀 아트 저장 완료: {filePath}");
}

}
