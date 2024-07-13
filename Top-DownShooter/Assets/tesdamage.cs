using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tesDamage : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            enemyHealth.TakeDamage(10);
        }
    }
}
