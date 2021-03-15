﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxHorizontal : MonoBehaviour
{
    public float parallaxFraction;
    private float startPosition;
    private Transform cam;

    // 
    private float width;

    void Start()
    {
        startPosition = transform.position.x;
        cam = Camera.main.transform;
        //
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void LateUpdate()
    {
        float offset = (cam.position.x * parallaxFraction);
        //
        float moved = cam.position.x - offset;
        if(moved > startPosition + width) startPosition += width;
        else if(moved < startPosition - width) startPosition -= width;
        transform.position = new Vector3(startPosition + offset, transform.position.y, transform.position.z);
    }
}
