using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

public class MyEffectControl : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public float baseGSR = 2500f;
    private float GSR = 0f;

    void Start()
    {
        print("start GSR");
        particleSystem = GetComponent<ParticleSystem>();
        var mainModule = particleSystem.main;
        mainModule.loop = true;
    }
    
    void Update()
    {
        // get GSR data
        float GSR = getData("GSR");
        // update Particle System effects according to GSR data
        ParticleSystem.MainModule mainModule = particleSystem.main;
        // mainModule.duration = -1f;
        mainModule.startSpeed = Mathf.Lerp(1f, 10f, GSR / baseGSR);
       // mainModule.startLifetime = Mathf.Lerp(10f, 100f, GSR / baseGSR);
        mainModule.startRotation = Mathf.Lerp(10f, 300f, GSR / baseGSR);
        var emission = particleSystem.emission;
        emission.rateOverTime = Mathf.Lerp(5f, 20f, GSR / baseGSR);
        var burst = new ParticleSystem.Burst(0, 10, 5, 5f);
        emission.SetBurst(0, burst);
        // emission.burstSpread = Mathf.Lerp(10f, 100f, GSR / baseGSR);
        // emission.burstCount = (int)Mathf.Lerp(10, 100, GSR / baseGSR);
        particleSystem.Play();
    }


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
            //print(stringdata);
            floatdata=float.Parse(stringdata,NumberStyles.AllowLeadingSign);
//            Debug.Log("Retrieved data: " + data);
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
                //print(targetData);
            }

        }
        return targetData;

    }
}

