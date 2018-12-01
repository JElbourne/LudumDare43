﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitDeath : MonoBehaviour {

    public void IsDead()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr) sr.enabled = false;

        //TODO
        //Play Sound
        //Play Animation
        //Play Particle Effects
        
        if (gameObject.tag == "Player")
        {
            GameController.Instance.GameOver();
        };

        Debug.Log("About to destroy");
        Destroy(gameObject);

    }
}
