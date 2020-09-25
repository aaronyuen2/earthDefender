using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public GameObject hitParticleEffect;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2.0f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // if the bullet hits an enemy
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            SpawnParticleEffect();
            Destroy(gameObject);   // destroy bullet
            
        }

        // if the bullet hits a shield
        if (col.gameObject.tag == "Shield")
        {
            //col.gameObject.GetComponent<Enemy>().TakeDamage(damage);

        }

        // if the bullet hits a pickup
        if (col.gameObject.tag == "Pickup")
        {
            //col.gameObject.GetComponent<Enemy>().TakeDamage(damage);

        }

        // shake camera
        CameraController.c.Shake(0.1f, 0.25f, 30.0f);

    }

    void SpawnParticleEffect()
    {
        GameObject pe = Instantiate(hitParticleEffect, transform.position, Quaternion.identity);
        pe.transform.LookAt(Planet.p.transform);
        Destroy(pe, 2.0f);
    }
}
