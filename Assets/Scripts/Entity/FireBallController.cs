using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour {

    public Transform rotationCentre;
    public float rotationSpeed;

    Vector3 rotationAxis = new Vector3(0, 0, 1);

    private void Awake()
    {
        //GameController.Instance.enemyEntities.Add(this.gameObject);
    }
    // Update is called once per frame
    void FixedUpdate () {
        transform.RotateAround(rotationCentre.transform.position,
            rotationAxis, rotationSpeed * Time.deltaTime);
    }
}
