using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq; // ✅ 정렬용

public class PixelGridManager : MonoBehaviour
{
    public GameObject pixelPrefab;
    public Transform gridParent;
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

        byte[] bytes = tex.EncodeToPNG();

        string folderPath = Path.Combine(Application.persistentDataPath, "Charactors");
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        // ✅ 자동 번호 붙이기
        int index = 1;
        string filePath;
        do
        {
            filePath = Path.Combine(folderPath, $"Image({index}).png");
            index++;
        } while (File.Exists(filePath));

        File.WriteAllBytes(filePath, bytes);
        Debug.Log($"✅ 픽셀 아트 저장 완료: {filePath}");
    }

    // ✅ 특정 번호의 그림 불러오기
    public void LoadImageByIndex(int index)
    {
        string folderPath = Path.Combine(Application.persistentDataPath, "Charactors");

        if (!Directory.Exists(folderPath))
        {
            Debug.LogWarning("⚠️ Charactors 폴더가 없습니다!");
            return;
        }

        // ✅ Charactors 폴더 내 PNG 파일들을 정렬해서 읽기
        string[] files = Directory.GetFiles(folderPath, "Image(*).png")
                                 .OrderBy(f => f)
                                 .ToArray();

        if (files.Length == 0)
        {
            Debug.LogWarning("⚠️ 저장된 이미지가 없습니다!");
            return;
        }

        if (index < 1 || index > files.Length)
        {
            Debug.LogWarning($"⚠️ 잘못된 인덱스입니다. (1~{files.Length} 사이여야 합니다)");
            return;
        }

        string filePath = files[index - 1]; // ✅ index는 1부터
        LoadFromPNG(filePath);
    }

    // ✅ 실제 불러오기 기능
    public void LoadFromPNG(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("⚠️ 파일이 존재하지 않습니다: " + filePath);
            return;
        }

        byte[] bytes = File.ReadAllBytes(filePath);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(bytes);

        int w = Mathf.Min(gridSize, tex.width);
        int h = Mathf.Min(gridSize, tex.height);

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                Color c = tex.GetPixel(x, h - 1 - y);
                pixels[x, y].SetColor(c);
            }
        }

        Debug.Log($"🎨 이미지 불러오기 완료: {Path.GetFileName(filePath)}");
    }
}
