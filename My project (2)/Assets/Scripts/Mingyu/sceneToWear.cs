using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class sceneToWear : MonoBehaviour
{
    public void SceneToWear()
    {
        SceneManager.LoadScene("Wear");
    }
}
