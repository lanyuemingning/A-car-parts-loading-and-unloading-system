using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class ScrollViewTest : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Ҫ��ӵ�Ԥ����")]
    GameObject IconPrefab;

    private RectTransform IconOriginTransform;

    [SerializeField]
    [Tooltip("Contentλ��")]
    private GameObject Content;

    [SerializeField]
    [Tooltip("Contentλ��")]
    private DetachManager detachManager;
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
        
        detachManager = GameObject.Find("DetachManager").GetComponent<DetachManager>();                    
        // ��������Ҫ��װ�Ĺ���
        a.onClick.AddListener(() =>{
            detachManager.toolName = "ͽ��";
            detachManager.messagerPanel.UpdateMessage("ͽ��");
            ChangeCursor("ͽ��");
        });
        b.onClick.AddListener(() => {
            detachManager.toolName = "����ǯ";
            detachManager.messagerPanel.UpdateMessage("����ǯ");
            ChangeCursor("����ǯ");
        });
        c.onClick.AddListener(() => {
            detachManager.toolName = "ר�����ŵ���ѹ��";
            detachManager.messagerPanel.UpdateMessage("ר�����ŵ���ѹ��");
            ChangeCursor("ר�����ŵ���ѹ��");
        });
        d.onClick.AddListener(() => {
            detachManager.toolName = "ר��殺�";
            detachManager.messagerPanel.UpdateMessage("ר��殺�");
            ChangeCursor("ר��殺�");
        });
        e.onClick.AddListener(() => {
            detachManager.toolName = "����ǯ";
            detachManager.messagerPanel.UpdateMessage("����ǯ");
            ChangeCursor("����ǯ");
        });
    }

    void Update()
    {
        //��A�����������
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
        spwanedPrefab.transform.Find("Text (TMP)").GetComponent<Text>().text=name;
        spwanedPrefab.GetComponent<Button>().onClick.AddListener(() =>
        {
            // �������ڵĹ�����
            detachManager.toolName = name;
            detachManager.messagerPanel.UpdateMessage(name);
            ChangeCursor(name);
        });
        LayoutRebuilder.ForceRebuildLayoutImmediate(Content.GetComponent<RectTransform>());

    }

    public void ChangeCursor(string name)
    {
        cursor.setCursorTexture(name);
    }
}

