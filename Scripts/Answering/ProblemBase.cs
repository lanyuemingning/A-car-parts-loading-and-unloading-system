using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemBase : MonoBehaviour
{
    [SerializeField]
    [Tooltip("���")]
    public string Title;
    [SerializeField]
    [Tooltip("ѡ��")]
    public List<string> Options;

    [SerializeField]
    [Tooltip("��ȷѡ�����,��0��ʼ")]
    public int RightOption;

}
