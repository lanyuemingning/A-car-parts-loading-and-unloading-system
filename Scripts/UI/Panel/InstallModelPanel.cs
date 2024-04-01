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
public class InstallModelPanel : MonoBehaviour
{

    public Button BtnBack;

    public Button BtnModel;

    public Button BtnSkip;

    public Button BtnExit;

    public Text title;


    [SerializeField]
    [Tooltip("拆卸管理器")]
    private InstallManager manager;

    [SerializeField]
    [Tooltip("信息面板")]
    private GameObject messagePanel;

    void Start()
    {
        if (manager == null)
        {
            manager = GameObject.Find("InstallManager").GetComponent<InstallManager>();
        }

        if (messagePanel == null)
        {
            messagePanel = GameObject.Find("InstallMessagerPanel");
        }

        SetContent();

        SetListencer();

        messagePanel.GetComponent<InstallMessagerPanel>().UpdateMessage();
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
        BtnSkip.onClick.AddListener(() =>
        {
            manager.Skip();
        });

        BtnExit.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        });

        BtnModel.onClick.AddListener(() =>
        {
            manager.SetModel();
            TitleUpdate();
            MessagerIsable(manager.GetModel(true));
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        });

        BtnBack.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        });

    }

    void TitleUpdate()
    {
        title.text = "汽车装配系统--" + manager.GetModel(false);
    }

    void MessagerIsable(string able)
    {
        if (able == "True")
        {
            messagePanel.SetActive(false);
        }
        else
        {
            messagePanel.SetActive(true);
        }
    }
}
