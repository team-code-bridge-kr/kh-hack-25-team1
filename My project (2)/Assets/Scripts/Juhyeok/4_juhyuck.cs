using System.Collections;
using UnityEngine;
using TMPro;

public class FriendAddHandler : MonoBehaviour
{
    [Header("팝업 패널(비활성화로 두세요)")]
    public GameObject popupPanel;       // NotImplementedPopup

    [Header("문구")]
    public TMP_Text messageText;        // TMP를 안 쓰면 Text로 바꿔 연결
    [TextArea] public string defaultMessage = "아직 구현되지 않은 기능입니다";

    [Header("자동 닫힘(초). 0이면 자동 닫힘 없음")]
    public float autoCloseSeconds = 1.5f;

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
}
