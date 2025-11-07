using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Inspector에서 바꿀 수 있는 씬 이름
    public string sceneName;

    // 버튼에서 호출
    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("SceneName이 비어있습니다!");
        }
    }
}