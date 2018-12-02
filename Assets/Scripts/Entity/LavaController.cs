using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour {

    public Transform startMarker;
    public Transform endMarker;

    // Movement speed in units/sec.
    public float speed = 0.075f;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    public LayerMask enemyLayer;

    private void Awake()
    {
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    // Update is called once per frame
    void Update () {
        // Distance moved = time * speed.
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);

        if (GameController.Instance.GetScore() > 200)
        {
            Debug.Log("Lava Increasing at 200");
            AudioManager.instance.ChangeBackgroundVolume(1.0f);
            speed = 0.4f;
        }
        else if (GameController.Instance.GetScore() > 100)
        {
            Debug.Log("Lava Increasing at 100");
            AudioManager.instance.ChangeBackgroundVolume(0.80f);
            speed = 0.3f;
        }
        else if (GameController.Instance.GetScore() > 50)
        {
            Debug.Log("Lava Increasing at 100");
            AudioManager.instance.ChangeBackgroundVolume(0.7f);
            speed = 0.2f;
        }
        else if (GameController.Instance.GetScore() > 150)
        {
            AudioManager.instance.ChangeBackgroundVolume(0.6f);
            speed = 0.1f;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        

        if ((enemyLayer & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            AudioManager.instance.Play("HitLava");

            //Debug.Log("Lava Going Down");
            TraitDeath td = other.gameObject.GetComponent<TraitDeath>();

            if (td)
            {
                Debug.Log("Found Enemy Death");
                td.IsDead();
            }
            other.enabled = false;
            startTime = Time.time;

            GameController.Instance.AddToScore(5);
        }
    }
}
