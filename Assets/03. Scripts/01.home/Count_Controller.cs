using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Count_Controller : MonoBehaviour
{
    [SerializeField] Text m_TextCount   = null;
    [SerializeField] Text m_TextScore   = null;
    [SerializeField] Image m_imgGage = null;
    [SerializeField] GameObject m_imgGageFrt = null;
    [SerializeField] GameObject m_imgGageBack = null;

    public static int getCount;
    public static int getScore;
    public static float getSize;
    public static float getPos;

    static bool m_dayChange = false;

    // Update is called once per frame
    void Update()
    {
        string today = System.DateTime.Now.ToString("MM.dd");

        getCount    = PlayerPrefs.GetInt("Count", 0);
        getScore    = PlayerPrefs.GetInt("Score", 0);
        getSize      = PlayerPrefs.GetFloat("Size", 0);
        getPos       = PlayerPrefs.GetFloat("Pos", -3.5f);

        if (getSize == 0)
        {
            m_imgGageFrt.SetActive(false);
            m_imgGageBack.SetActive(false);
        }

        else
        {
            m_imgGageFrt.SetActive(true);
            m_imgGageBack.SetActive(true);
        }

        string getToday = PlayerPrefs.GetString("Today", "null");

        if (Timer_Controller.m_isCount)
        {
            Timer_Controller.m_isCount = false;
        }

        m_TextCount.text = getCount.ToString();
        m_TextScore.text = getScore.ToString();

        m_imgGage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, getSize);
        m_imgGageBack.GetComponent<RectTransform>().anchoredPosition = new Vector3(getPos, -3.28f, 0);
        
        if (!m_dayChange && !getToday.Equals(today))
        {
            PlayerPrefs.SetInt("Count", 0);
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.SetInt("Size", 0);
            PlayerPrefs.SetFloat("Pos", -3.5f);
            PlayerPrefs.SetString("Today", today);

            PlayerPrefs.Save();
        
            m_dayChange = true;
        }
    }
}
