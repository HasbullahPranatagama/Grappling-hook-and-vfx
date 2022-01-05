﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGunT : MonoBehaviour
{
    public GraplingGunT grappling;

    private void Update()
    {
        if (grappling.IsGrappling()) return;
        {
            transform.LookAt(grappling.GetGrapplePoint());
        }
    }
}
