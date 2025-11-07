using UnityEngine;
using UnityEngine.SceneManagement;

public class Woojin_Buttons : MonoBehaviour
{
    public void OnExitButtonClick()
    {
        SceneManager.LoadScene("Main");
    }
}
