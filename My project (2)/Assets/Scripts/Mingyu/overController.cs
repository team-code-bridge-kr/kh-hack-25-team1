using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class overController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GoToMainAfterSeconds(timerController.remainingTime));
    }

    private IEnumerator GoToMainAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("Main"); // 시간 끝나면 Main씬으로
    }
}
