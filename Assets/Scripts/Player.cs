using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region VARIABLES
    //Status vars
    [Header("Player Life")]
    public int lifes = 3;

    //Speed Variables to manage movement and jump velocity
    [Header("Speed & Jump")]
    public float playerSpeed = 1;
    public float jumpSpeed = 3;
    public bool betterJump;
    public float fallMultiplier = 0.5f;
    public float lowJumpMultiplier = 2f;

    //Rigidbody to work with physics
    [Header("Physics")]
    Rigidbody2D rb;
    public bool ducking;

    //Boxcast to check ground
    [Header("Check Ground")]
    [SerializeField] private LayerMask jumpableGround;
    private BoxCollider2D coll; //To check ground

    //Animation vars
    [Header("Animations")]
    public SpriteRenderer spriteRenderer;
    public Animator playerAnim;

    //Enemy Collisions Vars
    [Header("VS Enemy Conditions")]
    public int push = 2;
    public bool invul;
    public Enemies Enemy;
    

    //Climb ladders
    public bool usingLadder = false;

    //SFX
    [Header("SFX")]
    public AudioSource jumpSound;
    public AudioSource jumpRebound;
    public AudioSource hurtSound;

    //Tutorial
    [Header("Tutorial")]
    public GameObject learnToKill; //Player dialogs with himself to learn kill enemies and duck
    public GameObject learnToDuck;

    //To fade in between scenes  
    [Header("GameOver")]
    public GameObject GameOverPanel;
    #endregion

    void Start()
    {
        //Add components with code
        rb = GetComponent<Rigidbody2D>(); 
        playerAnim = GetComponent<Animator>(); 
        Enemy = GetComponent<Enemies>();
        coll = GetComponent<BoxCollider2D>();
    }
    void FixedUpdate() //FixedUpdate because we are going to work with physics
    {
        PlayerMovement();
        BetterJump();
        GameOver();
        Falling();
        Jump();
        Ducking();  
    }

    #region COLLISIONS
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (invul == false) { lifes--; invul = true; }
            Invoke(nameof(EndInvul), 1.25f);
            playerAnim.SetTrigger("isHurting");
            if (collision.transform.position.x > transform.position.x)
            {
                hurtSound.Play();
                rb.AddForce(new Vector2(-3, 0), ForceMode2D.Impulse);
            }
            else
            {
                hurtSound.Play();
                rb.AddForce(new Vector2(3, 0), ForceMode2D.Impulse);
            }
        }
        if (collision.gameObject.CompareTag("Spikes"))
        {
            if (invul == false) {
                jumpRebound.Play();
                lifes--; invul = true;
                Invoke(nameof(EndInvul), 1.25f);
                playerAnim.SetTrigger("isHurting");
                rb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) //We add a bound when jump over an enemy
    {
        if (collision.transform.CompareTag("Weak"))
        {
            jumpRebound.Play();
            rb.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
        }
        if (collision.transform.CompareTag("Learn1")) //Show dialog learn to jump over enemies
        {
            LearnToKill();
        }
        if (collision.transform.CompareTag("Learn2")) //Show dialog learn to duck
        {
            LearnToDuck();
        }
    }
    #endregion

    #region PLAYER MECHANICS
    void PlayerMovement()
    {
        if(ducking == false) //Can't move while ducking
        { 
            //Player Movement
            if (Input.GetKey("d") || Input.GetKey("right"))
            {
                playerAnim.SetBool("isRunning", true);
                rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
                spriteRenderer.flipX = false;
                
            }
            else if (Input.GetKey("a") || Input.GetKey("left"))
            {
                playerAnim.SetBool("isRunning", true);
                rb.velocity = new Vector2(-playerSpeed, rb.velocity.y);
                spriteRenderer.flipX = true;    //Flip default sprite with animations when player moves to the left
                
            }
            else
            {
                playerAnim.SetBool("isRunning", false);
                rb.velocity = new Vector2(0, rb.velocity.y); //Player stays if we dont press anything
            }
        }
        
    }
    void Jump()
    {
        if(ducking == false) //Can't jump while you are ducking
        {
            //Check if player is touching platforms
            if (Input.GetKey("space") && IsGrounded())
            {
                jumpSound.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                playerAnim.SetBool("isJumping", true);
            }
            else { playerAnim.SetBool("isJumping", false); }

            if (!usingLadder)
            {
                playerAnim.SetFloat("vSpeed", rb.velocity.y);
            }
        }
        
    }
    void BetterJump()
    {
        if (ducking == false) //Can't jump while you are ducking
        {
            if (betterJump && IsGrounded())
            {
                if (rb.velocity.y < 0)
                {
                    rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
                }
                if (rb.velocity.y > 0 && !Input.GetKey("space"))
                {
                    rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
                }
            }
        }  
    }
    void Ducking()
    {
        if (Input.GetKey("left shift")) {
            if(rb.velocity.x == 0)  //We can't duck if we are moving
            {
                playerAnim.SetBool("isDucking", true);
                ducking = true;
            }
        }
        else { playerAnim.SetBool("isDucking", false); ducking = false; }
    }
    void Falling()
    {
        if(rb.velocity.y < 0 && !IsGrounded()) { playerAnim.SetBool("isFalling",true); }
        else { playerAnim.SetBool("isFalling", false); }
    }
    #endregion

    #region CHECKGROUND
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    #endregion

    #region INVULNERABILITY
    void EndInvul()
    {
        invul = false;
    }
    #endregion

    #region GAMEOVER
    void GameOver()
    {
        if (lifes == 0) { this.gameObject.SetActive(false); Debug.Log("Game Over"); 
            GameOverPanel.SetActive(true);
            Invoke(nameof(BackToTitle), 4);
        }
    }
    void BackToTitle()
    {
        SceneManager.LoadScene("MainMenu");

    }
    #endregion

    #region TUTORIAL
    //Learning mechanics
    void LearnToKill()
    {
        learnToKill.SetActive(true);
        Invoke(nameof(DisableLearns), 3);
    }
    void LearnToDuck()
    {
        learnToDuck.SetActive(true);
        Invoke(nameof(DisableLearns), 3);
    }
    void DisableLearns()
    {
        learnToKill.SetActive(false);
        learnToDuck.SetActive(false);
    }
    #endregion

}