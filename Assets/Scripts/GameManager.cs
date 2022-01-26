using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    mainMenu,
    inGame,
    gameOver,
    missionComplete,
}

public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    public void StartGame() //Method to start the game
    {
        SetGameState(GameState.inGame);
        
    }

    public void GameOver()  //Method called when Player dies
    {
        SetGameState(GameState.gameOver);
        
    }

    public void BackToMenu()    //Method to back to main menu
    {
        SetGameState(GameState.mainMenu);
        
    }

    public void MissionComplete()
    {
        SetGameState(GameState.missionComplete);

    }

    void SetGameState(GameState newGameState) //Main method to manage game states START - GAME OVER - BACK TO MENU
    {
        if(newGameState == GameState.mainMenu)
        {

        }else if(newGameState == GameState.inGame)
        {

        }else if(newGameState == GameState.gameOver)
        {
            
        }
        this.currentGameState = newGameState;
    }
    
}
