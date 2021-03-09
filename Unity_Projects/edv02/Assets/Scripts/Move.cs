using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 9.0f;
    public float leftBorder, rightBorder;
    // Start is called before the first frame update
    void Start()
    {
        //Esto viene predefinido en Unity
        //Transform t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);
       if(transform.position.x < leftBorder)
            transform.position = new Vector2(leftBorder, transform.position.y);
       else if(transform.position.x > rightBorder)
            transform.position = new Vector2(rightBorder, transform.position.y);
    }
}
