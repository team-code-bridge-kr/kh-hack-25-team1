using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ShutDown : MonoBehaviour
{
    public float maxRunTime = 10f; // 15분 = 900초
    public string gameOverSceneName = "Over"; // 이동할 씬 이름

    void Start()
    {
        StartCoroutine(GoToGameOverAfterTime(maxRunTime));
    }

    IEnumerator GoToGameOverAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        if (!string.IsNullOrEmpty(gameOverSceneName))
        {
            SceneManager.LoadScene(gameOverSceneName);
        }
        else
        {
            Debug.LogWarning("GameOver 씬 이름이 비어 있습니다!");
        }
    }
}
