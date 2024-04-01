using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetachBase : MonoBehaviour
{
    [SerializeField]
    [Tooltip("零件的次数")]
    public int DetachCount;

    [SerializeField]
    [Tooltip("拆除的方式")]
    public bool IsRotate;

    [SerializeField]
    [Tooltip("平移方向")]
    public Vector3 Dircetion;

    [SerializeField]
    [Tooltip("旋转方向")]
    public Vector3 RotateDircetion;

    [Serialize]
    [Tooltip("旋转角度")]
    public float RotateAngle = 15f;

    [Serialize]
    [Tooltip("位移量")]
    public float Distance = 0.5f;

    [SerializeField]
    [Tooltip("提醒内容")]
    public string WarringContent;

    [SerializeField]
    [Tooltip("需要的工具")]
    public string ToolName;


}
