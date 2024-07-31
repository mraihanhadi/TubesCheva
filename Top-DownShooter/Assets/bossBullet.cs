using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBullet : MonoBehaviour
{
    public float damage = 5f;
    public GameObject hitEffect;
    void Start()
    {
        manager enemyStats = FindObjectOfType<manager>();
        damage = enemyStats.bossDamage;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Musuh" && collision.gameObject.tag != "laser" && collision.gameObject.tag != "Camera" && collision.gameObject.tag != "MainCamera" && collision.gameObject.tag != "Item")
        {
            if(collision.gameObject.tag == "Player")
            {
                PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }  
            }
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect,0.85f);
            Destroy(gameObject);
        }
    }
}
