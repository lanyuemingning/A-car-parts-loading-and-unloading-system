using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIFrameWork;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UIFrameWork.Extent;

public class PartPanel : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Content")]
    GameObject content;

    public Camera mainCamera; // �����
    public float distanceFromCamera =10.0f; // ���嵽������ľ���

    void Start()
    {
        SetListencer();
    }

    void SetListencer()
    {
        // Debug.Log(content.transform.childCount);
        foreach (Transform t in content.transform)
        {
            Button button = t.GetComponent<Button>();
            GameObject part = GameObject.Find("Part").FindChildGameObject(t.transform.GetComponentInChildren<Text>().text);
            button.onClick.AddListener(() =>
            {
                part.SetActive(true);
                Vector3 screenCenter = mainCamera.WorldToViewportPoint(new Vector3(0.5f, 0.5f, 0.0f));

                // ���������Ŀ��λ�ã�λ�������ǰ��ָ�������λ��
                Vector3 targetPosition = mainCamera.transform.position - mainCamera.transform.forward * distanceFromCamera + mainCamera.transform.up * screenCenter.y - mainCamera.transform.right * screenCenter.x;
                part.transform.position = targetPosition;
            });
        }

    }
    
}
