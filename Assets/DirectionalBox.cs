using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalBox : MonoBehaviour {

    // Use this for initialization
    player player;

	void Start () {
        player = GetComponentInParent<player>();

    }
	
	// Update is called once per frame
	public void ChangeDirectionPosition () {
		if (player.CurrentlyFacing == player.DirectionFacing.Right)
        {
            transform.position = player.transform.position + Vector3.right * 4;
        }
        else if (player.CurrentlyFacing == player.DirectionFacing.Left)
        {
            transform.position = player.transform.position + Vector3.left * 4;
        }
        else if (player.CurrentlyFacing == player.DirectionFacing.Up)
        {
            transform.position = player.transform.position + Vector3.up * 4;
        }
        else if (player.CurrentlyFacing == player.DirectionFacing.Down)
        {
            transform.position = player.transform.position + Vector3.down * 4;
        }
    }
}
