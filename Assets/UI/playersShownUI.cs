using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playersShownUI : MonoBehaviour {

    public GameObject Player1Screen;
    public GameObject Player2Screen;
    public GameObject Player3Screen;
    public GameObject Player4Screen;


    // Use this for initialization
    void Start () {

        Player1Screen.SetActive(false);
        Player2Screen.SetActive(false);
        Player3Screen.SetActive(false);
        Player4Screen.SetActive(false);

        PlayerAlive[] playersAlive = FindObjectsOfType<PlayerAlive>();

        foreach (PlayerAlive PA in playersAlive)
        {
            if (PA.playerNumber == 1)
            {
                Player1Screen.SetActive(true);
            }
            else if (PA.playerNumber == 2)
            {
                Player2Screen.SetActive(true);
            }
            else if (PA.playerNumber == 3)
            {
                Player3Screen.SetActive(true);
            }
            else if (PA.playerNumber == 4)
            {
                Player4Screen.SetActive(true);
            }
        }

	}
	

    public void PlayerDead(int PNumber)
    {
        if (PNumber == 1)
        {
            Player1Screen.GetComponent<playerScreen>().playerDead(); ;
        }
        else if (PNumber == 2)
        {
            Player2Screen.GetComponent<playerScreen>().playerDead(); ;
        }
        else if (PNumber == 3)
        {
            Player3Screen.GetComponent<playerScreen>().playerDead(); ;
        }
        else if (PNumber == 4)
        {
            Player4Screen.GetComponent<playerScreen>().playerDead(); ;
        }
    }


}
