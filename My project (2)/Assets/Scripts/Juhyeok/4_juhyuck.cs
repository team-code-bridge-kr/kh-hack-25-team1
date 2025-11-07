using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FriendAddHandler : MonoBehaviour
{
    [Header("팝업 패널(비활성화로 두세요)")]
    public GameObject popupPanel;       // NotImplementedPopup

    [Header("문구")]
    public TMP_Text messageText;        // TextMeshPro 안 쓰면 UnityEngine.UI.Text로 바꾸세요
    [TextArea] public string defaultMessage = "아직 구현되지 않은 기능입니다";

    [Header("자동 닫힘(초). 0이면 자동 닫힘 없음")]
    public float autoCloseSeconds = 1.5f;

    [Header("나가기/뒤로가기 설정")]
    [Tooltip("friend가 별도 씬이면 true → mainSceneName으로 이동, 메인 씬 내 패널이면 false → friendWindowRoot를 닫음")]
    public bool friendIsStandaloneScene = true;

    [Tooltip("friend가 별도 씬일 때 돌아갈 씬 이름")]
    public string mainSceneName = "Main";

    [Tooltip("friend가 메인 씬 내 패널일 때 닫을 루트 오브젝트")]
    public GameObject friendWindowRoot;

    // ====== 친구 추가 버튼 ======
    public void OnClickAddFriend()
    {
        if (!popupPanel) { Debug.LogWarning("popupPanel 미할당"); return; }
        if (messageText) messageText.text = defaultMessage;

        popupPanel.SetActive(true);

        if (autoCloseSeconds > 0f)
        {
            StopAllCoroutines();
            StartCoroutine(AutoClose());
        }
    }

    public void ClosePopup()
    {
        StopAllCoroutines();
        if (popupPanel) popupPanel.SetActive(false);
    }

    IEnumerator AutoClose()
    {
        yield return new WaitForSeconds(autoCloseSeconds);
        ClosePopup();
    }

    // ====== 나가기 / 뒤로가기 버튼 ======
    public void OnClickExit()
    {
        if (friendIsStandaloneScene)
        {
            if (string.IsNullOrEmpty(mainSceneName))
            {
                Debug.LogWarning("mainSceneName이 비어 있습니다. Build Settings에 등록된 씬 이름을 넣어주세요.");
                return;
            }
            SceneManager.LoadScene(mainSceneName);
        }
        else
        {
            // 메인 씬 안의 패널이라면 패널만 닫기
            if (friendWindowRoot)
                friendWindowRoot.SetActive(false);
            else
                gameObject.SetActive(false); // 마지막 안전장치
        }
    }

    // ESC/Back 키로도 뒤로가기 동작
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnClickExit();
    }
}
