using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 speed;
    public Vector2 gravity = new Vector2(0.0f, -7.8f);
    GameObject basket;
    static int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        basket = GameObject.Find("basket");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, -360.0f * Time.deltaTime);
        speed += gravity * Time.deltaTime;
        Vector2 off =  speed * Time.deltaTime;
        transform.position += new Vector3(off.x, off.y);
        float distanceToBasket = Vector3.Distance(transform.position, basket.transform.position);
        if(distanceToBasket < 0.5f) {
            counter++;
            Debug.Log("Caught!: " + counter.ToString());
            Destroy(gameObject);
        } else if(transform.position.y < -15) {
            Destroy(gameObject);
        }
    }
}
