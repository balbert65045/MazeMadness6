using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour {

    // Use this for initialization
    Text winText;

    private void Awake()
    {
        winText = GetComponentInChildren<Text>();
    }


    public void ShowWinText(int PlayerNumber)
    {
        winText.text = "Player " + PlayerNumber + " Won!";
    }
}
