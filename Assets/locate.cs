using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Data;
using System.IO;
using System.Text;

public class locate : MonoBehaviour
{
	public Text X;//X�ı���
	public Text Y;//Y�ı���
	public float speed = 5f;//�ƶ��ٶ�
	public InputField x_input;//������x����
	public InputField y_input;//������y����
	public Button btn;//move��ť
	public Button btn0;//movedata��ť
	public Button btn1;//pause��ť
	float pos_x=0, pos_y = 0;
	bool A = false;
	bool B = false;
	bool C = false;
	DataTable dt;
	int i = 0;
	//Start�����ڵ�һ֡����֮ǰ������
	void Start()
	{
		string filePath = Application.streamingAssetsPath + "\\data.csv";
		dt = OpenCSV(filePath);
		Debug.Log(dt.Rows[0][0]);
		Debug.Log(dt.Rows[0][1]);
	}

	//Update()ÿ֡������һ��
	void Update()
	{
		X.text = (-transform.localPosition.x).ToString();
		Y.text= (-transform.localPosition.z).ToString();
		btn.onClick.AddListener(M);
		btn0.onClick.AddListener(N);
		btn1.onClick.AddListener(K);
		if (x_input.text.Length != 0)
		{
			if (x_input.text=="-") ; 
			else
			{
				pos_x = float.Parse(x_input.text.ToString());
				if (pos_x > 6.0)
				{
					pos_x = 6f;
					x_input.text = "6.00";
				}
				else if (pos_x < 0.0)
				{
					pos_x = 0f;
					x_input.text = "0.00";
				}
			}
		}
		if (y_input.text.Length != 0)
		{
			if (y_input.text  == "-") ;
			else
			{
				pos_y = float.Parse(y_input.text.ToString());
				if (pos_y > 11.0)
				{
					pos_y = 11f;
					y_input.text = "11.00";
				}
				else if (pos_y < 0.0)
				{
					pos_y = 0f;
					y_input.text = "0.00";
				}
			}
		}
		if (A)//���btn��ť����¼�
		{
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(-pos_x, 0, -pos_y), speed * Time.deltaTime);//�ƶ�����
			if (transform.localPosition == new Vector3(-pos_x, 0, -pos_y)) A = false;
		}
        if (B)//���btn0��ť����¼�
        {
			if (i < dt.Rows.Count)
			{
				float a = float.Parse(dt.Rows[i][0].ToString());
				float b = float.Parse(dt.Rows[i][1].ToString());
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(-a, 0, -b), speed * Time.deltaTime);
				if (transform.localPosition == new Vector3(-a, 0, -b)) i++;
			}
			else B = false;
		}
	}
	void M()
	{	
		A = true;
	}
	void N()
	{
		B = true;
	}
	void K()
	{
		A = false;
		B = false ;
	}
	public static DataTable OpenCSV(string filePath)//��csv��ȡ���ݷ���table
	{
		DataTable dt= new DataTable(); 
		using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
		{
			using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
			{
				//��¼ÿ�ζ�ȡ��һ�м�¼
				string strLine = "";
				//��¼ÿ�м�¼�еĸ��ֶ�����
				string[] aryLine = null;
				string[] tableHead = null;
				//��ʾ����
				int columnCount = 0;
				//��ʾ�Ƿ��Ƕ�ȡ�ĵ�һ��
				bool IsFirst = true;
				//���ж�ȡCSV�е�����
				while ((strLine = sr.ReadLine()) != null)
				{
					if (IsFirst == true)
					{
						tableHead = strLine.Split(',');
						IsFirst = false;
						columnCount = tableHead.Length;
						//������
						for (int i = 0; i < columnCount; i++)
						{
							DataColumn dc = new DataColumn(tableHead[i]);
							dt.Columns.Add(dc);
						}
					}
					else
					{
						aryLine = strLine.Split(',');
						//�˴������������
						{
							DataRow dr = dt.NewRow();
							for (int j = 0; j < columnCount; j++)
							{
								dr[j] = aryLine[j];
							}
							dt.Rows.Add(dr);
						}

					}
				}
				if (aryLine != null && aryLine.Length > 0)
				{
					dt.DefaultView.Sort = tableHead[0] + " " + "asc";
				}
				sr.Close();
				fs.Close();
				return dt;
			}
		}
	}
}
//redis
//2.516646 1.459816
