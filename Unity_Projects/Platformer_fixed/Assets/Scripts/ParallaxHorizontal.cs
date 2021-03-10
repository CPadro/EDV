using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxHorizontal : MonoBehaviour
{
    public float parallaxFraction;
    private float startPosition;
    private Transform cam;

    void Start()
    {
        startPosition = transform.position.x;
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        float offset = (cam.position.x * parallaxFraction);
        transform.position = new Vector3(startPosition + offset, transform.position.y, transform.position.z);
    }
}
