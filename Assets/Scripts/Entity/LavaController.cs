using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour {

    public Transform startMarker;
    public Transform endMarker;

    // Movement speed in units/sec.
    public float speed = 1.0F;

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
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if ((enemyLayer & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            Debug.Log("Lava Going Down");
            TraitDeath td = other.gameObject.GetComponent<TraitDeath>();

            //Destroy(other);
            if (td)
            {
                Debug.Log("Found Enemy Death");
                td.IsDead();
            }
            other.enabled = false;
            startTime = Time.time;
            //transform.position += Vector3.down * 5f;
        }
    }
}
