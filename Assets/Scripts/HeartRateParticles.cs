using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

public class HeartRateParticles : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public float baseheartrate = 2500f;
    // public float lowSpeed = 10f;
    // public float lowDensity = 10f;
    // public float highSpeed = 1000f;
    // public float highDensity = 1000f;
    private float heartRate = 0f;

    void Start()
    {
        print("start heartrate");
        particleSystem = GetComponent<ParticleSystem>();
        var mainModule = particleSystem.main;
        mainModule.loop = true;
    }
    
    void Update()
    {
        // 获取PPG心率数据
        //heartRate = GetHeartRate();
        float heartRate = getData("GSR");
        // 根据心率数据更新粒子系统属性
        ParticleSystem.MainModule mainModule = particleSystem.main;
        // mainModule.duration = -1f;
        mainModule.startSpeed = Mathf.Lerp(1f, 10f, heartRate / baseheartrate);
       // mainModule.startLifetime = Mathf.Lerp(10f, 100f, heartRate / baseheartrate);
        mainModule.startRotation = Mathf.Lerp(10f, 300f, heartRate / baseheartrate);
        var emission = particleSystem.emission;
        emission.rateOverTime = Mathf.Lerp(10f, 100f, heartRate / baseheartrate);
        var burst = new ParticleSystem.Burst(0, 10, 5, 5f);
        emission.SetBurst(0, burst);
        // emission.burstSpread = Mathf.Lerp(10f, 100f, heartRate / baseheartrate);
        // emission.burstCount = (int)Mathf.Lerp(10, 100, heartRate / baseheartrate);
        particleSystem.Play();
    }

    // ParticleSystemStopBehavior.StopEmittingAndClear
    // 模拟获取PPG心率数据
    // private float GetHeartRate()
    // {
    //     // 假设heartRate是从蓝牙传感器获取的数据
    //     return heartRate;
    // }
    //jsonName: GSR, HeartRate
    float getData(string jsonName){
        string databaseUrl = "https://techin515-74d59-default-rtdb.firebaseio.com/";
        string dataPath = jsonName+".json";
        // Construct the URL to retrieve the data
        string url = databaseUrl + dataPath;
        float floatdata = 0;

        // Send the GET request
        UnityWebRequest www = UnityWebRequest.Get(url);
        www.SendWebRequest();

        // Read the data from the response
        while (!www.isDone) { }
        if (www.result != UnityWebRequest.Result.Success)
        {
            // Handle the error
        }
        else
        {
            string data = www.downloadHandler.text;
            string stringdata = "";
            stringdata=findData(data,jsonName);
            print(stringdata);
            floatdata=float.Parse(stringdata,NumberStyles.AllowLeadingSign);
            Debug.Log("Retrieved data: " + data);
        }
        return floatdata;
    }

    string findData(string data,string dataName){
        string[] dataArr = data.Split('\"');
        string targetData = "";
        // foreach (string s in dataArr){
        //     print(s);
        // }
        for (int i=0;i<dataArr.Length;i++){
            // print(dataArr[i]);
            // print(i);
            if (dataArr[i].Contains(dataName)){
                targetData = dataArr[i + 4];
                // print("find!!!!!!!");
                print(targetData);
            }

        }
        return targetData;

    }
}

