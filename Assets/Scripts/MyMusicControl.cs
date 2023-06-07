using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MyMusicControl : MonoBehaviour
{
    public string portName = "COM111"; //port name, if needed
    public int baudRate = 9600;
    public AudioSource background_audioSource;
    public AudioClip[] background_audioClips;

    public AudioSource main_audioSource;
    public AudioClip[] main_audioClips;
    private static int current_backgroundClip=0;

    public Text textComponent;

    public static float heartRate = 1.0f; //fake number
    private float timer=0;

    //SerialPort serialPort;

    // Start is called before the first frame update
    void Start()
    {
        //serialPort = new SerialPort(portName, baudRate);
        //serialPort.ReadTimeout = 100;

        // // try the port
        // {
        //     serialPort.Open()
        // }
        // catch
        // {
        //     Debug.Log("Cannot Open!!" + e.Message);
        // }
        background_audioClips =  new AudioClip[]{(AudioClip)Resources.Load("Music/wind"),
                                     (AudioClip)Resources.Load("Music/rain"), 
                                     (AudioClip)Resources.Load("Music/thunder")};
        background_audioSource.clip = background_audioClips[current_backgroundClip];
        background_audioSource.volume=0.6f;
        background_audioSource.Play();

        main_audioClips =  new AudioClip[]{(AudioClip)Resources.Load("Music/happy_music"),
                                (AudioClip)Resources.Load("Music/meditation_guidance")};
        main_audioSource.clip=main_audioClips[1];
        main_audioSource.Play();
        InvokeRepeating("switchBackgroundMusic", 2f, 2f);

    }

    // Update is called once per frame
    void Update()
    {
        // read data from port
        try
        {
            //string data = serialPort.ReadLine().Trim();
            heartRate = getData("HeartRate");
            timer+=Time.deltaTime;
            //print(heartRate);

            // music part****
            // if (heartRate == 0)
            // {
            //     audioSource.Pause();
            // }
            // else
            // {
            //     float speed = 5.0f + heartRate / 10;
            //     float volume = 5.0f + heartRate / 10;
            //     audioSource.pitch = speed;
            //     audioSource.volume = volume;
            //     audioSource.Play();
            // }
            setText(string.Format("StateScore:{0}",170-heartRate));
            // switchBackgroundMusic();
            if (timer>=25){
                SceneManager.LoadScene("Med_Blue_Scene", LoadSceneMode.Single);
            }
            print(timer);




        }
        catch (System.Exception)
        {
            // Handle the exception
        }
    }

    public void setText(string newText)
    {
        textComponent.text = newText;
    }


    void switchBackgroundMusic(){
        print(string.Format("current_backgroundClip:{0},heartRate:{1}",current_backgroundClip,heartRate));
        if((heartRate<90) &&(current_backgroundClip!=0)){
            current_backgroundClip=0;
            background_audioSource.clip=background_audioClips[current_backgroundClip];
            background_audioSource.volume=0.6f;
            background_audioSource.Play();
        }
        else if((heartRate>=90) &&(heartRate<120) &&(current_backgroundClip!=1)){
            current_backgroundClip=1;
            background_audioSource.clip=background_audioClips[current_backgroundClip];
            background_audioSource.volume=0.5f;
            background_audioSource.Play();
        }
        else if((heartRate>120)&&(current_backgroundClip!=2)){
            current_backgroundClip=2;
            background_audioSource.clip=background_audioClips[current_backgroundClip];
            background_audioSource.volume=0.4f;
            background_audioSource.Play();
        }
        else{
            // print("Audio not changed");
        }
        
    }

    // void OnDisable()
    // {
    //     if (serialPort != null && serialPort.IsOpen)
    //     {
    //         serialPort.Close();
    //     }
    // }

    // get data from database
    float getData(string jsonName)
    {
        string databaseUrl = "https://techin515-74d59-default-rtdb.firebaseio.com/";
        string dataPath = jsonName + ".json";
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
            stringdata = findData(data, jsonName);
            //print(stringdata);
            floatdata = float.Parse(stringdata, CultureInfo.InvariantCulture);
//            Debug.Log("Retrieved data: " + data);
        }
        return floatdata;
    }

    string findData(string data, string dataName)
    {
        string[] dataArr = data.Split('\"');
        string targetData = "";
        // foreach (string s in dataArr){
        //     print(s);
        // }
        for (int i = 0; i < dataArr.Length; i++)
        {
            // print(dataArr[i]);
            // print(i);
            if (dataArr[i].Contains(dataName))
            {
                targetData = dataArr[i + 4];
                // print("find!!!!!!!");
                //print(targetData);
            }

        }
        return targetData;

    }
}
