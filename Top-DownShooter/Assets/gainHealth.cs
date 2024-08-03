using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gainHealth : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(despawn());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealth playerHP = collision.GetComponent<PlayerHealth>();
            if (playerHP != null)
            {
                playerHP.GainHealth(25);
            }
            Destroy(gameObject);
        }
    }
    IEnumerator despawn()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
