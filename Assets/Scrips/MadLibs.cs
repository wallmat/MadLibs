using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class MadLibs : MonoBehaviour
{
    Toggle m_AdjectiveToggle = null;
    Toggle m_SubjectToggle = null;
    Toggle m_StyleToggle = null;

    Text m_ResultText = null;

    int m_AdjectiveIndex = 0;
    int m_SubjectIndex = 0;
    int m_StyleIndex = 0;

    List<string> m_Adjectives;
    List<string> m_Subjects;
    List<string> m_Styles;

    // Use this for initialization
    void Awake ()
    {
        m_AdjectiveToggle = transform.Find("Image/AdjectiveBG/Toggle").GetComponent<Toggle>();
        m_SubjectToggle = transform.Find("Image/SubjectBG/Toggle").GetComponent<Toggle>();
        m_StyleToggle = transform.Find("Image/StyleBG/Toggle").GetComponent<Toggle>();

        m_ResultText = transform.Find("Image/ResultBG/Text").GetComponent<Text>();
    }

    void Start()
    {
        LoadFile(Application.streamingAssetsPath + "/Adjectives.txt", "Adjectives", out m_Adjectives);
        LoadFile(Application.streamingAssetsPath + "/Subjects.txt", "Subjects", out m_Subjects);
        LoadFile(Application.streamingAssetsPath + "/Styles.txt", "Styles", out m_Styles);
    }

    void LoadFile(string _path, string _debugLabel, out List<string> _list)
    {
        string _line;
        _list = new List<string>();
        StreamReader _reader = new StreamReader(_path);
        using (_reader)
        {
            do
            {
                //just read in each line and add that as an element into our list
                _line = _reader.ReadLine();
                if (_line != null && _line != "")
                {
                    _list.Add(_line);
                }
            }
            while (_line != null);
        }

        //Testing to see what we found
        //Debug.Log(_debugLabel + " Found: " + _list.Count);
        // for (int i = 0; i < _list.Count; i++)
        // {
        //     Debug.Log(_list[i]);
        // }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
         #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
         #else
            Application.Quit();
         #endif
        }
    }

    public void OnRunClicked()
    {
        //When clicked just get a random adjective, subject and style
        string _result = "";

        if (m_AdjectiveToggle.isOn)
        {
            m_AdjectiveIndex = Random.Range(0, m_Adjectives.Count);
            _result += m_Adjectives[m_AdjectiveIndex] + " ";
        }

        if (m_SubjectToggle.isOn)
        {
            m_SubjectIndex = Random.Range(0, m_Subjects.Count);
            _result += m_Subjects[m_SubjectIndex] + " ";
        }

        if (m_StyleToggle.isOn)
        {
            m_StyleIndex = Random.Range(0, m_Styles.Count);
            _result += "in the style of " + m_Styles[m_StyleIndex];
        }

        m_ResultText.text = _result.Replace("\n", "");
    }

    public void SearchWebAdjective()
    {
        //perform a web search assuming the user has chrome installed
        string _url = string.Format("http://www.Google.com/search?q=" + m_Adjectives[m_AdjectiveIndex].Replace(' ', '+') + "&tbm=isch");
        System.Diagnostics.Process.Start("chrome.exe", _url);
    }

    public void SearchWebSubject()
    {
        string _url = string.Format("http://www.Google.com/search?q=" + m_Subjects[m_SubjectIndex].Replace(' ', '+') + "&tbm=isch");
        System.Diagnostics.Process.Start("chrome.exe", _url);
    }

    public void SearchWebStyle()
    {
        string _url = string.Format("http://www.Google.com/search?q=" + m_Styles[m_StyleIndex].Replace(' ', '+') + "&tbm=isch");
        System.Diagnostics.Process.Start("chrome.exe", _url);
    }
}


