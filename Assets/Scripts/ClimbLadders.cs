using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbLadders : MonoBehaviour
{
    //Climb ladders
    public BoxCollider2D platformGround;

    Rigidbody2D rbClimb;
    Animator playerAnimClimb;
    
    public bool onLadder = false;
    public float climbSpeed;
    public float exitHop = 3f;

    public Player Player;

    private void Start()
    {
        rbClimb = GetComponent<Rigidbody2D>();
        playerAnimClimb = GetComponent<Animator>();
        Player = GetComponent<Player>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("ladder")) //If player is staying in front of the ladder
        {
            if (Input.GetAxisRaw("Vertical") != 0) //We press up or down
            {
                rbClimb.velocity = new Vector2(rbClimb.velocity.x, Input.GetAxisRaw("Vertical") * climbSpeed); //Move upward or downward with climb animation
                rbClimb.gravityScale = 0;
                onLadder = true;
                platformGround.enabled = false;
                Player.usingLadder = onLadder;
            }
            else if (Input.GetAxisRaw("Vertical") == 0 && onLadder) //while player is not going upward or backward and he is on ladder set velocity y on 0
            {
                rbClimb.velocity = new Vector2(rbClimb.velocity.x, 0);
            }

            playerAnimClimb.SetBool("isClimbing", onLadder);
            playerAnimClimb.SetFloat("vSpeed", Mathf.Abs(Input.GetAxisRaw("Vertical")));
        }
    }

    private void OnTriggerExit2D(Collider2D other) //Put the player above the platform and reset gravity to walk around
    {
        if (other.CompareTag("ladder") && onLadder)
        {
            rbClimb.gravityScale = 1;
            onLadder = false;
            Player.usingLadder = onLadder;
            platformGround.enabled = true;
            playerAnimClimb.SetBool("isClimbing", onLadder);

            
        }
    }
}
