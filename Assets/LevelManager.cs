using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    // Use this for initialization

    player[] players;
    Tile[] tiles;
    public GameObject buildUI;
    public GameObject deathBlock;
    public float BuildTime = 10f;
    private float BuildTimeOver;
    bool BuildTimeActive = true;

	void Start () {
        players = FindObjectsOfType<player>();
        tiles = FindObjectsOfType<Tile>();
        BuildTimeOver = Time.time + BuildTime;
        BuildTimeActive = true;
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
                int TotalBlocks = tiles.Length;
                int RandomBlock = Random.Range(0, TotalBlocks);
                Vector3 BlockPosition = tiles[RandomBlock].transform.position;
                Instantiate(deathBlock, BlockPosition, Quaternion.identity);

                // Deactivatebuild
                foreach (player P in players)
                {
                    P.deActiveBuild();
                }
            }
        }
	}
}
