﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    Application.Quit();
                    return;
                }
            }
        }
    }
}
