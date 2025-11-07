using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class emojiAppear : MonoBehaviour
{
    public Button myButton;          // 눌렀을 때 동작할 버튼
    public GameObject objectToShow;  // 머리 위에 나타낼 오브젝트
    public Transform characterHead;  // 캐릭터 머리 위치
    public float displayTime = 2f;   // 몇 초 동안 보여줄지

    void Start()
    {
        if (myButton != null)
            myButton.onClick.AddListener(OnButtonClicked);

        if (objectToShow != null)
            objectToShow.SetActive(false);  // 처음엔 숨기기
    }

    void OnButtonClicked()
    {
        if (objectToShow != null && characterHead != null)
        {
            // 머리 위치에 오브젝트 위치 맞추기
            objectToShow.transform.position = characterHead.position;
            objectToShow.SetActive(true);

            // 일정 시간 후 사라지게 코루틴 실행
            StartCoroutine(HideAfterTime(displayTime));
        }
    }

    IEnumerator HideAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        objectToShow.SetActive(false);
    }
}
