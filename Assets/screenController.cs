using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenController : MonoBehaviour {

    // Use this for initialization
    public GameObject newPlayerScreen;
    public GameObject startScreen;

    public void ChangeToPlayerScreen()
    {
        startScreen.SetActive(false);
        newPlayerScreen.SetActive(true);
    }
}
