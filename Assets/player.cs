using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour {


    public Canvas canvas;

    Rigidbody2D m_rigidbody;

    public float MoveMagnitude = 10f;

    public Tile TileOn;

    public GameObject enviroment;
    public GameObject WallImage;
    public GameObject Wall;
    private GameObject WallImageShown;

    public enum DirectionFacing { Up, Right, Down, Left, }
    public DirectionFacing CurrentlyFacing = DirectionFacing.Left;


    private bool m_isAxisInUse = false;
    private bool BuildTrigger = false;

    // Use this for initialization
    void Start () {
        m_rigidbody = GetComponent<Rigidbody2D>();
       
    }
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 MoveForceDirection = new Vector2(h, v).normalized;
        Vector2 MoveForce = MoveForceDirection * MoveMagnitude;

        m_rigidbody.AddForce(MoveForce);
       FindLookingDirection();


       if (Input.GetAxis("Build") != 0)
        {
            m_isAxisInUse = true;
            
        }
       else if (Input.GetAxis("Build") == 0)
        {
            m_isAxisInUse = false;
            BuildTrigger = true;
        }

       if (m_isAxisInUse && BuildTrigger)
        {
            if (TileOn.CheckBuild(CurrentlyFacing))
            {
                Instantiate(Wall, WallImageShown.transform.position, WallImageShown.transform.rotation);
                TileOn.Build(CurrentlyFacing);
                BuildTrigger = false;
            }
        }


    }


    void FindLookingDirection()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            if (x > 0.05)
            {
                CurrentlyFacing = DirectionFacing.Right;
            }
            else if (x < -0.05)
            {
                CurrentlyFacing = DirectionFacing.Left;
            }
        }
        else if ((Mathf.Abs(x) < Mathf.Abs(y)))
        {
            if (y > 0.05)
            {
                CurrentlyFacing = DirectionFacing.Up;
            }
            else if (y < -0.05)
            {
                CurrentlyFacing = DirectionFacing.Down;
            }
        }

        if (WallImageShown != null)
        {
            PositionImage();
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("CollisionMade");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Tile>())
        {
            TileOn = collision.gameObject.GetComponent<Tile>();

            if (WallImageShown == null)
            {
                WallImageShown = Instantiate(WallImage, TileOn.LeftPosition, Quaternion.Euler(Vector3.zero));
            }
            else
            {
                PositionImage();
            }
        }
    }


    void PositionImage()
    {

        Vector3 Position = Vector3.zero;
        Vector3 EulerRotation = Vector3.zero;
        switch (CurrentlyFacing)
        {
            case DirectionFacing.Left:
                Position = TileOn.LeftPosition;
                EulerRotation = Vector3.zero;
                break;
            case DirectionFacing.Right:
                Position = TileOn.RightPosition;
                EulerRotation = Vector3.zero;
                break;
            case DirectionFacing.Up:
                Position = TileOn.UpPosition;
                EulerRotation = new Vector3(0, 0, 90);
                break;
            case DirectionFacing.Down:
                Position = TileOn.DownPosition;
                EulerRotation = new Vector3(0, 0, 90);
                break;
        }

        WallImageShown.transform.position = Position;
        WallImageShown.transform.rotation = Quaternion.Euler(EulerRotation);
    }
}
