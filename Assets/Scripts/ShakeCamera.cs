﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour {

    public Animator cameraAnimation;

    public void CameraShake()
    {
        cameraAnimation.SetTrigger("shake");
    }
}
