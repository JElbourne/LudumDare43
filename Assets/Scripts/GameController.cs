﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    
    public static GameController Instance;

    public TextMeshProUGUI scoreTextUI;

    //public List<GameObject> enemyEntities = new List<GameObject>();


    int score = 0;
    int highScore;

	// Use this for initialization
	void Awake () {
		if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(this);
        }

	}

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");

        AudioManager.instance.ChangeBackgroundVolume(0.5f);
    }

    private void Update()
    {
        scoreTextUI.SetText("Score: " + score.ToString());
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("Score", score);
        if (score > highScore) PlayerPrefs.SetInt("HighScore", score);

        //Debug.Log("Game Controller sets game over");

        AudioManager.instance.StopBackgroundMusic();
        AudioManager.instance.ChangeBackgroundVolume(1.0f);
        AudioManager.instance.Play("GameOverMusic");

        SceneManager.LoadScene("GameOver");
    }

    public void AddToScore(int value)
    {
        score += value;
    }

    public int GetScore()
    {
        return score;
    }
	
}
