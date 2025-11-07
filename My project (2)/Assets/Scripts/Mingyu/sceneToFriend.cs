using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class sceneToFriend : MonoBehaviour
{
    public void SceneTofriend()
    {
        SceneManager.LoadScene("friend");
    }
}