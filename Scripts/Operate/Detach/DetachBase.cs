using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetachBase : MonoBehaviour
{
    [SerializeField]
    [Tooltip("����Ĵ���")]
    public int DetachCount;

    [SerializeField]
    [Tooltip("����ķ�ʽ")]
    public bool IsRotate;

    [SerializeField]
    [Tooltip("ƽ�Ʒ���")]
    public Vector3 Dircetion;

    [SerializeField]
    [Tooltip("��ת����")]
    public Vector3 RotateDircetion;

    [Serialize]
    [Tooltip("��ת�Ƕ�")]
    public float RotateAngle = 15f;

    [Serialize]
    [Tooltip("λ����")]
    public float Distance = 0.5f;

    [SerializeField]
    [Tooltip("��������")]
    public string WarringContent;

    [SerializeField]
    [Tooltip("��Ҫ�Ĺ���")]
    public string ToolName;


}
