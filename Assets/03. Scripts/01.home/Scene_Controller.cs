using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void isSceneChanged(string str)
    {
        if(str.Equals("true"))
        {
            SceneManager.LoadScene("02. timer");
        }
    }
}
