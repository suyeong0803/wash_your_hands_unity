using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer_Controller : MonoBehaviour
{
    [SerializeField]
    GameObject m_prefab = null;
    Sprite[] m_number      = null;
    
    private float m_time            = 30;
    public static bool m_isCount = false;

    List<Image> m_childImg      = new List<Image>();

    private void Start()
    {
        m_number = Resources.LoadAll<Sprite>("timer_num");
    }

    public void SetNumStr(string _str)
    {
        if (_str.Length == 1)
        {
            string temp = '0' + _str;
            _str = temp;
        }

        while (m_childImg.Count != _str.Length)
        {
            if(m_childImg.Count > _str.Length)
            {
                Destroy(m_childImg[0].gameObject);
                m_childImg.RemoveAt(0);
            }

            else if(m_childImg.Count < _str.Length)
            {
                var newObj = Instantiate(m_prefab, transform);
                m_childImg.Add(newObj.GetComponent<Image>());
            }
        }

        for(int i = 0; i<_str.Length; i++)
        {
            m_childImg[i].sprite = m_number[int.Parse(_str[i].ToString())];
        }

    }

    public void SetNum(int _num)
    {
        SetNumStr(_num.ToString());
    }

    void Update()
    {
        SetNum((int)Mathf.Ceil(m_time));

        if (m_time > 0)
            m_time -= Time.deltaTime;
        
        else
        {
            if(m_isCount == false)
            {
                Invoke("ChangeScene", 1.0f);
                m_isCount = true;
            }
        }
    }

    public void ChangeScene()
    {
        if(Count_Controller.getCount < 20 && Count_Controller.getScore < 100)
        {
            Count_Controller.getCount++;
            Count_Controller.getScore += 5;
            Count_Controller.getSize += 0.35f;
            Count_Controller.getPos += 0.35f;
        }

        PlayerPrefs.SetInt("Count", Count_Controller.getCount);
        PlayerPrefs.SetInt("Score", Count_Controller.getScore);
        PlayerPrefs.SetFloat("Size", Count_Controller.getSize);
        PlayerPrefs.SetFloat("Pos", Count_Controller.getPos);
        PlayerPrefs.Save();
        
        SceneManager.LoadScene("03. finish");
    }
}
