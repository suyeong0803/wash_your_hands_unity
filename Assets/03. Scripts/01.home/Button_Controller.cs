using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Button_Controller : MonoBehaviour
{
    public void OnClick_Start()
    {
        SceneManager.LoadScene("02. timer");
    }

    public void OnClick_Skip()
    {
        SceneManager.LoadScene("01. home");
    }

    public void OnClick_Memo()
    {
        SceneManager.LoadScene("01-1. memo");
    }
}
