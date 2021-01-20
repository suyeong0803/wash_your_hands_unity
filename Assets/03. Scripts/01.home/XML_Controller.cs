using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using System.Net;
using System.Net.Http;
using System.IO;
using UnityEngine.UI;

public class XML_Controller : MonoBehaviour
{
    [SerializeField]
    GameObject m_prefab = null;
    Sprite[] m_number      = null;

    [SerializeField] Sprite[] m_triSprite   = new Sprite[2];
    [SerializeField] Image m_triImg       = null;

    [SerializeField] Text m_txtCorona    = null;

    List<Image> m_childImg              = new List<Image>();

    IEnumerator Start()
    {
        string str_today = System.DateTime.Now.ToString("yyyyMMdd");
        string str_yesterday = System.DateTime.Now.AddDays(-1).ToString("yyyyMMdd");

        string url = "http://openapi.data.go.kr/openapi/service/rest/Covid19/getCovid19InfStateJson"; // URL
        url += "?ServiceKey=" + "o1GWOygBmpBO6CdMzv9S6KjmIWgIOXdVVAWwJwUoDP5bQywMJLBv77sqIoiJZ4AwV4wkzEmTYcujxG7VZyfdHw%3D%3D"; // Service Key
        url += "&ServiceKey=-";
        url += "&pageNo=1";
        url += "&numOfRows=10";
        url += "&startCreateDt=" + str_yesterday;
        url += "&endCreateDt=" + str_today;

        var www = new WWW(url);
        yield return www;

        if (www.error == null)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(www.text);

            ProcessXml(xmlDoc.SelectNodes("/response/body/items/item"));
        }
        else
        {
            Debug.Log("Error: " + www.error);
        }
    }

    private void ProcessXml(XmlNodeList nodes)
    {
        if (nodes.Count == 0) Debug.Log("null");
        int[] cnt = new int[nodes.Count];

        if (nodes.Count == 1)
        {
            cnt = new int[2];

            cnt[0] = PlayerPrefs.GetInt("Yesterday[0]", 0);
            cnt[1] = PlayerPrefs.GetInt("Yesterday[1]", 0);
        }

        else
        {
            int idx = 0;

            foreach (XmlNode node in nodes)
            {

                XmlNode tempCnt = node.SelectSingleNode("decideCnt");

                if (tempCnt == null) break;
                else
                {
                    cnt[idx] = Convert.ToInt32(tempCnt.InnerText);
                    idx++;
                }
            }

            if (nodes.Count > 2)
            {
                String[] date = new String[nodes.Count];
                String[] update = new String[nodes.Count];
                int _idx = 0;

                foreach (XmlNode node in nodes)
                {
                    XmlNode tempDate = node.SelectSingleNode("createDt");

                    if (tempDate == null) break;
                    else date[_idx] = tempDate.InnerText;
                    _idx++;
                }

                _idx = 0;
                for (int i = 0; i < date.Length - 1; i++)
                {
                    if (date[i].Equals(date[i + 1]))
                    {
                        cnt[_idx] = cnt[i + 1];
                    }
                    else _idx++;
                }
            }
            PlayerPrefs.SetInt("Yesterday[0]", cnt[0]);
            PlayerPrefs.SetInt("Yesterday[1]", cnt[1]);
            PlayerPrefs.Save();

        }

        if (cnt[1] != 0)
        {
            int sub = cnt[0] - cnt[1];
            if (sub <= 0)
            {
                m_number = Resources.LoadAll<Sprite>("파랑 숫자");
                m_triImg.sprite = m_triSprite[1];
            }
            else
            {
                m_number = Resources.LoadAll<Sprite>("빨강 숫자");
                m_triImg.sprite = m_triSprite[0];
            }

            m_txtCorona.text = cnt[0].ToString();

            SetNumStr(sub.ToString());
        }
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
}
