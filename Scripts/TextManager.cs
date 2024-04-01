using System.Collections;
using System.Collections.Generic;
using UIFrameWork;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class TextManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("数据存放路，从Assets开始")]
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
            Debug.LogWarning("数据存放路径未配置");
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
    /// 将信息写入用户文件
    /// </summary>
    /// <param name="type">实验类型</param>
    /// <param name="warring">错误次数</param>
    /// <param name="skip">跳过次数</param>
    /// <param name="score">得分</param>
    /// <param name="Time">用时</param>
    public void WritingTip(string type, string warring, string skip, string score,string Time)
    {
        Debug.Log("writingTip");
        m_TextBehavior.WriteFile(dataPath, user,$"实验类型：{type},错误次数：{warring},跳过次数：{skip},最后得分：{score},实验用时：{Time},实验日期：{System.DateTime.Now}");
    }
    public void ReadTips()
    {
        Debug.Log("readTip");
        m_TextBehavior.ReadFile(dataPath, user);
    }
    public void test()
    {
        Debug.Log("测试");
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
