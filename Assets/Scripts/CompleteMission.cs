using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CompleteMission : MonoBehaviour
{
    public ScoreManager score;
    public GameObject missionCompleted;
    public GameObject missionIncomplete;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("House")) //If we reach the end of the level we can:
        {
            if (score.totalStars == 0)  //Win and end game cause we got all the stars
            {
                MissionComplete();
            }
            else
            {
                missionIncomplete.SetActive(true); //Active text telling us we need to take them all
                Invoke(nameof(MIDesactivated), 3);
            }
        }
    }
    void MissionComplete()
    {
        missionCompleted.SetActive(true);
        Invoke(nameof(EndGame), 4);
    }
    void MIDesactivated()
    {
        missionIncomplete.SetActive(false); 
    }
    void EndGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
