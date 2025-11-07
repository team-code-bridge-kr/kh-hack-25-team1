using UnityEngine;
using System.IO;

public class FolderInitializer : MonoBehaviour
{
    void Start()
    {
        // ✅ persistentDataPath 내부 경로 설정
        string basePath = Application.persistentDataPath;
        string charFolderPath = Path.Combine(basePath, "Charactors");

        // ✅ 폴더 존재 확인 후 생성
        if (!Directory.Exists(charFolderPath))
        {
            Directory.CreateDirectory(charFolderPath);
            Debug.Log($"✅ Charactors 폴더 생성 완료: {charFolderPath}");
        }
        else
        {
            Debug.Log($"ℹ️ Charactors 폴더 이미 존재: {charFolderPath}");
        }

        // ✅ 예시: 파일 하나 저장해보기
        string testFilePath = Path.Combine(charFolderPath, "readme.txt");
        if (!File.Exists(testFilePath))
        {
            File.WriteAllText(testFilePath, "이 폴더는 캐릭터 데이터를 저장하는 곳입니다.");
            Debug.Log($"📄 테스트 파일 생성: {testFilePath}");
        }
    }
}
