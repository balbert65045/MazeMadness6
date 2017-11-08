using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    // Use this for initialization
    public Transform SpawnPositionPlayer1;
    public Transform SpawnPositionPlayer2;
    public Transform SpawnPositionPlayer3;
    public Transform SpawnPositionPlayer4;

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;


    void Awake () {
        PlayerAlive[] PlayersAlive = FindObjectsOfType<PlayerAlive>();
        
        foreach (PlayerAlive PA in PlayersAlive)
        {
            if (PA.playerNumber == 1)
            {
                GameObject P1 = Instantiate(Player1, SpawnPositionPlayer1.position, Quaternion.identity);
                P1.transform.SetParent(this.transform);
            }
            else if (PA.playerNumber == 2)
            {
                GameObject P2 = Instantiate(Player2, SpawnPositionPlayer2.position, Quaternion.identity);
                P2.transform.SetParent(this.transform);
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
