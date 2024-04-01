using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIFrameWork;
using TMPro;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// DetchModelPanel面板类
/// </summary>
public class DetchModelPanel : MonoBehaviour
{

    public Button BtnBack;

    public Button BtnModel;

    public Button BtnSkip;

    public Button BtnExit;

    public Text title;

    
    [SerializeField]
    [Tooltip("拆卸管理器")]
    private DetachManager manager;

    [SerializeField]
    [Tooltip("信息面板")]
    private GameObject messagePanel;

    void Start()
    {
        if (manager == null)
        {
            manager = GameObject.Find("DetachManager").GetComponent<DetachManager>();
        }
        
        if (messagePanel == null)
        {
            messagePanel = GameObject.Find("DetachMessagerPanel");
        }

        SetContent();

        SetListencer();
       
        messagePanel.GetComponent<DetachMessagerPanel>().UpdateMessage();
    }


    void Update()
    {

    }


    void SetContent()
    {
        BtnBack = transform.Find("BtnBack").GetComponent<Button>();

        BtnExit = transform.Find("BtnExit").GetComponent<Button>();

        BtnModel = transform.Find("BtnModel").GetComponent<Button>();

        BtnSkip = transform.Find("BtnSkip").GetComponent<Button>();

        title = transform.Find("Title").GetComponent<Text>();

    }

    void SetListencer()
    {
        BtnSkip.onClick.AddListener( ()=>
        {
            manager.Skip();
        });

        BtnExit.onClick.AddListener( () =>
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        });

        BtnModel.onClick.AddListener( () =>
        {
            manager.SetModel();
            TitleUpdate();
            MessagerIsable(manager.GetModel(true));
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        });

        BtnBack.onClick.AddListener( () =>
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        });

    }

    void TitleUpdate()
    {
        title.text = "汽车拆卸系统--"+manager.GetModel(false);
    }

    void MessagerIsable(string able )
    {
        if(able =="True")
        {
            messagePanel.SetActive(false);
        }
        else
        {
            messagePanel.SetActive(true);
        }
    }
}
