using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vibrate_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        vibrate_Class.Vibrate((long)3000);
    }
}
