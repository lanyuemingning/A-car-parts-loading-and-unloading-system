using System.Collections;
using System.Collections.Generic;
using TMPro;
using UIFrameWork.Extent;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ToolCretePanel : MonoBehaviour
{
    [SerializeField]
    [Tooltip("�����б�1")]
    Dropdown Add1;

    [SerializeField]
    [Tooltip("�����б�2")]
    Dropdown Add2;

    [SerializeField]
    [Tooltip("ͼƬ��")]
    List<Sprite> ToolSprite = new();

    [SerializeField]
    [Tooltip("���ֱ�")]
    List<string> names = new();

    [SerializeField]
    [Tooltip("��������")]
    string endName;

    [SerializeField]
    [Tooltip("�����ɵĹ���")]
    Image endImage;
    
    [SerializeField]
    [Tooltip("���߹���")]
    Button BtnSuccessed;

    [SerializeField]
    [Tooltip("��ж������")]
    DetachManager detachManager;


    [SerializeField]
    [Tooltip("����Ƿ��������б�������ѡ����±�ƴ�ӣ�")]
    List<int> checkList = new();

    Dictionary<int,string> nameTable= new();
    GameObject toolPanel;

    Dictionary<int, Sprite> checkTable = new();
    bool add1 = false;
    bool add2 = false;
    int add1_index = 0;
    int add2_index = 0;
    bool success = false;
    private void Start()
    {
        SetContent();

        SetListencer();
        checkTable.Clear();
        nameTable.Clear();
        for(int i =0; i<checkList.Count;i++)
        {
            checkTable.Add(checkList[i], ToolSprite[i]);
            nameTable.Add(checkList[i], names[i]);
        }
    }
 
    void Update()
    {

    }

    void SetContent()
    {
        
        Add1 = transform.Find("Dropdown_Add1").GetComponent<Dropdown>();
        if(Add1 ==null)
        {
            Debug.Log("aa");
        }
  /*      foreach(var a in transform.Find("Dropdown_Add1").GetComponents<Component>())
        {
            Debug.Log(a.GetType().Name);
        }*/

        Add2 = transform.Find("Dropdown_Add2").GetComponent<Dropdown>();
        
        BtnSuccessed = transform.Find("BtnSuccessed").GetComponent<Button>();

        endImage = transform.Find("End").GetComponent<Image>();

        toolPanel = GameObject.Find("ToolPanel");

        
    }

    void SetListencer()
    {
        Add1.onValueChanged.AddListener((int index) =>
        {
            add1 = true;
            add1_index = index;
            if(add1&&add2)
            {
                BothChanged();
            }
        }) ;

        Add2.onValueChanged .AddListener((int index) =>
        {
            add2= true;
            add2_index = index;
            if (add1 && add2)
            {
                BothChanged();
            }
        });

        BtnSuccessed.onClick.AddListener(() =>
        {
            Debug.Log(success);
            Debug.Log(add1);
            Debug.Log(add2);
            if(success)
            {
                // GameObject.Find("ToolCreatePanel").SetActive(false);
                // �ҵ����������ȥ��������
                // toolPanel.
                if (toolPanel.GetComponent<ToolPanel>().ScrollView.GetComponent<ScrollViewTest>())
                    toolPanel.GetComponent<ToolPanel>().ScrollView.GetComponent<ScrollViewTest>().CreteTool(endImage.sprite,endName);
                else
                    toolPanel.GetComponent<ToolPanel>().ScrollView.GetComponent<ScrollViewTest1>().CreteTool(endImage.sprite, endName);
                gameObject.SetActive(false);
               
            }
        });

    }

    void BothChanged()
    {    
        if (checkTable.ContainsKey(add1_index * 10 + add2_index))
        {
            endImage.sprite = checkTable[add1_index * 10 + add2_index];
            endName= nameTable[add1_index * 10 + add2_index];
            Debug.Log(endName);
            success = true;
        }
        else
        {
            success = false;
        }
    }

}
