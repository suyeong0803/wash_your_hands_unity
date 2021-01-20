using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class memoController : MonoBehaviour
{
    GameObject m_canvas;

    void Start()
    {
        m_canvas = GameObject.Find("Canvas");

        int i = 0;
        while(PlayerPrefs.HasKey("MEMO" + i))
        {
            var load = Resources.Load("Prefabs/memoText") as GameObject;
            var newObj = Instantiate(load) as GameObject;
            newObj.transform.SetParent(m_canvas.transform);
            newObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.3f, -2.5f - (0.8f * i), 0);
            newObj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

            Text text = newObj.GetComponent<Text>();
            text.text = PlayerPrefs.GetString("MEMO" + i);
            
            i++;
        }
    }
}
