using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    
    public Text starsCollected;

    public int totalStars;

    void Start()
    {
        totalStars = transform.childCount;  //Count the numbers of stars as stars to recollect
    }

    void Update()
    {
        totalStars = transform.childCount;  //Count the numbers of stars as stars to recollect
        starsCollected.text = totalStars.ToString();    //Convert int number of total stars to string for canvas hud

        
    }

    
}
