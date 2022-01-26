using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class StarsCollected : MonoBehaviour
{
    public AudioSource collectedSound;  //Audio var asigned from inspector which it plays when we collect stars

    //Destroy Stars prefab which will be counted by Score Manager
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collectedSound.Play();
            Invoke(nameof(DestroyDelayed), .5f); //How star sound doesn't play before game object is destroyed
            
            gameObject.GetComponent<SpriteRenderer>().enabled = false; //Disable sprite and collider to don't repeat sound
            gameObject.GetComponent<BoxCollider2D>().enabled = false; //cause it is invisible but it can be collided
        }
    }

    void DestroyDelayed() //We delayed a little bit a method who first make it disabled then destroy it
    {
        Destroy(this.gameObject);
    }
}
