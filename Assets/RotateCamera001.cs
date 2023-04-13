using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera001 : MonoBehaviour
{
    //本例是第一人称摄像机
    public float moveSpeed = 5f;
    public float xSpeed = 2f; //左右旋转摄像机的速度
    public float ySpeed = 2f; //上下旋转摄像机的速度
    public float yMinLimit = -90f; //上下旋转摄像机的最小角度限制
    public float yMaxLimit = 90f;  //上下旋转摄像机的最大角度限制


    //是否旋转镜头 鼠标右键按下时启用
    private bool isRotate;
    //左右旋转的角度 相对世界原始位置 非transform初始位置(沿三维Y轴)
    private float x;
    //上下旋转的角度 相对世界原始位置 非transform初始位置(沿三维X轴)
    private float y;



    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        //鼠标的左右移动 在三维中相当于沿Y轴转动
        x = angles.y;
        //鼠标的上下移动 在三维中相当于沿X轴转动
        y = angles.x;
    }


    void LateUpdate()
    {
        //在LateUpdate中更新摄像机 可以更平滑 在Update中会有抖动
        RotateCamera();
    }


    void RotateCamera()
    {
        isRotate = true;
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
        if (isRotate)
        {
            //x 和 y 是绝对值 相对于世界坐标没有转动的位置的最终转动位置

            //鼠标的上下移动 移动的Y值 在三维中相当于沿X轴转动
            y -= Input.GetAxis("Mouse Y") * ySpeed;
            //上下需要限定范围 在完全俯视 和 完全仰视之间
            //去掉该限制的话可以使摄像机倒置
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);


            //鼠标的左右移动 移动的X值 在三维中相当于沿Y轴转动
            //左右是允许360度旋转的 不需要限制到最大最小范围
            x += Input.GetAxis("Mouse X") * xSpeed;

            //Quaternion rotation = Quaternion.Euler(y, x, 0);
            //transform.rotation = rotation;

            //和上面2句等价
            transform.eulerAngles = new Vector3(y, x, 0);
        }
    }
}