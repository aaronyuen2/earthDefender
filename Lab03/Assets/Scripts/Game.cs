using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public float gameTime;
    public float gameTimeHighscroe;
    public bool gameActive;

    public GameObject planetShield;
    public GameObject turret;

    public static Game g;

    private void Awake()
    {
        g = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
    }

    public void StartGame()
    {
        gameActive = true;
        gameTime = 0.0f;
        Player.pp.canMove = true;
    }

    public void EndGame()
    {
        gameActive = false;
        Player.pp.canMove = false;
        
        //Player.pp.
    }
}
