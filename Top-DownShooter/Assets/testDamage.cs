using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerHealth.TakeDamage(10);
        }
    }
}
