using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class ScrollViewTest1 : MonoBehaviour
{
    [SerializeField]
    [Tooltip("要添加的预制体")]
    GameObject IconPrefab;

    private RectTransform IconOriginTransform;

    [SerializeField]
    [Tooltip("Content位置")]
    private GameObject Content;

    [SerializeField]
    [Tooltip("Content位置")]
    private InstallManager installManager;

    [SerializeField]
    private Cursora cursor;
    [SerializeField]
    Button a;
    [SerializeField]
    Button b;
    [SerializeField]
    Button c;
    [SerializeField]
    Button d; 
    [SerializeField]
    Button e;


    private int ToolCount = 1;

    private void Start()
    {
        IconOriginTransform = Content.transform.GetChild(0).GetComponent<RectTransform>();

        installManager = GameObject.Find("InstallManager").GetComponent<InstallManager>();                    
        // 几个不需要组装的工具
        a.onClick.AddListener(() =>{
            installManager.toolName = "徒手";
            installManager.messagerPanel.UpdateMessage("徒手");
            ChangeCursor("徒手");
        });
        b.onClick.AddListener(() => {
            installManager.toolName = "尖嘴钳";
            installManager.messagerPanel.UpdateMessage("尖嘴钳");
            ChangeCursor("尖嘴钳");
        });
        c.onClick.AddListener(() => {
            installManager.toolName = "专用气门弹簧压具";
            installManager.messagerPanel.UpdateMessage("专用气门弹簧压具");
            ChangeCursor("专用气门弹簧压具");
        });
        d.onClick.AddListener(() => {
            installManager.toolName = "专用铳棒";
            installManager.messagerPanel.UpdateMessage("专用铳棒");
            ChangeCursor("专用铳棒");
        });
        e.onClick.AddListener(() => {
            installManager.toolName = "孔用钳";
            installManager.messagerPanel.UpdateMessage("孔用钳");
            ChangeCursor("孔用钳");
        });
    }

    void Update()
    {
        //按A键添加子物体
        if (Input.GetKeyDown(KeyCode.A))
        {
            
            // Debug.Log(Content.transform.GetChild(ToolCount).name);
            GameObject spwanedPrefab = Instantiate(IconPrefab, IconOriginTransform);
            spwanedPrefab.transform.SetParent(Content.transform, false);
            spwanedPrefab.transform.Translate(Vector3.down*ToolCount*800); 
            ToolCount++;
            LayoutRebuilder.ForceRebuildLayoutImmediate(Content.GetComponent<RectTransform>());
            
        }
    }

    public void CreteTool(Sprite sprite,string name)
    {
        GameObject spwanedPrefab = Instantiate(IconPrefab, IconOriginTransform);
        spwanedPrefab.transform.SetParent(Content.transform, false);
        spwanedPrefab.transform.Translate(Vector3.down * ToolCount * 1400);
        ToolCount++;
        spwanedPrefab.name = name;
        spwanedPrefab.transform.GetComponent<Image>().sprite = sprite;
        spwanedPrefab.transform.Find("Text (Legacy)").GetComponent<Text>().text=name;
        spwanedPrefab.GetComponent<Button>().onClick.AddListener(() =>
        {
            // 设置现在的工具名
            installManager.toolName = name;
            installManager.messagerPanel.UpdateMessage(name);
            ChangeCursor(name);
        });
        LayoutRebuilder.ForceRebuildLayoutImmediate(Content.GetComponent<RectTransform>());

    }
    public void ChangeCursor(string name)
    {
        cursor.setCursorTexture(name);
    }
}

