using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // ��תΧ�Ƶ�����
    public GameObject CenterObject;

    [SerializeField]
    [Tooltip("�ƶ��ٶ�")]
    public float MoveSpeed = 10f;

    [SerializeField]
    [Tooltip("��ת�ٶ�")]
    public float RotateSpeed = 5f;

    [SerializeField]
    [Tooltip("�����ٶ�")]
    public float ScrollSpeed = 0.2f;

    void Start()
    {

    }

    void Update()
    {
        CameraRotate();
        CameraZoom();
    }
    public void CameraRotate() 
    {
        
        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = -Input.GetAxis("Mouse Y");
        // ����м�����ƽ��
        if (Input.GetKey(KeyCode.Mouse2))
        {          
            transform.Translate(Vector3.left * (mouseX * MoveSpeed) * Time.deltaTime);
            transform.Translate(Vector3.up * (mouseY * MoveSpeed) * Time.deltaTime);
        }
        // ����Ҽ�������ת
        if (Input.GetKey(KeyCode.Mouse1))
        {           
            transform.RotateAround(CenterObject.transform.position, Vector3.up, mouseX * RotateSpeed);
            transform.RotateAround(CenterObject.transform.position, transform.right, mouseY * RotateSpeed);
        }
    }

    /// <summary>
    /// ����м��������
    /// </summary>
    public void CameraZoom() 
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            transform.Translate(Vector3.forward * ScrollSpeed);


        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            transform.Translate(Vector3.forward * -ScrollSpeed);
    }
}
