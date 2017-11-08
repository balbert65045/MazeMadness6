using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckSystem : MonoBehaviour {


	public void CheckForEnoughPlayers()
    {
        PlayerAlive[] PlayersOut = FindObjectsOfType<PlayerAlive>();

        if (PlayersOut.Length > 1)
        {
            LevelManager levelmanager = FindObjectOfType<LevelManager>();
            levelmanager.LoadNextLevel();
            return;
        }
    }
}
