using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public TextMeshProUGUI scoreTextUI;
    public TextMeshProUGUI highScoreTextUI;

    private void Awake()
    {
        string hs = PlayerPrefs.GetInt("HighScore").ToString();
        if (hs == "0") hs = "--";

        if (scoreTextUI) scoreTextUI.SetText("Score: " + PlayerPrefs.GetInt("Score").ToString());
        if (highScoreTextUI) highScoreTextUI.SetText("High Score: " + hs);
    }

    public void StartGame()
    {
        AudioManager.instance.StopBackgroundMusic();
        AudioManager.instance.Play("GamePlayMusic");
        //Debug.Log("Button Clicked");
        SceneManager.LoadScene("PlayGame");
    }

    public void BackToMenu()
    {
        AudioManager.instance.StopBackgroundMusic();
        AudioManager.instance.Play("IntroMusic");
        SceneManager.LoadScene("Menu");
    }
}
