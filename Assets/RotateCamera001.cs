using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera001 : MonoBehaviour
{
    //�����ǵ�һ�˳������
    public float moveSpeed = 5f;
    public float xSpeed = 2f; //������ת��������ٶ�
    public float ySpeed = 2f; //������ת��������ٶ�
    public float yMinLimit = -90f; //������ת���������С�Ƕ�����
    public float yMaxLimit = 90f;  //������ת����������Ƕ�����


    //�Ƿ���ת��ͷ ����Ҽ�����ʱ����
    private bool isRotate;
    //������ת�ĽǶ� �������ԭʼλ�� ��transform��ʼλ��(����άY��)
    private float x;
    //������ת�ĽǶ� �������ԭʼλ�� ��transform��ʼλ��(����άX��)
    private float y;



    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        //���������ƶ� ����ά���൱����Y��ת��
        x = angles.y;
        //���������ƶ� ����ά���൱����X��ת��
        y = angles.x;
    }


    void LateUpdate()
    {
        //��LateUpdate�и�������� ���Ը�ƽ�� ��Update�л��ж���
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
            //x �� y �Ǿ���ֵ �������������û��ת����λ�õ�����ת��λ��

            //���������ƶ� �ƶ���Yֵ ����ά���൱����X��ת��
            y -= Input.GetAxis("Mouse Y") * ySpeed;
            //������Ҫ�޶���Χ ����ȫ���� �� ��ȫ����֮��
            //ȥ�������ƵĻ�����ʹ���������
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);


            //���������ƶ� �ƶ���Xֵ ����ά���൱����Y��ת��
            //����������360����ת�� ����Ҫ���Ƶ������С��Χ
            x += Input.GetAxis("Mouse X") * xSpeed;

            //Quaternion rotation = Quaternion.Euler(y, x, 0);
            //transform.rotation = rotation;

            //������2��ȼ�
            transform.eulerAngles = new Vector3(y, x, 0);
        }
    }
}