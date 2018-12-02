using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public void StartGame()
    {
        Debug.Log("Button Clicked");
        SceneManager.LoadScene("PlayGame");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
