using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController Instance;

    public List<GameObject> enemyEntities = new List<GameObject>();

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

    public void GameOver()
    {
        Debug.Log("Game Controller sets game over");
        SceneManager.LoadScene("GameOver");
    }
	
}
