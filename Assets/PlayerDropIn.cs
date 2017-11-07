using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDropIn : MonoBehaviour {

    // Use this for initialization
    public int PlayerNumber = 1;
    public bool PlayerIn = false;
    Image PlayerImage;

	void Start () {
        PlayerImage = GetComponentInChildren<Image>();
        PlayerImage.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Controller" + PlayerNumber + "_Dash"))
        {
            PlayerIn = true;
            PlayerImage.gameObject.SetActive(true);
        }
	}
}
