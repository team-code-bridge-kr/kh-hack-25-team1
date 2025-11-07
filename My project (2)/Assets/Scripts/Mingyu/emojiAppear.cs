using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeadObjectController : MonoBehaviour
{
    public Button myButton;          // 동작할 버튼
    public GameObject objectToShow;  // 머리 위에 나타날 오브젝트
    public Transform characterHead;  // 캐릭터 머리 위치
    public float displayTime = 2f;   // 몇 초 동안 보여줄지
    public Vector3 offset = new Vector3(0, 0.8f, 0); // 머리 위로 살짝 떠 있는 위치

    private bool isFollowing = false; // 머리 따라가는 상태

    void Start()
    {
        if (myButton != null)
            myButton.onClick.AddListener(OnButtonClicked);

        if (objectToShow != null)
            objectToShow.SetActive(false); // 처음엔 숨기기
    }

    void Update()
    {
        // 오브젝트가 머리를 따라가도록
        if (isFollowing && objectToShow != null && characterHead != null)
        {
            objectToShow.transform.position = characterHead.position + offset;
        }
    }

    void OnButtonClicked()
    {
        if (objectToShow != null && characterHead != null)
        {
            objectToShow.SetActive(true);
            isFollowing = true; // 머리 따라가기 시작
            StartCoroutine(HideAfterTime(displayTime));
        }
    }

    IEnumerator HideAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        objectToShow.SetActive(false);
        isFollowing = false; // 따라가기 종료
    }
}
