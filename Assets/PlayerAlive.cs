using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlive : MonoBehaviour {

    public int playerNumber;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
