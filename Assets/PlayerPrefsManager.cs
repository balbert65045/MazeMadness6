using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour {


    const string BUILD_TIMER_KEY = "build_timer";
    const string DEATH_DURATION_KEY = "death_duration";
    const string DEATH_RESET_KEY = "death_reset";
    const string BOOST_KEY = "boost";
    const string EVERYONE_DESTROY_KEY = "everyone_destroy";
    const string MAP_KEY = "map";

 


    //public static void SetMasterVolume(float volume)
    //{
    //    if (volume >= 0.0f && volume <= 1f)
    //    {
    //        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
    //    }

    //    else
    //    {
    //        Debug.LogError("Master volume out of range");
    //    }
    //}


    public static void SetBuildTimer(int time)
    {
        PlayerPrefs.SetInt(BUILD_TIMER_KEY, time);
    }
    public static void SetDeathDuration(int time)
    {
        PlayerPrefs.SetInt(DEATH_DURATION_KEY, time);
    }
    public static void SetDeathReset(int time)
    {
        PlayerPrefs.SetInt(DEATH_RESET_KEY, time);
    }
    public static void SetBoost(int active)
    {
        PlayerPrefs.SetInt(BOOST_KEY, active);
    }
    public static void SetEveryoneDestroy(int active)
    {
        PlayerPrefs.SetInt(EVERYONE_DESTROY_KEY, active);
    }


}
