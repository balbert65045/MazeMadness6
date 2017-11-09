using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelSetUp : MonoBehaviour {

    // Use this for initialization
    public Dropdown BuildTimerDropDown;
    public Dropdown DeathDurationDropDown;
    public Dropdown DeathResetDropDown;
    public Dropdown BoostDropDown;
    public Dropdown EveryoneDestroyDropDown;
    public Dropdown Map;

    LevelManager levelManager;

    void Start () {
        levelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
    }
    
    public void SaveandContinue()
    {

        PlayerPrefsManager.SetBuildTimer(Int32.Parse(BuildTimerDropDown.options[BuildTimerDropDown.value].text));
        PlayerPrefsManager.SetDeathDuration(Int32.Parse(DeathDurationDropDown.options[DeathDurationDropDown.value].text));
        PlayerPrefsManager.SetDeathReset(Int32.Parse(DeathResetDropDown.options[DeathResetDropDown.value].text));
        PlayerPrefsManager.SetBoost(BoostDropDown.value);
        PlayerPrefsManager.SetEveryoneDestroy(EveryoneDestroyDropDown.value);

        levelManager.LoadLevel(Map.options[Map.value].text + "_Map");

    }

}
