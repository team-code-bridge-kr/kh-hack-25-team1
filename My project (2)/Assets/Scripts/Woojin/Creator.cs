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

            // ✅ 필터모드를 Point로 설정 (픽셀 깨끗하게 유지)
            tex.filterMode = FilterMode.Point;
            tex.Apply();

            Sprite sprite = Sprite.Create(
                tex,
                new Rect(0, 0, tex.width, tex.height),
                new Vector2(0.5f, 0.5f),
                32f // ✅ 픽셀 퍼 유닛 (기본 100보다 작게 해서 확대 효과)
            );

            // 오브젝트 생성
            GameObject go = new GameObject(Path.GetFileNameWithoutExtension(file));
            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = sprite;

            // ✅ 오브젝트 크기 2배로 조정
            go.transform.localScale = new Vector3(2f, 2f, 1f);

            // 보기 좋게 정렬
            go.transform.position = new Vector3(offsetX, 0, 0);

            go.AddComponent<characterToMove>();
            go.GetComponent<characterToMove>().minkyu_speed = 1;
            go.GetComponent<characterToMove>().minkyu_swingSpeed = 5;
            go.GetComponent<characterToMove>().minkyu_swingAmount = 8;
            go.GetComponent<characterToMove>().minkyu_minX = -2.15f;
            go.GetComponent<characterToMove>().minkyu_maxX = 2.15f;
            go.GetComponent<characterToMove>().minkyu_minY = -4.2f;
            go.GetComponent<characterToMove>().minkyu_maxY = 4.2f;

            Debug.Log($"✅ Sprite 생성 완료: {file}");
        }
    }
}
