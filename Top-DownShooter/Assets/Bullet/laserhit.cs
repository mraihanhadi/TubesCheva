using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHit : MonoBehaviour
{
    public GameObject hitEffect;
    public AudioSource hitSfx;
    private float damage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "laser" && collision.gameObject.tag != "Camera" && collision.gameObject.tag != "MainCamera" && collision.gameObject.tag != "Item")
        {
            if (collision.gameObject.tag == "Musuh")
            {
                EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    GameObject player = GameObject.FindWithTag("Player");
                    if (player != null)
                    {
                        playerStats stats = player.GetComponent<playerStats>();
                        if (stats != null)
                        {
                            damage = stats.damage;
                            enemy.TakeDamage(damage);
                        }
                    }
                }
            }
            hitSfx = gameObject.GetComponent<AudioSource>();
            hitSfx.Play();
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.85f);
            Destroy(gameObject);
        }
    }
}
