using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    AudioController audioControl;
    Health healthControl;

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    public int HealthPoints = 6;
    public float knockbackTime = 1f;
    public bool knockbacked; 
    private bool noDmg = false;
    void Awake () {
        audioControl = FindObjectOfType<AudioController>();
        gameManager = FindObjectOfType<GameManager>();
        healthControl =  FindObjectOfType<Health>();
    }
    void Start () {
        // previousPosition = transform.position;
    }

    void Update()
    {   
       gatherInput();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void gatherInput() {
        if (!gameManager.isPaused && isAlive()) {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (movement.x != 0 || movement.y != 0) {
                if(!audioControl.isPlaying("PlayerRun")) {
                    audioControl.Play("PlayerRun");
                } 
            } else {
                audioControl.Stop("PlayerRun");
            }
            if (Input.GetKeyDown(KeyCode.I)) {
                noDmg = !noDmg;
            }
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }
    public void DealDamage () {
        if (!knockbacked && !noDmg) {
            audioControl.Play("PlayerHit");
            HealthPoints -= 1;
            healthControl.UpdateHealth(HealthPoints);
            StartCoroutine(Knocked());
        }
    }
    public bool isAlive () {
        return HealthPoints > 0;
    }
    private IEnumerator Knocked()
    {
        yield return new WaitForSeconds(knockbackTime);
        knockbacked = false;
    }
}
