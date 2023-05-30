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
    PlayerController player;
    SpriteRenderer spRend;
    public float fadeInTime = 2f;
    public float fadeOutTime = 2f;

    private Coroutine fadeCoroutine;
    void Awake () {
        audioControl = FindObjectOfType<AudioController>();
        player = FindObjectOfType<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spRend = GetComponent<SpriteRenderer>();
        // Set sprite alpha value to 0
        Color col = spRend.color;
        col.a = 0f;
        spRend.color = col;
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.CompareTag("Player")) {
            animator.SetBool("Attack", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Light")) {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(FadeIn(spRend));
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Light")) {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(FadeOut(spRend));
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
    // public void FadeInCoroutine () {
    //     StopCoroutine(FadeOut(spRend));
    //     if (fadeIn != null) {
    //         StopCoroutine(FadeIn(spRend));
    //     }
    //     fadeIn = StartCoroutine(FadeIn(spRend));
    // } 
    // public void FadeOutCoroutine () {
    //     StopCoroutine(FadeIn(spRend));
    //     if (fadeOut != null) {
    //         StopCoroutine(FadeOut(spRend));
    //     }
    //     fadeOut = StartCoroutine(FadeOut(spRend));
    // } 
    public IEnumerator FadeIn(SpriteRenderer spRend)
    {
        // Get the current color of the image
        Color c = spRend.color;
        // Get the initial alpha value
        float startAlpha = c.a;
        // Initialize a variable to store the elapsed time
        float elapsedTime = 0f;
        // Loop until the elapsed time is greater than or equal to the fade duration
        while (elapsedTime < fadeInTime)
        {
            // Wait for the next frame
            yield return null;
            // Increase the elapsed time by the time since last frame
            elapsedTime += Time.deltaTime;
            // Calculate the new alpha value based on the elapsed time and fade duration
            c.a = Mathf.Lerp(startAlpha, 1f, elapsedTime / fadeInTime);
            // Assign the new color to the image
            spRend.color = c;
        }
    }
    public IEnumerator FadeOut(SpriteRenderer spRend)
    {
        // Get the current color of the image
        Color c = spRend.color;
        // Get the initial alpha value
        float startAlpha = c.a;
        // Initialize a variable to store the elapsed time
        float elapsedTime = 0f;
        // Loop until the elapsed time is greater than or equal to the fade duration
        while (elapsedTime < fadeOutTime)
        {
            // Wait for the next frame
            yield return null;
            // Increase the elapsed time by the time since last frame
            elapsedTime += Time.deltaTime;
            // Calculate the new alpha value based on the elapsed time and fade duration
            c.a = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeOutTime);
            // Assign the new color to the image
            spRend.color = c;
        }
    }
}
