using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject[] bulletImpactEffects; // Array to hold the bullet impact prefabs
    public float impactEffectDuration = 0.0001f;  // Duration before the impact effect is destroyed

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Log the name of the object we're colliding with
        Debug.Log("Bullet collided with: " + collision.gameObject.name);

        // Check if the collision is with an object named "obstacle_car"
        if (collision.gameObject.name == "obstacle_car")
        {
            // Get the contact point of the collision
            Vector2 contactPoint = collision.contacts[0].point;

            // Choose a random bullet impact effect
            int randomIndex = Random.Range(0, bulletImpactEffects.Length);
            GameObject impactEffect = bulletImpactEffects[randomIndex];

            // Log the selected bullet impact effect
            Debug.Log("Selected bullet impact effect: " + impactEffect.name);

            // Instantiate the bullet impact effect at the contact point
            GameObject instantiatedEffect = Instantiate(impactEffect, contactPoint, Quaternion.identity);

            // Destroy the instantiated impact effect after a delay
            Destroy(instantiatedEffect, impactEffectDuration);

            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
