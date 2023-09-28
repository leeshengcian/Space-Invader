using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // graffiti
    public Text uiText;

    //states
    [HideInInspector]
    public enum State { NotStarted, Playing_Lv1, Won_Lv1, Playing_Lv2, GameOver, WonGame }

    // current state
    State currState;

    // Enemy Manager
    EnemyManager enemyManager;
    // Start is called before the first frame update
    void Start()
    {
        // start as not playing
        currState = State.NotStarted;

        // refresh UI
        RefreshUI();

        // find the enemy manager
        enemyManager = GameObject.FindObjectOfType<EnemyManager>();

        // log error if it wasn't found
        if(enemyManager == null)
        {
            Debug.LogError("there needs to be an EnemyManager in the scene");
        }
    }

    void RefreshUI()
    {
        // act according to the state
        switch(currState)
        {
            case State.NotStarted:
                uiText.text = "Shoot here to begin";
                break;

            case State.Playing_Lv1:
            case State.Playing_Lv2:
                uiText.text = "Enemies left: " + enemyManager.numEnemies;
                break;

            case State.GameOver:
                uiText.text = "Game Over! Shoot here";
                break;

            case State.Won_Lv1:
                uiText.text = "Level 2 get ready";
                break;
            case State.WonGame:
                uiText.text = "YOU WON! Shoot here";
                break;
        }  
    }

    public void InitGame()
    {
        
        /*if (currState == State.Playing_Lv1) return;
        currState = State.Playing_Lv1;*/

        // set the state
        switch (currState)
        {
            case State.NotStarted:
                currState = State.Playing_Lv1;
                break;
            // don't initiate the game if the game is already running!
            case State.Playing_Lv1:
                return;
            case State.Won_Lv1:
                currState = State.Playing_Lv2;
                break;
            case State.Playing_Lv2:
                return;
        }

        // create enemy wave
        enemyManager.CreateEnemyWave(currState);

        // show text on the graffiti
        RefreshUI();
    }

    // game over
    public void GameOver()
    {
        // do nothing if we were already on game over
        if (currState == State.GameOver) return;

        // set the state to game over
        currState = State.GameOver;

        // show text on the graffiti
        RefreshUI();

        // remove all enemies
        enemyManager.KillAll();
    }

    // checks whether we've won, and if we did win, refresh UI
    public void HandleEnemyDead()
    {
        if (currState != State.Playing_Lv1 && currState != State.Playing_Lv2) return;

        RefreshUI();

        // have we won the game?
        if(enemyManager.numEnemies <= 0)
        {
            // set the state of the game
            if (currState == State.Playing_Lv1)
            {
                currState = State.Won_Lv1;
            }
            else if (currState == State.Playing_Lv2)
            {
                currState = State.WonGame;
            }

            // show text on the graffiti
            RefreshUI();

            // remove all enemies
            enemyManager.KillAll();
        }
    }
}
