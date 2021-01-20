using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class memoBtn_Controller : MonoBehaviour
{
    [SerializeField] GameObject m_textBox = null;

    GameObject m_memoBox = null;
    GameObject m_canvas;
    InputField m_input;

    List<GameObject> m_memoBoxList  = new List<GameObject>();
    List<Button> m_buttonList              = new List<Button>();
    List<Toggle> m_toggle                  = new List<Toggle>();

    static int i;
    bool m_isDelete = false;

    void Start()
    {
        m_input = m_textBox.GetComponentInChildren<InputField>();

        m_canvas = GameObject.Find("Canvas");
        m_textBox.SetActive(false);

        i = PlayerPrefs.GetInt("MemoSize", 0);

        for (int j = 0; j<i; j++)
        {
            GameObject load = Resources.Load("Prefabs/memoBox") as GameObject;
            m_memoBox = Instantiate(load) as GameObject;

            m_memoBox.transform.SetParent(m_canvas.transform);
            m_memoBox.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 3.15f + (-3.15f * j), 0);
            m_memoBox.transform.localScale = new Vector3(1, 1, 1);
            m_memoBoxList.Add(m_memoBox);

            Text text = m_memoBox.GetComponentInChildren<Text>();
            text.text = PlayerPrefs.GetString("MEMO" + j);

            Toggle toggle = m_memoBox.GetComponentInChildren<Toggle>();
            m_toggle.Add(toggle);
            
            if (PlayerPrefs.GetInt("MemoCheck" + j) == 1)
                toggle.isOn = true;
            else
                toggle.isOn = false;

            Button temp = m_memoBox.GetComponentInChildren<Button>();
            m_buttonList.Add(temp);
            if(!m_isDelete) temp.gameObject.SetActive(false);    
            else temp.gameObject.SetActive(true);
        }

     
        for (int j = 0; j < m_buttonList.Count; j++)
        {
            int index = j;
            m_buttonList[j].onClick.AddListener(delegate { this.memoDelete(index); });
        }
    }

    public void Back()
    {
        checkSave();

        SceneManager.LoadScene("01. home");
    }

    public void Add()
    {
        checkSave();

        m_textBox.SetActive(true);
        m_textBox.transform.SetAsLastSibling();

        for (int i = 0; i < m_buttonList.Count; i++) m_buttonList[i].gameObject.SetActive(false);
        if (m_isDelete) m_isDelete = false;
    }

    public void setText()
    {
        string memo = m_input.text;
        m_input.text = "";

        if(PlayerPrefs.GetInt("MemoSize") < 4)
        {
            PlayerPrefs.SetString("MEMO" + i, memo);
            PlayerPrefs.Save();

            GameObject load = Resources.Load("Prefabs/memoBox") as GameObject;
            m_memoBox = Instantiate(load) as GameObject;

            m_memoBox.transform.SetParent(m_canvas.transform);
            m_memoBox.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 3.15f + (-3.15f * i), 0);
            m_memoBox.transform.localScale = new Vector3(1, 1, 1);
            m_memoBoxList.Add(m_memoBox);

            Text text = m_memoBox.GetComponentInChildren<Text>();
            text.text = PlayerPrefs.GetString("MEMO" + i);

            PlayerPrefs.SetInt("MemoCheck" + i, 0);

            int index = i;
            Button temp = m_memoBox.GetComponentInChildren<Button>();
            m_buttonList.Add(temp);
            temp.gameObject.SetActive(false);
            temp.onClick.AddListener(delegate { this.memoDelete(index); });

            Toggle toggle = m_memoBox.GetComponentInChildren<Toggle>();
            m_toggle.Add(toggle);

            i = PlayerPrefs.GetInt("MemoSize");
            PlayerPrefs.SetInt("MemoSize", ++i);
            m_textBox.transform.SetAsLastSibling();
        }
        
    }

    public void check()
    {
        checkSave();

        if (m_isDelete)
        {
            for (int i = 0; i < m_buttonList.Count; i++)
            {
                m_buttonList[i].gameObject.SetActive(false);

                int check = PlayerPrefs.GetInt("MemoCheck" + i);
                if (check == 1) m_toggle[i].isOn = true;
                else m_toggle[i].isOn = false;
            }
            m_isDelete = false;
        }
    }

    public void checkSave()
    {
        for (int i = 0; i < m_toggle.Count; i++)
        {
            int temp = PlayerPrefs.GetInt("MemoCheck" + i);
            bool check;

            if (temp == 1) check = true;
            else check = false;

            if (check != m_toggle[i].isOn)
            {
                if (check == true)
                    PlayerPrefs.SetInt("MemoCheck" + i, 0);
                else
                    PlayerPrefs.SetInt("MemoCheck" + i, 1);
            }
        }
    }

    public void delete()
    {
        for (int i = 0; i < m_toggle.Count; i++)
        {
            int temp = PlayerPrefs.GetInt("MemoCheck" + i);
            bool check;

            if (temp == 1) check = true;
            else check = false;

            if (check != m_toggle[i].isOn)
            {
                if (check == true)
                    PlayerPrefs.SetInt("MemoCheck" + i, 0);
                else
                    PlayerPrefs.SetInt("MemoCheck" + i, 1);
            }
        }

        if (!m_isDelete)
        {
            for (int i = 0; i < m_buttonList.Count; i++)
            {
                m_buttonList[i].gameObject.SetActive(true);
            }
            m_isDelete = true;
        }
    }

    public void memoDelete(int index)
    {
        for (int j = index + 1; j < m_memoBoxList.Count ; j++)
        {
            string memo = PlayerPrefs.GetString("MEMO" + j);
            Text temp = m_memoBoxList[j - 1].GetComponentInChildren<Text>();
            temp.text = memo;
            PlayerPrefs.SetString("MEMO" + (j - 1), memo);
            
            int check = PlayerPrefs.GetInt("MemoCheck" + j);
            PlayerPrefs.SetInt("MemoCheck" + (j - 1), check);

            if (check == 1)
                m_toggle[j - 1].isOn = true;
            else
                m_toggle[j - 1].isOn = false;
        }

        PlayerPrefs.DeleteKey("MEMO" + (m_memoBoxList.Count - 1));
        Destroy(m_memoBoxList[m_memoBoxList.Count - 1].gameObject);
        m_buttonList.RemoveAt(m_buttonList.Count - 1);
        m_toggle.RemoveAt(m_toggle.Count - 1);
        m_memoBoxList.RemoveAt(m_memoBoxList.Count - 1);
        PlayerPrefs.SetInt("MemoSize", --i);
    }

    public void close()
    {
        m_textBox.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            int i = 0;
            while (PlayerPrefs.HasKey("MEMO" + i))
            {
                PlayerPrefs.DeleteKey("MEMO" + i);
                i++;
            }

                m_toggle.RemoveRange(0, m_toggle.Count);
            m_memoBoxList.RemoveRange(0, m_memoBoxList.Count);
            m_buttonList.RemoveRange(0, m_buttonList.Count);
        }
    }
}
