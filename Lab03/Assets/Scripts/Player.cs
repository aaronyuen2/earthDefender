using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public varialbes
    public float moveSpeed;
    public float attackRate;
    private float attackTimer;
    public float bulletSpeed;
    public float bulletSpread = 1.0f;
    public Transform playerSprite;

    // boolean variables to control start/stop game & initialise game
    public bool canMove;
    public bool canHoldFire;  // hold down space to fire continuously?

    // prefab variables
    public GameObject bulletPrefab;

    // static variable for the player
    public static Player pp;

    // Happy Fun Times multiplayer
    HFTInput hInput;
    HFTGamepad hGamepad;
    public SpriteRenderer renderer;


    // Start is called before the first frame update
    void Awake()
    {
        pp = this;  // assign this player instance to pp static variable
    }

    private void Start()
    {
        hInput = GetComponent<HFTInput>();
        hGamepad = GetComponent<HFTGamepad>();

        //renderer = GetComponent<Renderer>();
        renderer.material.color = hGamepad.color;

    }

    // Update is called once per frame
    void Update()
    {
        // can move
        if (canMove)
        {
            RotatePlayer();
        }

        // no hold fire
        // if (!canHoldFire && Input.GetKeyDown(KeyCode.Space) && canMove)
        if (!canHoldFire && hInput.GetButtonDown("fire1") && canMove)
        {
            if (attackTimer > attackRate)
            {
                attackTimer = 0.0f;
                Shoot();
            }
        }

        // can hold fire

        attackTimer += Time.deltaTime;
    }

    // custom functions
    void RotatePlayer()
    {
        transform.eulerAngles += new Vector3(0,
                                             0,
            (-moveSpeed * hInput.GetAxis("Horizontal")) * Time.deltaTime);
       // (-moveSpeed * Input.GetAxis("Horizontal")) * Time.deltaTime);

        playerSprite.localEulerAngles = new Vector3(0, 0,
            hInput.GetAxis("Horizontal") * -30);
       // Input.GetAxis("Horizontal") * -30);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, playerSprite.transform.position,
            transform.rotation);

        Vector2 dir = playerSprite.transform.position.normalized *
            (bulletSpeed * Random.Range(1.0f, 1.1f));

        Vector3 offset = bullet.transform.right * Random.Range(-bulletSpread, bulletSpread);

        dir.x += offset.x;
        dir.y += offset.y;

        bullet.GetComponent<Rigidbody2D>().velocity = dir;

        // can hold fire by holding space bar

        // audio manager play shooting sfx
    }

    // effects
    // allows the player to hold down the fire button
    public void ActivateSpeedFire()
    {
        if (!canHoldFire)
            StartCoroutine(SpeedFireTimer());
    }

    // thread
    IEnumerator SpeedFireTimer()
    {
        canHoldFire = true;
        yield return new WaitForSeconds(5.0f);
        canHoldFire = false;
    }
}
