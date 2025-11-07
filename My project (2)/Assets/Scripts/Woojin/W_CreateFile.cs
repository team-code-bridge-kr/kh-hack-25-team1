using UnityEngine;
using UnityEngine.UI; // UI 컴포넌트 사용을 위해 추가
using System.IO;

public class FolderInitializer : MonoBehaviour
{
    private const int gridSize = 32;       // 기본 이미지 크기
    private const int maxSlots = 5;        // Image(1)~Image(5)
    private const string folderName = "Charactors";

    void Awake()
    {
        InitializeFolder();
        // ⭐️ 폴더 초기화 후 이미지 로드 및 씬의 UI 컴포넌트에 적용
        LoadAndApplyImagesToSceneUI(); 
    }

    void InitializeFolder()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);

        // ✅ 폴더 없으면 생성
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            Debug.Log($"📁 Charactors 폴더 생성 완료: {folderPath}");
        }

        // ✅ Image(1)~Image(5) 존재 확인 및 생성
        for (int i = 1; i <= maxSlots; i++)
        {
            string filePath = Path.Combine(folderPath, $"Image({i}).png");
            if (!File.Exists(filePath))
            {
                CreateTransparentImage(filePath);
                Debug.Log($"🆕 기본 투명 이미지 생성: Image({i})");
            }
        }
    }

    // ✅ 32x32 투명 PNG 생성 (이 함수는 변경 없음)
    void CreateTransparentImage(string path)
    {
        Texture2D tex = new Texture2D(gridSize, gridSize, TextureFormat.RGBA32, false);
        Color[] pixels = new Color[gridSize * gridSize];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = new Color(0, 0, 0, 0); // 완전 투명 (RGB=0, A=0)
        }
        tex.SetPixels(pixels);
        tex.Apply();
        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes(path, bytes);
    }
    
    // ⭐️ 씬에서 GameObject를 이름으로 찾아 이미지를 적용하는 함수
    void LoadAndApplyImagesToSceneUI()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);

        for (int i = 1; i <= maxSlots; i++)
        {
            string filePath = Path.Combine(folderPath, $"Image({i}).png");
            string gameObjectName = $"Image({i})"; // ⭐️ 씬에서 찾을 GameObject 이름 가정
            
            // 1. 씬에서 해당 이름의 GameObject 찾기
            GameObject slotObject = GameObject.Find(gameObjectName);
            if (slotObject == null)
            {
                // 찾지 못하면 경고 후 다음 루프로 이동
                Debug.LogWarning($"⚠️ 씬에서 '{gameObjectName}' GameObject를 찾을 수 없습니다. 적용 생략.");
                continue; 
            }

            // 2. Image 컴포넌트 가져오기
            Image targetImage = slotObject.GetComponent<Image>();
            if (targetImage == null)
            {
                // Image 컴포넌트가 없으면 경고 후 다음 루프로 이동
                Debug.LogWarning($"⚠️ '{gameObjectName}'에 Image 컴포넌트가 없습니다. 적용 생략.");
                continue;
            }

            if (File.Exists(filePath))
            {
                byte[] fileData = File.ReadAllBytes(filePath);
                Texture2D tex = new Texture2D(2, 2, TextureFormat.RGBA32, false); 
                
                if (tex.LoadImage(fileData)) 
                {
                    // 3. Sprite로 변환
                    Sprite sprite = Sprite.Create(
                        tex,
                        new Rect(0, 0, tex.width, tex.height), 
                        Vector2.one * 0.5f,                    
                        100f,                                  
                        0,                                     
                        SpriteMeshType.FullRect                
                    );

                    // 4. UI Image 컴포넌트에 적용
                    targetImage.sprite = sprite;
                    // ⭐️ 투명도 표현을 위해 Color를 흰색으로 설정
                    targetImage.color = Color.white; 
                    
                    Debug.Log($"🖼️ {gameObjectName}에 Image({i}).png 로드 및 적용 완료.");
                }
                else
                {
                    Debug.LogError($"❌ Texture2D.LoadImage 실패: {filePath}");
                }
            }
            // 파일이 없으면 InitializeFolder()에서 이미 생성했으므로 이 경고는 보통 발생하지 않음
        }
    }
}