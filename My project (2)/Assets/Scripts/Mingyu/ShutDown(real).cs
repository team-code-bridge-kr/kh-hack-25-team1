using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ShutDown : MonoBehaviour
{
    public float maxRunTime = 900f; // 15분 = 900초
    public string sceneAfterRestart = "Over"; // 재실행 시 띄울 씬 이름

    void Start()
    {
        // 앱 시작 시 이전에 저장된 씬이 있으면 바로 이동
        if (PlayerPrefs.HasKey("NextScene"))
        {
            string nextScene = PlayerPrefs.GetString("NextScene");
            PlayerPrefs.DeleteKey("NextScene"); // 한번 읽고 삭제
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            StartCoroutine(ForceQuitAfterTime(maxRunTime));
        }
    }

    IEnumerator ForceQuitAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("15분 경과! 앱 종료");

        // 앱 종료 전에 다음 실행 시 띄울 씬 저장
        PlayerPrefs.SetString("NextScene", sceneAfterRestart);
        PlayerPrefs.Save();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
