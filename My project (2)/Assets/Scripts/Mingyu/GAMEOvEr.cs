using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GAMEOvEr : MonoBehaviour
{
    public void gameover()
    {
        SceneManager.LoadScene("Main");
        Debug.Log("열기 버튼을 눌러 스트레스 지수가 올라갑니다");
    }
}