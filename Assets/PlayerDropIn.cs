using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDropIn : MonoBehaviour {

    // Use this for initialization
    public int PlayerNumber = 1;
    public bool PlayerIn = false;
    public Text text;
    Image PlayerImage;

    PlayerAlive playerAlive;
    public GameObject PlayerGameObjectAlive;

	void Start () {

        PlayerImage = GetComponentInChildren<Image>();
        PlayerAlive[] PlayersAlive =  FindObjectsOfType<PlayerAlive>();
        foreach (PlayerAlive PA in PlayersAlive)
        {
            if (PA.playerNumber == PlayerNumber)
            {
                playerAlive = PA;
                PlayerImage.gameObject.SetActive(true);
                text.gameObject.SetActive(false);
                return;
            }
        }
        PlayerImage.gameObject.SetActive(false);
        text.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update() {

        if (playerAlive == null)
        {
            if (Input.GetButtonDown("Controller" + PlayerNumber + "_Dash"))
            {
                PlayerIn = true;
                PlayerImage.gameObject.SetActive(true);
                text.gameObject.SetActive(false);
                GameObject player = Instantiate(PlayerGameObjectAlive);
                player.name = "Player" + PlayerNumber;
                playerAlive = player.GetComponent<PlayerAlive>();
                playerAlive.playerNumber = PlayerNumber;

            }
        }

        if (playerAlive != null)
        {
            if (Input.GetButtonDown("Controller" + PlayerNumber + "_Cancel"))
            {
                PlayerIn = false;
                PlayerImage.gameObject.SetActive(false);
                text.gameObject.SetActive(true);
                Destroy(playerAlive.gameObject);
                playerAlive = null;
            }
        }
    }
}
