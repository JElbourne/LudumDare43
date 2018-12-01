using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitInstantDeath : MonoBehaviour {

    public LayerMask damageLayer;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if ((damageLayer & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            Debug.Log("PLAYER DEAD");
            TraitDeath td = GetComponent<TraitDeath>();

            if (td)
            {
                Debug.Log("Found GameObjects Death");
                td.IsDead();
            }
            other.enabled = false;
        }
    }
}
