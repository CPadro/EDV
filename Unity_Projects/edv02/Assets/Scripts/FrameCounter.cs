using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameCounter : MonoBehaviour
{

    private long frameCounter;
    private float accum;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started!");
        frameCounter = 0;
        accum = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(++frameCounter % 100 == 0) {
            Debug.Log("Frames: " + frameCounter.ToString());
        }
    }
}
