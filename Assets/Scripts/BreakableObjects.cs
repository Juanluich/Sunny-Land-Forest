using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObjects : MonoBehaviour
{
    public Animator boxAnim;
    public GameObject carrot;
    public Player Player;
    public AudioSource carrotSound;
    public AudioSource stoneSlide;
    
    private void Start()
    {
        boxAnim = GetComponent<Animator>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stoneSlide.Play(); //If player touches the stone box sounds stone sliding
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) //When box falls it breaks and drop one carrot 
    {
        if (collision.CompareTag("Wall"))
        {
            Player.lifes++;                         //(extra life instantly)
            boxAnim.SetTrigger("isBreaking");       //Breaking animation
            Invoke(nameof(Destroy), 1);             //little delay to give animation plays entire
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            carrot.gameObject.SetActive(true);
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            carrotSound.Play();
        }
    }
    void Destroy()
    {
        Destroy(gameObject);
    }

}
