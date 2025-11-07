using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor; // 에디터용
#endif

public class GAMEOvEr : MonoBehaviour
{
    public Button quitButton; // 연결할 버튼

    void Start()
    {
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(OnQuitButtonClicked);
        }
    }

    void OnQuitButtonClicked()
    {
        Debug.Log("게임 종료 버튼 클릭");

#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // 에디터에서 종료
#else
        Application.Quit(); // 빌드된 애플리케이션 종료
#endif
    }
}
