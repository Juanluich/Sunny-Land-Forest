using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Player Player;
    
    public Sprite[] spriteLifes;
    
    void Update()
    {

        //Set hud sprites related to 
        if (Player.lifes == 3 || Player.lifes == 4){
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteLifes[3];
        }
        else if (Player.lifes == 2){
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteLifes[2];
        }
        else if (Player.lifes == 1){
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteLifes[1];
        }
        else{
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteLifes[0];
        }
    }
}
