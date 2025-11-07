using UnityEngine;
using UnityEngine.SceneManagement;

public class timerController : MonoBehaviour
{
    public static float remainingTime; // Over씬에서 사용하기 위해 static으로 저장

    public void StartTimerInSeconds(float seconds)
    {
        remainingTime = seconds; // 시간 저장
        SceneManager.LoadScene("Over"); // 바로 Over씬으로 이동
    }
}
