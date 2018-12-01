using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour {

    public LayerMask platformLayer;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if ((platformLayer & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            TraitDeath td = other.gameObject.GetComponent<TraitDeath>();
            other.enabled = false;
            
            if (td)
            {
                Debug.Log("Found Platform Death");
                td.IsDead();
            }
        }
    }
}
