using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    #region VARIABLES
    //ENEMY STATES
    public enum EnemyState { Patrol, Pursuit, Attack }
    public EnemyState enemyAction;

    public Player Player;

    [SerializeField] private float enemySpeed = 1.5f;
    public int enemyLifes = 1;

    [Header("Physics")]
    Rigidbody2D rbEnemy;

    [Header("Animators")]
    public Animator beeFloatAnim;
    public Animator beeAnim;
    public Animator plantAnim;
    public Animator snailAnim;

    [Header("Positions")]
    private Vector2 enemyPosition;
    private Vector2 playerPosition;
    private float distance;
    #endregion

    void Start()
    {
        rbEnemy = GetComponent<Rigidbody2D>();
        plantAnim = GetComponent<Animator>();
        beeAnim = GetComponent<Animator>();
        snailAnim = GetComponent<Animator>();
    }

    void Update()
    {
        //Take enemy and player position constantly updated to know them
        enemyPosition = this.gameObject.transform.position;
        playerPosition = Player.transform.position;
        distance = Vector2.Distance(enemyPosition, playerPosition);

        //We switch between different enemy status where we can choose one for our differents enemy behaviours
        switch (enemyAction){
            case EnemyState.Patrol: Patrol(); snailDeath(); break;
            case EnemyState.Pursuit: Patrol(); Pursuit();LookAtPlayer();beeDeath(); break;
            case EnemyState.Attack: PlantAttack();LookAtPlayer(); plantDeath(); break;
        }
    }

    #region COLLISIONS
    private void OnTriggerExit2D(Collider2D collision) //Flip Enemy when detect no ground in front
    {
        if(collision.gameObject.tag == "Ground"){
            enemySpeed = -enemySpeed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) //This method change enemy direction when collide with walls
    {
        if(collision.gameObject.tag == "Wall"){
            enemySpeed = -enemySpeed;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) //Flip enemy direction when collides with wall or another enemy
    {
        if (collision.gameObject.tag == "Wall"){
            enemySpeed = -enemySpeed;
        }
        if (collision.gameObject.tag == "Enemy"){
            enemySpeed = -enemySpeed;
        }
    }
    #endregion

    #region ENEMY BEHAVIOURS AND STATES
    //Patrol methods
    void Patrol()
    {
        rbEnemy.velocity = new Vector2(-enemySpeed, rbEnemy.velocity.y);
        if (rbEnemy.velocity.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else { gameObject.GetComponent<SpriteRenderer>().flipX = true; }
    }

    //Set enemy lookint at player when he is close
    void LookAtPlayer()
    {
        if ((enemyPosition.x > playerPosition.x) && (distance < 1.5)){  
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if((enemyPosition.x < playerPosition.x) && (distance < 1.5)){
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
    }

    //Plant Attack behaviour
    void PlantAttack()
    {
        if (distance < 0.5){    //If player is closer than 0.5 in x plays attack animation and move his box collider
            plantAnim.SetBool("isAttacking",true);
            if(enemyPosition.x < playerPosition.x){
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0.1f, -0.104723f);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.36f, 0.2405539f);
                 }
            if (enemyPosition.x > playerPosition.x){
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.1f, -0.104723f);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.36f, 0.2405539f);
            }
        }
        else { plantAnim.SetBool("isAttacking", false);
               gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.01414371f, -0.104723f);
               gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.2014041f, 0.2405539f);
        }
    }

    //Pursuit when enemy moves close around the player
    void Pursuit()
    {
        //When enemy is close around the player set a new vector2 in enemy position toward current player position
        if ((distance < 1.5) && Player.ducking == false)
        {   //Only follow the player if he is facing him
            if(Player.transform.position.x < transform.position.x && gameObject.GetComponent<SpriteRenderer>().flipX == false)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, enemySpeed * Time.deltaTime * 3f);
                beeFloatAnim.enabled = false;
            }
            if (Player.transform.position.x > transform.position.x && gameObject.GetComponent<SpriteRenderer>().flipX == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, enemySpeed * Time.deltaTime * 3f);
                beeFloatAnim.enabled = false;
            }
        }
        else { beeFloatAnim.enabled = true; }  
    }

    //Plant animation when dies
    void plantDeath()
    {
        if (enemyLifes == 0){
            plantAnim.SetTrigger("plantDeath");
            Invoke(nameof(enemyDeath), .6f);
        }
    }

    //Bee animation when dies
    void beeDeath()
    {
        if (enemyLifes == 0){
            enemySpeed = 0;
            rbEnemy.constraints = RigidbodyConstraints2D.FreezeAll;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            beeAnim.SetTrigger("beeDeath");
            beeFloatAnim.enabled = false;
            Invoke(nameof(enemyDeath), .6f);
        }
    }

    //Snail animation when dies
    void snailDeath()
    {
        if (enemyLifes == 0){
            enemySpeed = 0;
            rbEnemy.constraints = RigidbodyConstraints2D.FreezeAll;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            snailAnim.SetTrigger("snailDeath");
            Invoke(nameof(enemyDeath), .6f);
        }
    }

    //Enemy method called later to give animation plays entire
    void enemyDeath()
    {
        Destroy(gameObject);
    }

    #endregion



}
