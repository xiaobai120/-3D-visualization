using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ServiceStack.Redis;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HslCommunication.MQTT;
using Newtonsoft;
public class RedisGetData : MonoBehaviour
{
    string exePath = @"D:\Unity\Redis\redis-server.exe";
    RedisClient redisClient = new RedisClient("127.0.0.1", 6379);//����Redis������
    // Start is called before the first frame update
    void Start()
    {
        Process pro = new Process();
        pro.StartInfo.FileName = exePath;
        pro.StartInfo.UseShellExecute = true;
        pro.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        pro.Start();
    }

    // Update is called once per frame
    void Update()
    {
        string Get(string key)
        {
            return redisClient.GetValue(key);
        }
    }
    private void OnDestroy()
    {
        KillProcess("redis-server");
    }
    private void KillProcess(string processName)
    {
        //processName��exe���ļ���

        Process[] processes = Process.GetProcesses();
        foreach (Process process in processes)
        {
            try
            {
                if (!process.HasExited)
                {
                    if (process.ProcessName == processName)
                    {
                        process.Kill();
                        UnityEngine.Debug.Log("�ѹرս���");
                    }
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e.ToString());
            }
        }
    }
}

