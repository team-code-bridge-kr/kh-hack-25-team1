using UnityEngine;
using UnityEngine.SceneManagement;

public class timerController : MonoBehaviour
{
    public static int remainingTime; // Over������ ����ϱ� ���� static���� ����

    public void StartTimerInSeconds(int seconds)
    {
        remainingTime = seconds; // �ð� ����
        SceneManager.LoadScene("Over"); // �ٷ� Over������ �̵�
    }
}
