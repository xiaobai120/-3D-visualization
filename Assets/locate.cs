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
	public Text X;//X文本框
	public Text Y;//Y文本框
	public float speed = 5f;//移动速度
	public InputField x_input;//待输入x坐标
	public InputField y_input;//待输入y坐标
	public Button btn;//move按钮
	public Button btn0;//movedata按钮
	public Button btn1;//pause按钮
	float pos_x=0, pos_y = 0;
	bool A = false;
	bool B = false;
	bool C = false;
	DataTable dt;
	int i = 0;
	//Start函数在第一帧更新之前被调用
	void Start()
	{
		string filePath = Application.streamingAssetsPath + "\\data.csv";
		dt = OpenCSV(filePath);
		Debug.Log(dt.Rows[0][0]);
		Debug.Log(dt.Rows[0][1]);
	}

	//Update()每帧被调用一次
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
		if (A)//点击btn按钮后的事件
		{
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(-pos_x, 0, -pos_y), speed * Time.deltaTime);//移动函数
			if (transform.localPosition == new Vector3(-pos_x, 0, -pos_y)) A = false;
		}
        if (B)//点击btn0按钮后的事件
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
	public static DataTable OpenCSV(string filePath)//从csv读取数据返回table
	{
		DataTable dt= new DataTable(); 
		using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
		{
			using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
			{
				//记录每次读取的一行记录
				string strLine = "";
				//记录每行记录中的各字段内容
				string[] aryLine = null;
				string[] tableHead = null;
				//标示列数
				int columnCount = 0;
				//标示是否是读取的第一行
				bool IsFirst = true;
				//逐行读取CSV中的数据
				while ((strLine = sr.ReadLine()) != null)
				{
					if (IsFirst == true)
					{
						tableHead = strLine.Split(',');
						IsFirst = false;
						columnCount = tableHead.Length;
						//创建列
						for (int i = 0; i < columnCount; i++)
						{
							DataColumn dc = new DataColumn(tableHead[i]);
							dt.Columns.Add(dc);
						}
					}
					else
					{
						aryLine = strLine.Split(',');
						//此处可以添加条件
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
