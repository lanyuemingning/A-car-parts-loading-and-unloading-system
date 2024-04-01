using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstallManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("操作流程")]
    List<GameObject> InstallPartList;

    [SerializeField]
    [Tooltip("提示的颜色")]
    public Color redColor = Color.red;

    [SerializeField]
    [Tooltip("警告面板")]
    public GameObject WarringPanel;

    [SerializeField]
    [Tooltip("倒计时时长")]
    public float timeEnd = 40;

    [SerializeField]
    public TextManager textManager;
   
    public Text WarringContent;

    private bool TestModel = false;

    private int indexer = 0;

    public string toolName = "徒手";

    int Score = 100;
    int WarringScore;
    int WarringCount = 0;
    int FailScore;
    int FailCount = 0;
    bool isDragging = false;
    Vector3 offset;
    float timeLeft;
    public InstallMessagerPanel messagerPanel;
    private void Start()
    {
        RedBlink();
        messagerPanel = GameObject.Find("InstallMessagerPanel").GetComponent<InstallMessagerPanel>();
        InstallPartList[indexer].AddComponent<BoxCollider>();
        WarringContent = WarringPanel.transform.GetComponentsInChildren<Transform>()[1].GetComponent<Text>();
        WarringScore = Score / InstallPartList.Count();
        FailScore = WarringScore * 2;
        WarringCount= 0;
        FailCount= 0;
        // 转成秒
        timeEnd *= 60;
        textManager = GameObject.Find("TextManager").GetComponent<TextManager>();
        timeLeft =timeEnd;
        StartCoroutine(TimerOut());
    }

    public void Update()
    {

    }
    IEnumerator TimerOut()
    {
        while (timeLeft > 0)
        {
            // 减少计时器的时间
            timeLeft -= Time.deltaTime;
            // 每帧更新UI显示的时间
            yield return null; // 等待下一帧
        }
        // 当计时器时间结束时执行的操作
        TimerFinished();

    }
    void TimerFinished()
    {
        WarringContent.text = "超时，即将重新测试";
        WarringPanel.SetActive(true);
        Invoke("WarringEnd1", 4f);
        
    }
    private void RedBlink()
    {
        if (!TestModel)
        {
            InstallPartList[indexer].SetActive(true);
            InstallPartList[indexer].AddComponent<RedBlink>();
        }
    }
    public void NoRedBlink()
    {
        if (InstallPartList[indexer].GetComponent<RedBlink>())
        {
            InstallPartList[indexer].GetComponent<RedBlink>().EndRedBlink();
            Destroy(InstallPartList[indexer].GetComponent<RedBlink>());
        }
    }
    public void Skip()
    {
        // Destroy(InstallPartList[indexer].GetComponent<BoxCollider>());

        // Destroy(InstallPartList[indexer]);
        NoRedBlink();

        if (indexer + 1 >= InstallPartList.Count)
        { 
            ALlEnd();
            // WarringPanel.gameObject.SetActive(true);
            // WarringContent.text = "您最后的得分："+(Score-(WarringCount*WarringScore)-(FailureCount*FailureScore)).ToString()+"\n您错误的次数:"+(WarringCount+FailureCount).ToString();
            //dasdas
            // 拆卸结束
            Destroy(gameObject);
           
        }
        else
        {
            indexer++;
           
            InstallPartList[indexer].AddComponent<BoxCollider>();
            RedBlink();
            FailCount++;


            messagerPanel.UpdateMessage();
        }
    }

    public void Suitable()
    {
        NoRedBlink();
        if (indexer + 1 >= InstallPartList.Count)
        {
            ALlEnd();
            // 拆卸结束
            Destroy(gameObject);
            
        }
        else
        {
            Debug.Log(indexer.ToString() + InstallPartList.Count.ToString());
            indexer++;
            Debug.LogWarning(indexer + InstallPartList[indexer].name);
            InstallPartList[indexer].AddComponent<BoxCollider>();
            RedBlink();
        }
    }

    public bool CheckRightTool()
    {
        if (InstallPartList[indexer] .GetComponent<InstallBase>().ToolName == toolName)
            return true;
        else
            return false;
    }
    public bool CheckRightName(string name)
    {
        if (InstallPartList[indexer] .GetComponent<InstallBase>().RightPart.name == name)
            return true;
        else
            return false;
    }
    public PartMassage GetMessager()
    {
        float a = (float)indexer / (float)InstallPartList.Count;
        return new PartMassage(
            InstallPartList[indexer].name,
            InstallPartList[indexer].GetComponent<InstallBase>().WarringContent,
            InstallPartList[indexer].GetComponent<InstallBase>().ToolName,
            a
            );
    }

    public void WarringEven(string content)
    {
        WarringCount++;
        WarringPanel.SetActive(true);
        WarringContent.text = content;
        Invoke("WarringEnd", 2f);
    }
    public void ALlEnd()
    {
        Score -= (FailCount * FailCount + WarringCount * WarringScore);
        
        if (Score <= 0)
            Score = 0;

        WarringContent.text = "得分：" + Score + "/n即将回到主界面";
        WarringPanel.SetActive(true);
        Invoke("WarringEnd", 4f);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        textManager.WritingTip("装配"+ TestModel, WarringCount.ToString(), FailCount.ToString(), Score.ToString("0.00"),(timeEnd-timeLeft).ToString());
    }
    public void WarringEnd()
    {

        WarringPanel.SetActive(false);
    }public void WarringEnd1()
    {

        WarringPanel.SetActive(false);
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
    public string GetToolName()
    {
        return InstallPartList[indexer].GetComponent<InstallBase>().ToolName;
    }

    public string GetPartName()
    {
        return InstallPartList[indexer].GetComponent<InstallBase>().RightPart.name;
    }

    public void SetModel()
    {
        if (!TestModel)
        {
            TestModel = true;
            NoRedBlink();
        }
        else
        {
            TestModel = false;
            RedBlink();
        }
    }

    public string GetModel(bool isBool)
    {
        if (isBool)
            return TestModel.ToString();
        else
        {
            if (TestModel)
                return "考试模式";
            else
                return "练习模式";
        }
    }
}
