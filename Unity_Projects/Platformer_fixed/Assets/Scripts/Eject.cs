using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eject : MonoBehaviour
{
    public float springForce = 30f;
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) {
            PlatformerPlayer player = other.GetComponent<PlatformerPlayer>();
            player.Jump(springForce);
            _anim.SetTrigger("playerOn");
        }
    }
}
