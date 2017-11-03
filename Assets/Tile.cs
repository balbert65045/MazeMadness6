using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public Vector3 UpPosition { get { return (new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z)); } }
    public Vector3 DownPosition { get { return (new Vector3(transform.position.x, transform.position.y - 5f, transform.position.z)); } }
    public Vector3 RightPosition { get { return (new Vector3(transform.position.x + 5f, transform.position.y, transform.position.z)); } }
    public Vector3 LeftPosition { get { return (new Vector3(transform.position.x - 5f, transform.position.y, transform.position.z)); } }

    public GameObject WallUp;
    public GameObject WallDown;
    public GameObject WallRight;
    public GameObject WallLeft;

    public bool CheckForTile(player.DirectionFacing playerdirection)
    {
        switch (playerdirection)
        {
            case player.DirectionFacing.Up:
                if (WallUp != null) { return (true); }
                else { return false; }
            case player.DirectionFacing.Down:
                if (WallDown != null) { return (true); }
                else { return false; }
            case player.DirectionFacing.Right:
                if (WallRight != null) { return (true); }
                else { return false; }
            case player.DirectionFacing.Left:
                if (WallLeft != null) { return (true); }
                else { return false; }
        }
        return false;
    }

    public void Build(player.DirectionFacing playerdirection, GameObject wall)
    {
        switch (playerdirection)
        {
            case player.DirectionFacing.Up:
                WallUp = wall;
                break;
            case player.DirectionFacing.Down:
                WallDown = wall;
                break;
            case player.DirectionFacing.Right:
                WallRight = wall;
                break;
            case player.DirectionFacing.Left:
                WallLeft = wall;
                break;
        }
    }

    public void DestoryWall(player.DirectionFacing playerdirection)
    {
        switch (playerdirection)
        {
            case player.DirectionFacing.Up:
                Destroy(WallUp);
                break;
            case player.DirectionFacing.Down:
                Destroy(WallDown);
                break;
            case player.DirectionFacing.Right:
                Destroy(WallRight);
                break;
            case player.DirectionFacing.Left:
                Destroy(WallLeft);
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(11.5f, 11.5f, 0));
        Gizmos.DrawWireCube(transform.position, new Vector3(5.25f, 5.25f, 0));
    }

}
