using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xpItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerxp playerXp = collision.GetComponent<playerxp>();
            if (playerXp != null)
            {
                playerXp.ActivateDoubleXp();
            }
            Destroy(gameObject);
        }
    }
}
