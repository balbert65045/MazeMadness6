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

    public GameObject deathBlock;
    private float DeathTime;
    private float DeathTimeDuration;
    private bool DeathTimeActive = false;
    public GameObject DeathboxUI;


    public float BuildTime = 10f;
    private float BuildTimeOver;
    bool BuildTimeActive = true;

	void Start () {
        Time.timeScale = 1;
        players = FindObjectsOfType<player>();
        foreach (player P in players)
        {
            playersAlive.Add(P);
        }
        tiles = FindObjectsOfType<Tile>();
        BuildTimeOver = Time.time + BuildTime;
        BuildTimeActive = true;
        DeathboxUI.SetActive(false);
        WinScreen.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if (BuildTimeActive)
        {
            buildUI.GetComponentInChildren<Text>().text = ((int)(BuildTimeOver- Time.time)).ToString();

            if (Time.time > BuildTimeOver)
            {
                // Disable the UI
                buildUI.SetActive(false);
                BuildTimeActive = false;

                // Spawn a block
                SpawnNewDeathBox();

                // Deactivatebuild
                foreach (player P in players)
                {
                    P.deActiveBuild();
                }
            }
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
                DeathboxUI.gameObject.SetActive(false);
                SpawnNewDeathBox();
                DeathTimeActive = false;
            }
        }
	}

    public void SpawnNewDeathBox()
    {
        int TotalBlocks = tiles.Length;
        int RandomBlock = Random.Range(0, TotalBlocks);
        Vector3 BlockPosition = tiles[RandomBlock].transform.position;
        Instantiate(deathBlock, BlockPosition, Quaternion.identity);
    }

    public void StartDeathTimer(float newDeathTime)
    {
        DeathTimeDuration = newDeathTime;
        DeathTime = Time.time + DeathTimeDuration;
        DeathTimeActive = true;
        DeathboxUI.SetActive(true);

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
