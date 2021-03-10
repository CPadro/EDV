using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float speed = 4.5f;
    public float jumpForce = 10.0f;
    private Rigidbody2D _body;
    private Animator _anim;
    private BoxCollider2D _box;
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        float deltaX = Input.GetAxisRaw("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, _body.velocity.y);
        _body.velocity = movement;

        //
        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool grounded = (hit != null);
        _anim.SetBool("jumping", !grounded);

        //
        _body.gravityScale = (grounded &&
            Mathf.Approximately(deltaX, 0.0f) &&
            //Mathf.Approximately(_body.velocity.y, 0.0f)) ? 0.0f : 1.0f;
            Mathf.Abs(_body.velocity.y) < 0.5f) ? 0.0f : 1.0f;
        if(grounded && Input.GetKey(KeyCode.Space)) {
            _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        //
        MovingPlatform platform = null;
        Vector3 pScale = Vector3.one;
        if(hit != null) {
            platform = hit.GetComponent<MovingPlatform>();
        }
        if(platform != null) {
            transform.parent = platform.transform;
            pScale = platform.transform.localScale;
        }
        else {
            transform.parent = null;
        }

        _anim.SetFloat("speed",Mathf.Abs(deltaX));
        if(!Mathf.Approximately(deltaX, 0f)){
            transform.localScale = new Vector3(Mathf.Sign(deltaX) / pScale.x, 1 / pScale.y, 1);
        }
    }


    private void OnDrawGizmos() 
    {
        if(_box != null) {
            Vector3 max = _box.bounds.max;
            Vector3 min = _box.bounds.min;
            Vector3 corner1 = new Vector3(max.x, min.y - .1f,  0);
            Vector3 corner2 = new Vector3(min.x, min.y - .2f, 0);
            Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
            bool grounded = (hit != null);
            if(grounded) {Gizmos.color = Color.red;}
            else {Gizmos.color = Color.green;}
            Gizmos.DrawCube((corner1 + corner2) * 0.5f, corner2 - corner1);
        }
    }
}
