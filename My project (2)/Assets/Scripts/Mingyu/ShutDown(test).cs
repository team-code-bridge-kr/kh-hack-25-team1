using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ForcedShutdownPaneltest : MonoBehaviour
{
    public float maxRunTime = 20f; // 15분 = 900초
    public GameObject shutdownPanel; // 종료 버튼이 있는 패널
    public Button shutdownButton;    // 패널 안 버튼

    void Start()
    {
        // 패널은 처음에는 숨김
        if (shutdownPanel != null)
            shutdownPanel.SetActive(false);

        // 버튼 클릭 시 종료 연결
        if (shutdownButton != null)
            shutdownButton.onClick.AddListener(OnShutdownButtonClicked);

        // 15분 후 패널 활성화
        StartCoroutine(ShowShutdownPanelAfterTime(maxRunTime));
    }

    IEnumerator ShowShutdownPanelAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        if (shutdownPanel != null)
            shutdownPanel.SetActive(true); // 패널 표시
    }

    void OnShutdownButtonClicked()
    {
        Debug.Log("강제 종료 버튼 클릭");

#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // 에디터 종료
#else
        Application.Quit(); // 빌드 종료
#endif
    }
}
