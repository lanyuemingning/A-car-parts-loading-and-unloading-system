using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIFrameWork;
using UIFrameWork.Extent;
using UnityEditor;
using UnityEngine.SceneManagement;

/// <summary>
/// StartPanel面板类
/// </summary>
public class StartPanel : MonoBehaviour
{
    Button BtnExit;
    Button BtnInstall;
    Button BtnDetach;
    Button BtnCheckData;
    private void Start()
    {
        SetContent();

        SetListencer();
    }

    void SetContent()
    {
        BtnExit = transform.Find("BtnExit").GetComponent<Button>();
        BtnInstall = transform.Find("BtnInstall").GetComponent<Button>();
        BtnDetach = transform.Find("BtnDetach").GetComponent<Button>();
        BtnCheckData = transform.Find("BtnCheckData").GetComponent<Button>();
    }

    void SetListencer()
    {
        BtnDetach.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        });
        BtnInstall.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        });
        BtnCheckData.onClick.AddListener(() =>
        {
            
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

}

 

