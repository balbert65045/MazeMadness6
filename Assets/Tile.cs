using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public Vector3 UpPosition { get { return (new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z)); } }
    public Vector3 DownPosition { get { return (new Vector3(transform.position.x, transform.position.y - 5f, transform.position.z)); } }
    public Vector3 RightPosition { get { return (new Vector3(transform.position.x + 5f, transform.position.y, transform.position.z)); } }
    public Vector3 LeftPosition { get { return (new Vector3(transform.position.x - 5f, transform.position.y, transform.position.z)); } }

    public bool WallUp = false;
    public bool WallDown = false;
    public bool WallRight = false;
    public bool WallLeft = false;

    public bool CheckBuild(player.DirectionFacing playerdirection)
    {
        switch (playerdirection)
        {
            case player.DirectionFacing.Up:
                if (WallUp) { return (false); }
                else { return true; }
            case player.DirectionFacing.Down:
                if (WallDown) { return (false); }
                else { return true; }
            case player.DirectionFacing.Right:
                if (WallRight) { return (false); }
                else { return true; }
            case player.DirectionFacing.Left:
                if (WallLeft) { return (false); }
                else { return true; }
        }
        return false;
    }

    public void Build(player.DirectionFacing playerdirection)
    {
        switch (playerdirection)
        {
            case player.DirectionFacing.Up:
                WallUp = true;
                break;
            case player.DirectionFacing.Down:
                WallDown = true;
                break;
            case player.DirectionFacing.Right:
                WallRight = true;
                break;
            case player.DirectionFacing.Left:
                WallLeft = true;
                break;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(11.5f, 11.5f, 0));
        Gizmos.DrawWireCube(transform.position, new Vector3(5.25f, 5.25f, 0));
    }

}
