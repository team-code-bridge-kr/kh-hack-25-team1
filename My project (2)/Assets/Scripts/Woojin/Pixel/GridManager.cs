using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class PixelGridManager : MonoBehaviour
{
    public GameObject pixelPrefab;
    public Transform gridParent;
    public Color drawColor = Color.black;
    public int gridSize = 32;
    public int drawpoint;

    private PixelButton[,] pixels;
    private int currentImageIndex = -1; // 현재 편집 중인 이미지 번호 (-1 = 새로 그리기)
    private const int MAX_SLOTS = 5;    // ✅ 최대 슬롯 수

    void Start()
    {
        drawpoint = PlayerPrefs.GetInt("drawpoint", 0);
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

    // ✅ 그림 저장
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

        string filePath;

        // ✅ 기존 그림 덮어쓰기
        if (currentImageIndex > 0)
        {
            filePath = Path.Combine(folderPath, $"Image({currentImageIndex}).png");
            File.WriteAllBytes(filePath, bytes);
            Debug.Log($"💾 Image({currentImageIndex}) 덮어쓰기 완료");
            return;
        }

        // ✅ 새 저장 시 - 빈 슬롯 찾기
        int nextSlot = GetNextAvailableSlot(folderPath);

        if (nextSlot == -1)
        {
            Debug.LogWarning("⚠️ 저장 공간이 가득 찼습니다! (최대 5개)");
            return;
        }

        filePath = Path.Combine(folderPath, $"Image({nextSlot}).png");
        File.WriteAllBytes(filePath, bytes);
        currentImageIndex = nextSlot;

        Debug.Log($"🆕 Image({nextSlot}) 저장 완료: {filePath}");
    }

    // ✅ 비어 있는 슬롯 번호 찾기
    int GetNextAvailableSlot(string folderPath)
    {
        for (int i = 1; i <= MAX_SLOTS; i++)
        {
            string path = Path.Combine(folderPath, $"Image({i}).png");
            if (!File.Exists(path))
                return i;
        }
        return -1; // 모두 찼음
    }

    // ✅ 그림 불러오기 (1~5번만 가능)
    public void LoadImageByIndex(int index)
    {
        if (index < 1 || index > MAX_SLOTS)
        {
            Debug.LogWarning($"⚠️ 불러올 수 있는 슬롯은 1~{MAX_SLOTS}번입니다.");
            return;
        }

        string folderPath = Path.Combine(Application.persistentDataPath, "Charactors");
        string filePath = Path.Combine(folderPath, $"Image({index}).png");

        if (!File.Exists(filePath))
        {
            Debug.LogWarning($"⚠️ Image({index}) 파일이 없습니다!");
            return;
        }

        LoadFromPNG(filePath);
        currentImageIndex = index;
        Debug.Log($"🎨 Image({index}) 불러오기 완료");
    }

    // ✅ 실제 PNG 읽기
    public void LoadFromPNG(string filePath)
    {
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
    }

    // ✅ 새 캔버스로 초기화
    public void ClearCanvas()
    {
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                pixels[x, y].SetColor(new Color(0, 0, 0, 0)); // 투명
            }
        }
        currentImageIndex = -1;
        Debug.Log("🧹 새 캔버스로 초기화 완료");
    }

    void OnDestroy(){
        PlayerPrefs.SetInt("drawpoint", drawpoint);
    }
}
