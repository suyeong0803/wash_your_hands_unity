using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background_Controller : MonoBehaviour
{
    Sprite[] m_background = null;
    [SerializeField] Image m_img_background = null;

    int m_index;

    void Start()
    {
        m_index = 0;

        m_background = Resources.LoadAll<Sprite>("img_background");
        m_img_background.sprite = m_background[0];
        InvokeRepeating("ChangeImg", 5.0f, 5.0f);
    }

    void ChangeImg()
    {
        if (m_index < 5) m_index++;
        else m_index = 5;
    }

    void Update()
    {
        m_img_background.sprite = m_background[m_index];
    }
}
