using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartClick : MonoBehaviour
{
    public void OnStartBtnClick()
    {
        print("buttonclick");
        SceneManager.LoadScene("Med_Blue_Scene", LoadSceneMode.Single);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
