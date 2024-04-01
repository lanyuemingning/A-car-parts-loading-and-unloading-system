using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DetachManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("事件系统")]
    EvenSystem even;

    [SerializeField]
    [Tooltip("操作流程")]
    List<GameObject> DetachPartList;

    [SerializeField]
    [Tooltip("提示的颜色")]
    public Color redColor = Color.red;

    [SerializeField]
    [Tooltip("警告面板")]
    public GameObject WarringPanel;
    [SerializeField]

    [Tooltip("倒计时时长")]
    public float timeEnd = 0.1f;
    [SerializeField]
    TextManager textManager;

    public Text WarringContent;

    private bool TestModel = false;

    private int indexer = 0;

    public LayerMask targetLayer;

    public string toolName = "徒手";

    int Score = 100;
    int WarringScore;
    int WarringCount = 0;
    int FailScore;
    int FailCount = 0;
    float timeLeft;
    public DetachMessagerPanel messagerPanel;
    private void Start()
    {
        RedBlink();
        messagerPanel = GameObject.Find("DetachMessagerPanel").GetComponent<DetachMessagerPanel>();
        // WarringPanel = GameObject.Find("WarringPanel");

        WarringScore = Score / DetachPartList.Count;
        FailScore = WarringScore * 2;
        WarringCount = 0;
        FailCount = 0;
        // 转成秒
        timeEnd *= 60;
        Debug.Log(timeEnd);
        textManager = GameObject.Find("TextManager").GetComponent<TextManager>();
        timeLeft = timeEnd;
        DetachPartList[indexer].AddComponent<BoxCollider>();
        WarringContent = WarringPanel.transform.GetComponentsInChildren<Transform>()[1].GetComponent<Text>();
        StartCoroutine(TimerOut());
    }
    IEnumerator TimerOut()
    {
        while (timeLeft >= 0)
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
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == DetachPartList[indexer].name)
                {
                   
                    if (toolName == DetachPartList[indexer].GetComponent<DetachBase>().ToolName)
                    {
                        // 拆卸
                        Detach();
                        if (DetachPartList[indexer].GetComponent<DetachBase>().DetachCount == 0)
                        {
                            Destroy(DetachPartList[indexer].GetComponent<BoxCollider>());
                            Destroy(DetachPartList[indexer]);
                            if (indexer + 1 >= DetachPartList.Count)
                            {
                                ALlEnd();
                                // 拆卸结束
                                Destroy(gameObject);
                                
                            }
                            else
                            {
                                indexer++;
                               
                                DetachPartList[indexer].AddComponent<BoxCollider>();
                                RedBlink();

                                messagerPanel.UpdateMessage();
                            }
                        }
                    }
                    else
                    {
                        WarringEven("当前所需工具为：" + DetachPartList[indexer].GetComponent<DetachBase>().ToolName);
                        WarringCount++;
                    }

                }
                else
                {
                    WarringEven("当前操作应该为：" + DetachPartList[indexer].GetComponent<DetachBase>().WarringContent);
                }
            }

        }
    }
    private void RedBlink()
    {
        if (!TestModel)
        {
         
            DetachPartList[indexer].AddComponent<RedBlink>();
        }
    }

    void NoRedBlink()
    {
        if (DetachPartList[indexer].GetComponent<RedBlink>())
        {
            DetachPartList[indexer].GetComponent<RedBlink>().EndRedBlink();
            Destroy(DetachPartList[indexer].GetComponent<RedBlink>());
        }
    }
    void Detach()
    {

        Debug.Log(DetachPartList[indexer].name + DetachPartList[indexer].GetComponent<DetachBase>().DetachCount);

        DetachPartList[indexer].GetComponent<DetachBase>().DetachCount -= 1;
        DetachPartList[indexer].transform.Rotate(
            DetachPartList[indexer].GetComponent<DetachBase>().RotateDircetion,
            DetachPartList[indexer].GetComponent<DetachBase>().RotateAngle);

        DetachPartList[indexer].transform.Translate(
            DetachPartList[indexer].GetComponent<DetachBase>().Distance *
            DetachPartList[indexer].GetComponent<DetachBase>().Dircetion);
    }


    public PartMassage GetMessager()
    {
        float a= (float)indexer / (float)DetachPartList.Count;
        return new PartMassage(
            DetachPartList[indexer].name,
            DetachPartList[indexer].GetComponent<DetachBase>().WarringContent,
            DetachPartList[indexer].GetComponent<DetachBase>().ToolName,
            a
            );
       
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

    public void Skip()
    {
        Destroy(DetachPartList[indexer].GetComponent<BoxCollider>());
        Destroy(DetachPartList[indexer]);

        if (indexer + 1 >= DetachPartList.Count)
        {
            // WarringPanel.gameObject.SetActive(true);
            // WarringContent.text = "您最后的得分："+(Score-(WarringCount*WarringScore)-(FailureCount*FailureScore)).ToString()+"\n您错误的次数:"+(WarringCount+FailureCount).ToString();
            //dasdas
            // 拆卸结束
            ALlEnd();
            Destroy(gameObject);
            

        }
        else
        {
            indexer++;
            FailCount++;
            DetachPartList[indexer].AddComponent<BoxCollider>();
            RedBlink();

            messagerPanel.UpdateMessage();
        }
    }

    public void WarringEven(string content)
    {
        WarringPanel.SetActive(true);
        WarringContent.text = content;
        Invoke("WarringEnd", 4f);
    }
    public void ALlEnd()
    {
        Score -= (FailCount * FailCount + WarringCount * WarringScore);

        if (Score <= 0)
            Score = 0;

        WarringContent.text = "得分：" + Score + "/n即将回到主界面";
        WarringPanel.SetActive(true);
        Invoke("WarringEnd", 4f);
        SceneManager.LoadScene(2, LoadSceneMode.Single);
        textManager.WritingTip("拆卸"+ TestModel, WarringCount.ToString(), FailCount.ToString(), Score.ToString("0.00"), (timeEnd - timeLeft).ToString());
    }
    public void WarringEnd()
    {
        WarringPanel.SetActive(false);
    }public void WarringEnd1()
    {
        WarringPanel.SetActive(false);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

}

public class PartMassage
{
    public string WarringContent;

    public string ToolName;

    public string GmaeObjectName;

    public float Process;

    public PartMassage(string name, string warringContent, string toolName, float process)
    {
        this.GmaeObjectName = name;
        this.WarringContent = warringContent;
        this.ToolName = toolName;
        this.Process = process;
    }
}

