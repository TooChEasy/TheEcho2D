using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    public Rigidbody2D rb;
    public Animator animator;
    // Vector2 previousPosition;
    Vector2 movement;
    AudioController audioControl;
    public int HealthPoints = 6;
    public float knockbackTime = 1f;
    public bool knockbacked; 
    void Awake () {
        audioControl = FindObjectOfType<AudioController>();
    }
    void Start () {
        // previousPosition = transform.position;
    }

    void Update()
    {   
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Vector2 current2DPosition = new Vector2(transform.position.x, transform.position.y);

        // if (current2DPosition != previousPosition) {
        //     if(!audioControl.isPlaying("PlayerRun")) {
        //         audioControl.Play("PlayerRun");
        //     } 
        // } else {
        //     if (audioControl.isPlaying("PlayerRun"))
        //     {
        //         audioControl.Stop("PlayerRun");
        //     }
        // }
        // previousPosition = transform.position;

        if (movement.x != 0 || movement.y != 0) {
             if(!audioControl.isPlaying("PlayerRun")) {
                audioControl.Play("PlayerRun");
            } 
        } else {
            audioControl.Stop("PlayerRun");
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void DealDamage () {
        if (!knockbacked) {
            audioControl.Play("PlayerHit");
            Debug.Log("Damaged");
            HealthPoints -= 1;
            StartCoroutine(Knocked());
        }
    }
    private IEnumerator Knocked()
    {
        yield return new WaitForSeconds(knockbackTime);
        knockbacked = false;
    }
}
