using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class overController : MonoBehaviour
{
    public int drawpoint;
    void Start()
    {
        StartCoroutine(GoToMainAfterSeconds(timerController.remainingTime));
    }

    private IEnumerator GoToMainAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        drawpoint = PlayerPrefs.GetInt("drawpoint", 0);
        drawpoint += seconds/10;
        PlayerPrefs.SetInt("drawpoint", drawpoint);
        SceneManager.LoadScene("Main"); // �ð� ������ Main������
    }
}
