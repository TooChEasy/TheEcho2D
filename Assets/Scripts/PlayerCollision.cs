using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    private PlayerMovement playerObject;

    void Start() {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerObject = gameObject.GetComponent<PlayerMovement>();
    }
    void OnCollisionEnter2D(Collision2D collision) {
        // Check if the player collided with the collider
        if (collision.gameObject.CompareTag("Environment")) {
            // Get the direction of the collision
            Vector2 direction = collision.contacts[0].point - (Vector2)transform.position;
            // Prevent the player from moving in the direction of the collision
            playerRigidbody.AddForce(direction * 1000, ForceMode2D.Impulse);
        }
         if (collision.gameObject.CompareTag("Enemy")) {
            Debug.Log("collided");
            playerObject.DealDamage();
            // Get the direction of the collision
            // Prevent the player from moving in the direction of the collision
            // playerRigidbody.AddForce(direction * 100000, ForceMode2D.Impulse);
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            Debug.Log(direction);
            Debug.DrawRay(transform.position, direction, Color.red);
            playerRigidbody.velocity = direction * 100;
        }
    }
}
