using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour {

    public int Player = 1;

    Slider DestroySlider;
    public float DestroySpeed = 1f;

    public float MoveMagnitude = 10f;
    public float DeathCubeIncreaseSpeed = 100;

    public float boostSpeed = 10;
    public float BoostTimer = 1f;
    private bool boostAvailable = true;
    private float lastBoostTime;

    public Tile TileOn;

    public GameObject enviroment;
    public GameObject WallImage;
    public GameObject Wall;
    private GameObject WallImageShown;

    Rigidbody2D m_rigidbody;

    public enum DirectionFacing { Up, Right, Down, Left, }
    public DirectionFacing CurrentlyFacing = DirectionFacing.Left;


    private bool BuildEnable = true;
    public bool m_isAxisInUse = false;
    private bool BuildTrigger = false;

    public bool DeathBox = false;
    
    BattleManager battleManager;

    private PowerIndicator powerIndicator;
    // Use this for initialization

    public void Freeze()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
    }

    public void unFreeze()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }


    public void Dead()
    {
        Destroy(WallImageShown);
        FindObjectOfType<playersShownUI>().PlayerDead(Player);
        Destroy(this.gameObject);
        battleManager.CheckForWin(this);
    }

    public void deActiveBuild()
    {
        Destroy(WallImageShown);
        BuildEnable = false;
    }

    public void DisableDeathPowers()
    {
        DeathBox = false;
        powerIndicator.gameObject.SetActive(false);
    }


    void Start () {
        m_rigidbody = GetComponent<Rigidbody2D>();
        battleManager = FindObjectOfType<BattleManager>();
        powerIndicator = GetComponentInChildren<PowerIndicator>();
        powerIndicator.gameObject.SetActive(false);
        DestroySlider = GetComponentInChildren<Slider>();
        DestroySlider.gameObject.SetActive(false);
        BuildEnable = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float h = Input.GetAxis("Controller" + Player + "_L_Horizontal");
        float v = Input.GetAxis("Controller" + Player + "_L_Vertical");

        Vector2 MoveForceDirection = new Vector2(h, v).normalized;
        Vector2 MoveForce;
        if (DeathBox) { MoveForce = MoveForceDirection * (MoveMagnitude + DeathCubeIncreaseSpeed); }
        else { MoveForce = MoveForceDirection * MoveMagnitude; }

        m_rigidbody.AddForce(MoveForce);
        FindLookingDirection();
        if (DeathBox) { ChecktoDestroy(); }
        else { if (DestroySlider.gameObject.activeSelf) { DestroySlider.gameObject.SetActive(false); } }
            
        if (BuildEnable) { ChecktoBuild(); }


        if (boostAvailable)
        {
            if (Input.GetButtonDown("Controller" + Player + "_Dash"))
            {
                m_rigidbody.AddForce(MoveForceDirection * boostSpeed, ForceMode2D.Impulse);
                boostAvailable = false;
                lastBoostTime = Time.time;
            }
        }
        else
        {
            if (Time.time > lastBoostTime + BoostTimer)
            {
                boostAvailable = true;
            }
        }

    }

    private void ChecktoBuild()
    {
        if (Input.GetAxis("Controller" + Player + "_Build") != 0)
        {
            m_isAxisInUse = true;

        }
        else if (Input.GetAxis("Controller" + Player + "_Build") == 0)
        {
            m_isAxisInUse = false;
            BuildTrigger = true;
        }

        if (m_isAxisInUse && BuildTrigger)
        {
            if (!TileOn.CheckForTile(CurrentlyFacing))
            {
                GameObject tileWall = Instantiate(Wall, WallImageShown.transform.position, WallImageShown.transform.rotation);
                TileOn.Build(CurrentlyFacing, tileWall);
                BuildTrigger = false;
            }
        }
    }

    private void ChecktoDestroy()
    {
        if (Input.GetAxis("Controller" + Player + "_Destroy") != 0)
        {
            if (TileOn.CheckForTile(CurrentlyFacing))
            {
                DestroySlider.gameObject.SetActive(true);
                DestroySlider.value += Time.deltaTime * DestroySpeed;
                if (DestroySlider.value >= 1)
                {
                    TileOn.DestoryWall(CurrentlyFacing);
                    DestroySlider.value = 0;
                    DestroySlider.gameObject.SetActive(false);
                }
            }
            else
            {
                if (DestroySlider.gameObject != null)
                {
                    DestroySlider.value = 0;
                    DestroySlider.gameObject.SetActive(false);
                }
            }
        }
        else if (Input.GetAxis("Controller" + Player + "_Destroy") == 0)
        {
            if (DestroySlider.gameObject != null)
            {
                DestroySlider.value = 0;
                DestroySlider.gameObject.SetActive(false);
            }
        }
    }

    void FindLookingDirection()
    {
        float x = Input.GetAxis("Controller" + Player + "_R_Horizontal");
        float y = Input.GetAxis("Controller" + Player + "_R_Vertical");
        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            if (x > 0.05)
            {
                CurrentlyFacing = DirectionFacing.Right;
                BroadcastMessage("ChangeDirectionPosition");
            }
            else if (x < -0.05)
            {
                CurrentlyFacing = DirectionFacing.Left;
                BroadcastMessage("ChangeDirectionPosition");
            }
        }
        else if ((Mathf.Abs(x) < Mathf.Abs(y)))
        {
            if (y > 0.05)
            {
                CurrentlyFacing = DirectionFacing.Up;
                BroadcastMessage("ChangeDirectionPosition");
            }
            else if (y < -0.05)
            {
                CurrentlyFacing = DirectionFacing.Down;
                BroadcastMessage("ChangeDirectionPosition");
            }
        }

        if (WallImageShown != null )
        {
            PositionImage();
        }

    }







    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (DeathBox)
        {
            if (collision.gameObject.GetComponent<player>())
            {
                collision.gameObject.GetComponent<player>().Dead();
            }
        }

        if (collision.gameObject.GetComponent<DeathBlock>())
        {
            battleManager.StartDeathTimer(collision.gameObject.GetComponent<DeathBlock>().DeathTime);
            DeathBox = true;
            powerIndicator.gameObject.SetActive(true);
            Destroy(collision.gameObject);
        }

        Debug.Log("CollisionMade");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Tile>())
        {
            TileOn = collision.gameObject.GetComponent<Tile>();

            if (WallImageShown == null)
            {
                if (BuildEnable) { WallImageShown = Instantiate(WallImage, TileOn.LeftPosition, Quaternion.Euler(Vector3.zero)); }
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
