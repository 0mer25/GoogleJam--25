using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public int lives = 3;
    static public int score;
    public int addLevelScore = 0;
    public Text livesText;
    public Text scoreText;
    public bool arcadeGameOver;
    public int brickNumber = 0;
    public R_LevelManager levelManager;
    //public Brick brick;
    int nextScene;

    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
        brickNumber = GameObject.FindGameObjectsWithTag("brick").Length; // Found in unity documentationAA
        //Debug.Log("number of bricks at start: " + brickNumber);
        nextScene = SceneManager.GetActiveScene().buildIndex + 1; // Next scene load
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateLives(int changeLives)
    {
        lives += changeLives;

        if (lives <= 0)
        {
            lives = 0;
            Debug.Log("Game Over");
            ArcadeGameOver();
        }

        livesText.text = "Lives: " + lives;
    }

    private void ArcadeGameOver() // Checks arcade game and loads my arcade game over scene
    {
        Debug.Log("Game Over");
        arcadeGameOver = true;
        //SceneManager.LoadScene("ArcadeGameOverLevel");
        levelManager.GameOver();
    }

    public void UpdateScore(int points) // Updates my score
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void BrickCounter()
    {
        brickNumber--;
        int brokenBricks = 0;
        brokenBricks++;
        //Debug.Log(brickNumber + "brick(s) left");
        //Debug.Log(brokenBricks + " brick(s) broken");

        if (brickNumber <= 0)
        {
            if (SceneManager.GetActiveScene().name == ("R_BrickGame_L1"))
            {
                SceneManager.LoadScene("R_BrickGame_L2");
            }
            else if (SceneManager.GetActiveScene().name == ("R_BrickGame_L2"))
            {
                SceneManager.LoadScene("R_BrickGame_L3");
            }
            else if (SceneManager.GetActiveScene().name == ("R_BrickGame_L3"))
            {
                SceneManager.LoadScene("R_ArcadeMachineWin");
            }
            //SceneManager.LoadScene(nextScene);
        }
    }
}
