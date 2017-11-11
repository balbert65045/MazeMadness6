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


        int BuildTime = PlayerPrefsManager.GetBuildTimer();

        for (int i = 0; i < BuildTimerDropDown.options.Count; i++)
        {
            if (BuildTimerDropDown.options[i].text == BuildTime.ToString())
            {
                BuildTimerDropDown.value = i;
            }
        }

        int DeathDuration = PlayerPrefsManager.GetDeathDuration();

        for (int i = 0; i < DeathDurationDropDown.options.Count; i++)
        {
            if (DeathDurationDropDown.options[i].text == DeathDuration.ToString())
            {
                DeathDurationDropDown.value = i;
            }
        }

        int DeathReset = PlayerPrefsManager.GetDeathReset();

        for (int i = 0; i < DeathResetDropDown.options.Count; i++)
        {
            if (DeathResetDropDown.options[i].text == DeathReset.ToString())
            {
                DeathResetDropDown.value = i;
            }
        }

        int Boost = PlayerPrefsManager.GetBoost();
        BoostDropDown.value = Boost;

        int EveryoneCanDestroy = PlayerPrefsManager.GetEveryoneDestroy();
        EveryoneDestroyDropDown.value = EveryoneCanDestroy;




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
