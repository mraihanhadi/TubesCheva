using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public Rigidbody2D rb ;
    public Animator animate;
    public Transform firePoint;
    private SpriteRenderer spriteRenderer;

    Vector2 movement;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animate.SetFloat("Speed",movement.magnitude);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
            firePoint.localPosition = new Vector3(-Mathf.Abs(0.0669f), 0.0303f, firePoint.localPosition.z);
        }
        else
        {
            spriteRenderer.flipX = false;
             firePoint.localPosition = new Vector3(Mathf.Abs(0.0669f), 0.0303f, firePoint.localPosition.z);
        }
    }

    void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement.normalized * movementSpeed * Time.fixedDeltaTime);
        
    }
}
