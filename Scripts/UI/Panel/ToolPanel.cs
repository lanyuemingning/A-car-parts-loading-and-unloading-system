using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIFrameWork;

/// <summary>
/// ToolPanel面板类
/// </summary>
public class ToolPanel : MonoBehaviour
{

    public Button BtnCreateTool;
    public List<Button> buttons= new List<Button>();

    public GameObject ScrollView;
    public GameObject ToolCreatePanel;
    void Start()
    {
        SetContent();
        SetListencer();

    }

    void Update()
    {

    }
    void SetContent()
    {
        ScrollView = GameObject.Find("Tool");
        BtnCreateTool = transform.Find("BtnCreateTool").GetComponent<Button>();
        // 需要手动赋值，bug待修
        // ToolCreatePanel = GameObject.Find("ToolCreatePanel");
    }

    void SetListencer()
    {
        BtnCreateTool.onClick.AddListener(() =>
        {
            ToolCreatePanel.SetActive(true);
        });
    }
}