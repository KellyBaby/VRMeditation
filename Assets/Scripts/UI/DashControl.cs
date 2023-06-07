using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashControl : MonoBehaviour
{
    public Text textComponent; // Reference to the Text component
    // void Start(){
    //     textComponent.text = "aaaa";
    // }

    public void setText(string newText)
    {
        textComponent.text = newText;
    }
}