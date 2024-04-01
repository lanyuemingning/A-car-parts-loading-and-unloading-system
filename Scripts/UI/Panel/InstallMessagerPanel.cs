using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIFrameWork;
using TMPro;

/// <summary>
/// DetachMessagerPanel面板类
/// </summary>
public class InstallMessagerPanel : MonoBehaviour
{
    public Text NowTool;

    public Text NeedTool;

    public Text NowPart;

    public Text Process;

    private UIRoot root;

    [SerializeField]
    [Tooltip("拆卸管理器")]
    private InstallManager manager;

    void Start()
    {
        if (manager == null)
        {
            manager = GameObject.Find("InstallManager").GetComponent<InstallManager>();
        }

        root = UIRoot.Instance;

        SetContent();

        SetListencer();

    }


    void Update()
    {

    }


    void SetContent()
    {
        Process = transform.Find("Process").GetComponent<Text>();

        NowTool = transform.Find("NowTool").GetComponent<Text>();

        NeedTool = transform.Find("NeedTool").GetComponent<Text>();

        NowPart = transform.Find("NowPart").GetComponent<Text>();

    }

    void SetListencer()
    {

    }

    public void UpdateMessage()
    {
        PartMassage message = manager.  GetMessager();

        NeedTool.text = "所需的工具:" + message.ToolName;

        Process.text = "以完成进度:" + message.Process.ToString("0.00");

        NowPart.text = "当前操作:" + message.WarringContent;
    }

    public void UpdateMessage(string tool)
    {
        NowTool.text = "正在使用的工具:" + tool;
    }
}
