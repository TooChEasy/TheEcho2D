using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public AIPath aiPath;
    public Animator animator;
    public Rigidbody2D rb;
    AudioController audioControl;
    PlayerMovement player;
    void Awake () {
        audioControl = FindObjectOfType<AudioController>();
        player = FindObjectOfType<PlayerMovement>();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.CompareTag("Player")) {
            animator.SetBool("Attack", true);
        }
    }

    void Update()
    {
        if(aiPath.velocity.x != 0 || aiPath.velocity.y != 0) {
            animator.SetFloat("Speed", 1f); 
            if(aiPath.desiredVelocity.x >= 0.01f) {
                animator.SetFloat("Horizontal", 1f);
            }
            if(aiPath.desiredVelocity.x < 0.01f) {
                animator.SetFloat("Horizontal", -1f);
            }
            if(aiPath.desiredVelocity.y >= 0.01f) {
                animator.SetFloat("Vertical", 1f);
            }
            if(aiPath.desiredVelocity.y < 0.01f) {
                animator.SetFloat("Vertical", -1f);
            }
        } else {
            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", 0f);
            animator.SetFloat("Speed", 0f); 
        }
       
    }
    public void StartAnimation () {
        // audioControl.Play("EnemyAttack");
        // player.DealDamage();
    }
    public void EndAnimation () {
        // player.DealDamage();
        animator.SetBool("Attack", false);
    }
   
}
