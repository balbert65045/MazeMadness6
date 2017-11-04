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


    public Tile TileUp;
    public Tile TileDown;
    public Tile TileRight;
    public Tile TileLeft;

    public LayerMask mask = 8;

    private void Start()
    {
        //Find Tiles Nearby
        RaycastHit2D hitup = Physics2D.Raycast(transform.position + Vector3.up*5f, Vector2.up, 10f, mask);
        if (hitup.transform != null) { TileUp = hitup.transform.GetComponent<Tile>(); }

        RaycastHit2D hitdown = Physics2D.Raycast(transform.position + Vector3.down * 5f, Vector2.down, 10f, mask);
        if (hitdown.transform != null) { TileDown = hitdown.transform.GetComponent<Tile>(); }

        RaycastHit2D hitright = Physics2D.Raycast(transform.position + Vector3.right * 5f, Vector2.right, 10f, mask);
        if (hitright.transform != null) { TileRight = hitright.transform.GetComponent<Tile>(); }

        RaycastHit2D hitleft = Physics2D.Raycast(transform.position + Vector3.left * 5f, Vector2.left, 10f, mask);
        if (hitleft.transform != null) { TileLeft = hitleft.transform.GetComponent<Tile>(); }
    }


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
                if (TileUp != null) { TileUp.WallDown = wall; }
                break;
            case player.DirectionFacing.Down:
                WallDown = wall;
                if (TileDown != null) { TileDown.WallUp = wall; }
                break;
            case player.DirectionFacing.Right:
                WallRight = wall;
                if (TileRight != null) { TileRight.WallLeft = wall; }
                break;
            case player.DirectionFacing.Left:
                WallLeft = wall;
                if (TileLeft != null) { TileLeft.WallRight = wall; }
                break;
        }
    }

    public void DestoryWall(player.DirectionFacing playerdirection)
    {
        switch (playerdirection)
        {
            case player.DirectionFacing.Up:
                Destroy(WallUp);
                WallUp = null;
                if (TileUp != null) { TileUp.WallDown = null; }
                break;
            case player.DirectionFacing.Down:
                Destroy(WallDown);
                WallDown = null;
                if (TileDown != null) { TileDown.WallUp = null; }
                break;
            case player.DirectionFacing.Right:
                Destroy(WallRight);
                WallRight = null;
                if (TileRight != null) { TileRight.WallLeft = null; }
                break;
            case player.DirectionFacing.Left:
                Destroy(WallLeft);
                WallLeft = null;
                if (TileLeft != null) { TileLeft.WallRight = null; }
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(11.5f, 11.5f, 0));
        Gizmos.DrawWireCube(transform.position, new Vector3(5.25f, 5.25f, 0));
    }

}
