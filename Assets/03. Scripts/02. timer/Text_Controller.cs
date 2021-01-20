using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Controller : MonoBehaviour
{
    [SerializeField]
    GameObject m_prefab  = null;
    Sprite[] m_number       = null;
    
    public static bool m_isCount = false;

    List<Image> m_childImg      = new List<Image>();

    private void Start()
    {
        m_number = Resources.LoadAll<Sprite>("timer_num");
    }

    public void SetNumStr(string _str)
    {
        while (m_childImg.Count != _str.Length)
        {
            if (m_childImg.Count > _str.Length)
            {
                Destroy(m_childImg[0].gameObject);
                m_childImg.RemoveAt(0);
            }

            else if (m_childImg.Count < _str.Length)
            {
                var newObj = Instantiate(m_prefab, transform);
                m_childImg.Add(newObj.GetComponent<Image>());
            }
        }

        for (int i = 0; i < _str.Length; i++)
        {
            m_childImg[i].sprite = m_number[int.Parse(_str[i].ToString())];
        }

    }

    public void SetNum(int _num)
    {
        SetNumStr(_num.ToString());
    }
    // Update is called once per frame
    void Update()
    {
        SetNum(Count_Controller.getScore);
    }
}
