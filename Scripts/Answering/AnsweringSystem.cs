using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnsweringSystem : MonoBehaviour
{
    [SerializeField]
    [Tooltip("��Ŀ�б�")]
    public List<ProblemBase> Problems =new ();

    public int ReturnRight(int i)
    {
        return Problems[i].RightOption;
    }
    public List<string> ReturnOption(int i)
    {
        return Problems[i].Options;
    }
    public string ReturnQuestion(int i)
    {
        return Problems[i].Title;
    }
}
