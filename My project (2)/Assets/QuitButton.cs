using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Call when you press the end of the game button
    public void ClickQuit()
    {
        // Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Android
        Application.Quit();
#endif
    }
}