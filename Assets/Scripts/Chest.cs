using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Sprite chestOpen;
    public GameObject Star;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player")) //Collision which activates the chest and open it
        {
            Star.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().sprite = chestOpen;
        }
    }
}
