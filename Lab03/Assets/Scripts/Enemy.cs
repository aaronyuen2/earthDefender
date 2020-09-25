using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Red, Yellow, Blue }

public class Enemy : MonoBehaviour
{
    public int health;
    public float moveSpeed;
    public int damage;
    public bool stunned;
    public EnemyType type;

    // prefabs
    public GameObject deathParticleEffect;
    public GameObject duplicatePrefab;

    // components
    public SpriteRenderer sr;




    // Start is called before the first frame update
    void Start()
    {
        // if the enmey is yellow, randmly rearrange the 2nd shield
        if (type == EnemyType.Yellow)
        {
            GameObject shield2 = transform.Find("Shield2").gameObject;
            int ranNum = Random.Range(1, 5);

            if (ranNum == 1)
                shield2.transform.localEulerAngles = new Vector3(0, 0, 89);
            if (ranNum == 2)
                shield2.transform.localEulerAngles = new Vector3(0, 0, -180);
            if (ranNum >= 3)
                shield2.SetActive(false);
        }

        // add a little variation to the move speed
        moveSpeed *= Random.Range(0.9f, 1.1f);
        
    }

    // Update is called once per frame
    void Update()
    {
        // move to earth
        if (!stunned)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                Vector3.zero, moveSpeed * Time.deltaTime);

            
        }

        LookAtPlanet();
    }

    void LookAtPlanet()
    {
        Vector3 dir = transform.position.normalized;
        float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        transform.eulerAngles = new Vector3(0, 0, ang);
    }

    public void TakeDamage(int dmg)
    {
        // if the  health is less than or equals to 0, then die
        if (health - dmg <= 0)
        {
            Die();
        } else
        {
            health -= dmg;
            // stun

            // play audio effect
        }

        // flash the enemy sprite

    }

    public void Die()
    {
        GameObject pe = Instantiate(deathParticleEffect, transform.position,
            Quaternion.identity);
        Destroy(pe, 2.0f);

        if (type == EnemyType.Blue)
        {
            Duplicate();
        }

        // play audio effect

        // destory the enemy
        Destroy(gameObject);
    }

    void Duplicate()
    {
        GameObject e1 = Instantiate(duplicatePrefab, transform.position +
            (transform.up * -2), Quaternion.identity);

        GameObject e2 = Instantiate(duplicatePrefab, transform.position +
            (transform.right * 2), Quaternion.identity);

        GameObject e3 = Instantiate(duplicatePrefab, transform.position +
            (transform.right * -2), Quaternion.identity);
    }

    // hit the earth
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Earth")
        {
            // damage earth hp
            Planet.p.TakeDamage(damage);
            Die();
        }
    }
}
