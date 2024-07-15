using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserhit : MonoBehaviour
{
    public float damage = 5f;
    public GameObject hitEffect;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Player" && collision.gameObject.tag != "laser")
        {
            if(collision.gameObject.tag == "Musuh")
            {
                EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }

            }
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect,0.85f);
            Destroy(gameObject);
        }
    }
}
