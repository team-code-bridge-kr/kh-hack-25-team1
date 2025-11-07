using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class sceneToDraw : MonoBehaviour
{
    public void SceneToDraw()
    {
        SceneManager.LoadScene("Draw");
    }
}
