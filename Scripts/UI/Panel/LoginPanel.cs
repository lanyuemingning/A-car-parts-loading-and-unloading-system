using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    [SerializeField]
    InputField name;
    [SerializeField]
    InputField password;
    [SerializeField]
    TextManager textManager;
    [SerializeField]
    Button Btnlogin;
    [SerializeField]
    Button BtnExit;
    // Start is called before the first frame update
    void Start()
    {
        SetContent();
        SetListencer();
    }

    void SetContent()
    {
        name = transform.Find("name").GetComponent<InputField>();
        password = transform.Find("password").GetComponent<InputField>();
        textManager = GameObject.Find("TextManager").GetComponent<TextManager>();
        Btnlogin = transform.Find("Btnlogin").GetComponent<Button>();
        BtnExit = transform.Find("Btnexit").GetComponent<Button>();
    }
    
    void SetListencer()
    {
        Btnlogin.onClick.AddListener(() =>
        {
            textManager.CreateUser(name.text,password.text);
            if(textManager.LoginUser(name.text, password.text))
            {
                gameObject.SetActive(false);
            }

        });
        BtnExit.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
