using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class screenController : MonoBehaviour {

    // Use this for initialization
    public GameObject newPlayerScreen;
    public GameObject startScreen;
    public GameObject FirstButtonToSee;
    private EventSystem ES;

    private void Start()
    {
        ES = FindObjectOfType<EventSystem>();
    }

    public void ChangeToPlayerScreen()
    {

        ES.firstSelectedGameObject = FirstButtonToSee;
        startScreen.SetActive(false);
        newPlayerScreen.SetActive(true);
    }

    public void ChangeToStartScreen()
    {
        startScreen.SetActive(true);
        newPlayerScreen.SetActive(false);
    }

}
