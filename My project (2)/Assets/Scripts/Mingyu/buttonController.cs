using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;


public class buttonController : MonoBehaviour
{
    public Button homeButton;
    public CanvasGroup buttonGroup;   // 기존 버튼 그룹
    public CanvasGroup buttonGroup2;  // 추가된 버튼 그룹
    public float fadeDuration = 0.7f;
    private bool isFaded = false;

    void Awake()
    {
        // 초기 설정
        if (buttonGroup != null)
        {
            buttonGroup.alpha = 1f;
            buttonGroup.interactable = true;
            buttonGroup.blocksRaycasts = true;
        }

        if (buttonGroup2 != null)
        {
            buttonGroup2.alpha = 0f;
            buttonGroup2.interactable = false;
            buttonGroup2.blocksRaycasts = false;
        }
    }

    void Start()
    {
        if (homeButton != null)
        {
            homeButton.onClick.AddListener(OnHomeButtonClicked);
        }
    }

    void Update()
    {
        // buttonGroup2가 보이는 상태에서만
        if (isFaded && buttonGroup2.alpha > 0f && Input.GetMouseButtonDown(0))
        {
            // 마우스가 UI(버튼 등) 위에 있으면 무시
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                StartCoroutine(FadeIn());  // buttonGroup2 페이드아웃 + buttonGroup 페이드인
            }
        }
    }

    void OnHomeButtonClicked()
    {
        if (!isFaded)
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        isFaded = true;

        // buttonGroup 페이드아웃 + buttonGroup2 페이드인 동시에 실행
        StartCoroutine(FadeCanvasGroup(buttonGroup, 1f, 0f));
        StartCoroutine(FadeCanvasGroup(buttonGroup2, 0f, 1f));

        yield return new WaitForSeconds(fadeDuration);

        buttonGroup.interactable = false;
        buttonGroup.blocksRaycasts = false;

        buttonGroup2.interactable = true;
        buttonGroup2.blocksRaycasts = true;
    }

    IEnumerator FadeIn()
    {
        // buttonGroup 페이드인 + buttonGroup2 페이드아웃 동시에 실행
        StartCoroutine(FadeCanvasGroup(buttonGroup, 0f, 1f));
        StartCoroutine(FadeCanvasGroup(buttonGroup2, 1f, 0f));

        yield return new WaitForSeconds(fadeDuration);

        buttonGroup.interactable = true;
        buttonGroup.blocksRaycasts = true;

        buttonGroup2.interactable = false;
        buttonGroup2.blocksRaycasts = false;

        isFaded = false;
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float from, float to)
    {
        if (cg == null) yield break;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            yield return null;
        }
        cg.alpha = to;
    }
}
