using System.Collections;
using System.Collections.Generic;
using UIFrameWork;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class TextManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("���ݴ��·����Assets��ʼ")]
    private string dataPath;
    private static TextManager instance;

    public static TextManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<TextManager>();
            return instance;
        }
    }

    TextBehavior m_TextBehavior =new();

    private string user =123.ToString();
    private string password=123.ToString();

    private void Start() 
    {
        DontDestroyOnLoad(gameObject);
        string persistentPath = Application.persistentDataPath;
        Debug.Log("Persistent Data Path: " + persistentPath);
       

        if (dataPath == null)
            Debug.LogWarning("���ݴ��·��δ����");
       // dataPath = "E:/dai/car_install/Assets" + dataPath;
           
           dataPath= persistentPath;
        // test();
    }
    public bool CreateUser(string user, string password)
    {
        if(m_TextBehavior.CreateFile(dataPath,user))
        {
             m_TextBehavior.WriteDict(dataPath,user, "password",password);
            return true;  
        }
        return false;
       
    }

    public bool CheckUser(string user, string password)
    {
        Debug.Log("checkUser");
        if(m_TextBehavior.CheckFileExit(dataPath, user))
        {
            if (m_TextBehavior.CheckDict(dataPath, user,"password", password))
            {
                
                return true;
            }
        }
        return false;
    }

    public bool LoginUser(string user, string password)
    {
        if(CheckUser(user,password))
        {
            SetUser(user, password);
            return true;
        }
        return false;
    }
    private void SetUser(string name, string passwords)
    {
        user = name;
        password = passwords;
    }

    
    /// <summary>
    /// ����Ϣд���û��ļ�
    /// </summary>
    /// <param name="type">ʵ������</param>
    /// <param name="warring">�������</param>
    /// <param name="skip">��������</param>
    /// <param name="score">�÷�</param>
    /// <param name="Time">��ʱ</param>
    public void WritingTip(string type, string warring, string skip, string score,string Time)
    {
        Debug.Log("writingTip");
        m_TextBehavior.WriteFile(dataPath, user,$"ʵ�����ͣ�{type},���������{warring},����������{skip},���÷֣�{score},ʵ����ʱ��{Time},ʵ�����ڣ�{System.DateTime.Now}");
    }
    public void ReadTips()
    {
        Debug.Log("readTip");
        m_TextBehavior.ReadFile(dataPath, user);
    }
    public void test()
    {
        Debug.Log("����");
        string n = "123";
        CreateUser(n, n);
        if (CheckUser(n,n))
        {
            SetUser(n, n);
            Debug.Log("check success");

        }
        WritingTip("a", "b", "c", "d", "e");
        WritingTip("a1", "b2", "c3", "d4", "e5");

        ReadTips();
        Debug.Log("asd");
    }
   
}
