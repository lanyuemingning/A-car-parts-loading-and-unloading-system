using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallBase : MonoBehaviour
{
    [SerializeField]
    [Tooltip("匹配的零件")]
    public GameObject RightPart;

    [SerializeField]
    [Tooltip("提醒内容")]
    public string WarringContent;

    [SerializeField]
    [Tooltip("需要的工具")]
    public string ToolName;
}
