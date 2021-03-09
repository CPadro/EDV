using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float shootingPeriod = 1.0f;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 0.0f, shootingPeriod);
    }

    //method Fire
    void Fire() {
        GameObject obj = Instantiate<GameObject>(projectile);
        obj.transform.position = transform.position;

        Projectile proj = obj.GetComponent<Projectile>();
        proj.speed = new Vector2(Random.Range(6, 12), Random.Range(-1, 0));
    }
}
