using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class myfirebase : MonoBehaviour
{


    private const string databaseUrl = "https://techin515-74d59-default-rtdb.firebaseio.com/";
    private const string dataPath = "/data.json";

    void Start()
    {
        getData("GSR");
    }

    string getData(string jsonName){
        string databaseUrl = "https://techin515-74d59-default-rtdb.firebaseio.com/";
        string dataPath = jsonName+".json";
        // Construct the URL to retrieve the data
        string url = databaseUrl + dataPath;

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
            findData(data,jsonName);
            //Debug.Log("Retrieved data: " + data);
        }
        return "data";
    }

    string findData(string data,string dataName){
        string[] dataArr = data.Split('\"');
        string targetData = "";
        // foreach (string s in dataArr){
        //     print(s);
        // }
        for (int i=0;i<dataArr.Length;i++){
            if (dataArr[i].Contains(dataName)){
                targetData = dataArr[i + 4];
                //print(targetData);
            }

        }
        return targetData;

    }
}
