using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProblemPanel : MonoBehaviour
{
    [SerializeField]
    GameObject AnsweringSystem;

    AnsweringSystem m_AnsweringSystem;

    [SerializeField]
    Button BtnA;

    [SerializeField]
    Button BtnB;

    [SerializeField]
    Button BtnC;

    [SerializeField]
    Button BtnD;

    [SerializeField]
    Button BtnExit;

    [SerializeField]
    Text Successed;

    [SerializeField]
    Text Question;

    List<string> Option =new();
    List<Button> Btns =new();
    int ProblemState=0;
    bool Input = true;
    int end =0;
    private void Start()
    {
        SetContent();

        SetListencer();

        UpdateContent();
    }

    void Update()
    {
        
    }

    void SetContent()
    {
        BtnA = transform.Find("Option").Find("BtnA").GetComponent<Button>();
        BtnB = transform.Find("Option").Find("BtnB").GetComponent<Button>();
        BtnC = transform.Find("Option").Find("BtnC").GetComponent<Button>();
        BtnD = transform.Find("Option").Find("BtnD").GetComponent<Button>();
        BtnExit = transform.Find("BtnExit").GetComponent<Button>();
        Successed = transform.Find("Successed").GetComponent<Text>();
        Question = transform.Find("Question").GetComponent<Text>();
        m_AnsweringSystem = AnsweringSystem.GetComponent<AnsweringSystem>();
        
    }

    void SetListencer()
    {
        if(Input)
        BtnA.onClick.AddListener(() =>
        {
            Right(0);
        });

        if(Input)
        BtnB.onClick.AddListener(() =>
        {
            Right(1);
        });

        if(Input)
        BtnC.onClick.AddListener(() =>
        {
            Right(2);
        });

        if(Input)
        BtnD.onClick.AddListener(() =>
        {
            Right(3);
        });

        BtnExit.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

        Option.Add("A");
        Option.Add("B");
        Option.Add("C");
        Option.Add("D");
        Btns.Add(BtnA);
        Btns.Add(BtnB);
        Btns.Add(BtnC);
        Btns.Add(BtnD);
    }

    void Right(int i)
    {
        if (i == m_AnsweringSystem.ReturnRight(ProblemState))
        {
            Successed.text = "回答正确";
            end++;
        }
        else
        {
            Successed.text = "正确答案为: " + Option[m_AnsweringSystem.ReturnRight(ProblemState)];
        }
        Input= false;
        Invoke("UpdateContent", 2);
        if(end >=4)
            gameObject.SetActive(false);
        
    }

    void UpdateContent()
    {
        ProblemState =Random.Range(0, m_AnsweringSystem.Problems.Count);
        List<string> options = m_AnsweringSystem.ReturnOption(ProblemState);
        Debug.Log(options);
        for(int i=0;i<4;i++)
        {
            Btns[i].GetComponentInChildren<Text>().text = options[i];
        }
        Question.text = m_AnsweringSystem.ReturnQuestion(ProblemState);
        Successed.text = "";
        Input = true;
    }
}
