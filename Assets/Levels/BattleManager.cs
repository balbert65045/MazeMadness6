using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {

    // Use this for initialization

    player[] players;
    List<player> playersAlive = new List<player>();
    Tile[] tiles;

    public GameObject buildUI;
    public GameObject WinScreen;
    StartScreen startScreen;

    public GameObject deathBlock;
    private float DeathTime;
    private float DeathTimeDuration;
    private bool DeathTimeActive = false;

    public GameObject DeathboxUI;
    private Slider DeathSlider;
    private Text DeathResetTimer;

    public float BuildTime = 10f;
    private float BuildTimeOver;
    bool BuildTimeActive = true;

    public float CountdownTime = 6f;
    float CountdownStart;

    public float DeathTimeReset;
    private float DeathBlockSpawnTime;
    GameObject deathBlockOut;

    void Start () {
        Time.timeScale = 1;

        players = FindObjectsOfType<player>();
        startScreen = FindObjectOfType<StartScreen>();
        foreach (player P in players)
        {
            playersAlive.Add(P);
            P.Freeze();
        }
        tiles = FindObjectsOfType<Tile>();
        BuildTimeActive = true;

        DeathSlider = DeathboxUI.GetComponentInChildren<Slider>();
        DeathResetTimer = DeathboxUI.GetComponentInChildren<Text>();

        DeathSlider.gameObject.SetActive(false);
        DeathboxUI.SetActive(false);
        WinScreen.SetActive(false);
        buildUI.SetActive(false);


        CountdownStart = Time.time;
        startScreen.CountdownText.text = ((int)CountdownTime).ToString();

    }
	
	// Update is called once per frame
	void Update () {
        if (startScreen.gameObject.activeSelf)
        {
            StartCoroutine(StartCountDown());
        }


        else
        {
            if (BuildTimeActive)
            {
                StartCoroutine(BuildTimeCountDown());
            }

            if (DeathTimeActive)
            {
                float Value = (DeathTime - Time.time) / DeathTimeDuration;
                DeathboxUI.GetComponentInChildren<Slider>().value = Value;
                if (Time.time > DeathTime)
                {
                    foreach (player P in players)
                    {
                        P.DisableDeathPowers();
                    }
                    DeathSlider.gameObject.SetActive(false);
                    DeathResetTimer.gameObject.SetActive(true);
                    SpawnNewDeathBox();
                    DeathTimeActive = false;
                }
            }

            if (deathBlockOut != null)
            {
                DeathResetTimer.text = ((int)(DeathBlockSpawnTime + DeathTimeReset - Time.time)).ToString();
                if (Time.time > DeathBlockSpawnTime + DeathTimeReset)
                {
                    Destroy(deathBlockOut);
                    SpawnNewDeathBox();
                }
            }
        }
	}

    //IEnumerator DeathTimeCountDown()
    //{

    //}


    IEnumerator BuildTimeCountDown()
    {
        buildUI.GetComponentInChildren<Text>().text = ((int)(BuildTimeOver - Time.time)).ToString();
        if (Time.time > BuildTimeOver)
        {
            // Disable the UI
            buildUI.SetActive(false);
            BuildTimeActive = false;

            // Spawn a block
            SpawnNewDeathBox();
            DeathboxUI.SetActive(true);
            // Deactivatebuild
            foreach (player P in players)
            {
                P.deActiveBuild();
            }
        }
        yield return new WaitForSeconds(.9f);

    }


        IEnumerator StartCountDown()
    {
        startScreen.CountdownText.text = ((int)(CountdownTime - (Time.time - CountdownStart))).ToString();
        yield return new WaitForSeconds(.9f);
        if (Time.time - CountdownStart > CountdownTime)
        {
            startScreen.gameObject.SetActive(false);
            BuildTimeOver = Time.time + BuildTime;
            buildUI.SetActive(true);
            foreach (player P in playersAlive)
            {
                P.unFreeze();
            }
        }
        else if (Time.time - CountdownStart + 1 > CountdownTime)
        {
            startScreen.TitleText.text = "GO!";
        }

    }




    public void SpawnNewDeathBox()
    {
        int TotalBlocks = tiles.Length;
        int RandomBlock = Random.Range(0, TotalBlocks);
        Vector3 BlockPosition = tiles[RandomBlock].transform.position;
        deathBlockOut = Instantiate(deathBlock, BlockPosition, Quaternion.identity);
        DeathBlockSpawnTime = Time.time;
    }

    public void StartDeathTimer(float newDeathTime)
    {
        DeathTimeDuration = newDeathTime;
        DeathTime = Time.time + DeathTimeDuration;
        DeathTimeActive = true;
        DeathSlider.gameObject.SetActive(true);
        DeathResetTimer.gameObject.SetActive(false);

    }

    public void CheckForWin(player playerDead)
    {
        playersAlive.Remove(playerDead);

        if (playersAlive.Count <= 1)
        {
            Time.timeScale = 0;
            WinScreen.SetActive(true);
            WinScreen winScreen = WinScreen.GetComponent<WinScreen>();
            winScreen.ShowWinText(playersAlive[0].Player);
        }

       
    }
}

