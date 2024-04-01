using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemBase : MonoBehaviour
{
    [SerializeField]
    [Tooltip("题干")]
    public string Title;
    [SerializeField]
    [Tooltip("选项")]
    public List<string> Options;

    [SerializeField]
    [Tooltip("正确选项序号,从0开始")]
    public int RightOption;

}
