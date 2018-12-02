using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIconsController : MonoBehaviour {

    Image image;

    public Sprite health0;
    public Sprite health1;
    public Sprite health2;
    public Sprite health3;

    public TraitHealth playerHealth;

    // Use this for initialization
    void Awake () {
        image = GetComponent<Image>();
        image.sprite = health3;
	}
	
	// Update is called once per frame
	void Update () {
        int currentHealth = playerHealth.GetHealthAsInt();

        if (currentHealth == 0)
        {
            image.sprite = health0;
        }
        else if(currentHealth == 1)
        {
            image.sprite = health1;
        }
        else if (currentHealth == 2)
        {
            image.sprite = health2;
        }
        else if (currentHealth == 3)
        {
            image.sprite = health3;
        }
    }
}
