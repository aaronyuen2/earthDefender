using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    // public variables
    public int health;
    public GameObject deathParticleEffect;
    public SpriteRenderer sr;
    public static Planet p;

    // Start is called before the first frame update
    private void Awake()
    {
        p = this;
    }

    public void TakeDamage(int dmg)
    {
        // if the health is less than or equal to 0, then end game
        if (health - dmg <= 0)
        {
            // end game

            // create the explosion particle effect
            GameObject pe = Instantiate(deathParticleEffect, transform.position,
                Quaternion.identity);
            Destroy(pe, 2.0f);  // detroy pe after 2 seconds
        } else
        {
            health -= dmg;
        }

        // shake camera
        CameraController.c.Shake(0.3f, 0.5f, 50.0f);
        // flash white

        // update ui

    }
}
