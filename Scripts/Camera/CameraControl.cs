using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // 旋转围绕的中心
    public GameObject CenterObject;

    [SerializeField]
    [Tooltip("移动速度")]
    public float MoveSpeed = 10f;

    [SerializeField]
    [Tooltip("旋转速度")]
    public float RotateSpeed = 5f;

    [SerializeField]
    [Tooltip("缩放速度")]
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
        // 鼠标中键控制平移
        if (Input.GetKey(KeyCode.Mouse2))
        {          
            transform.Translate(Vector3.left * (mouseX * MoveSpeed) * Time.deltaTime);
            transform.Translate(Vector3.up * (mouseY * MoveSpeed) * Time.deltaTime);
        }
        // 鼠标右键控制旋转
        if (Input.GetKey(KeyCode.Mouse1))
        {           
            transform.RotateAround(CenterObject.transform.position, Vector3.up, mouseX * RotateSpeed);
            transform.RotateAround(CenterObject.transform.position, transform.right, mouseY * RotateSpeed);
        }
    }

    /// <summary>
    /// 鼠标中间控制缩放
    /// </summary>
    public void CameraZoom() 
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            transform.Translate(Vector3.forward * ScrollSpeed);


        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            transform.Translate(Vector3.forward * -ScrollSpeed);
    }
}
