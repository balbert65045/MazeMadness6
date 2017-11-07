using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerScreen : MonoBehaviour {

    // Use this for initialization
    public Image DeathImage;

	void Start () {
        DeathImage.gameObject.SetActive(false);

    }
	
    public void playerDead()
    {
        DeathImage.gameObject.SetActive(true);
    }

}
