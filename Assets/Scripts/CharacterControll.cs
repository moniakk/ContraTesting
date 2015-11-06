using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterControll : MonoBehaviour
{
    public LayerMask WhatIsGround;
    public Transform GroundCheck;
    public GameObject TestPlatform;
    public float MaxSpeed = 10f;
    public float JumpForce = 10;

    public Transform CurrentWaypoint;
    public Transform NextWaypoint;

    private const float GroundRadius = 0.3f;
    private bool _grounded;
    private Animator _anim;
    private Rigidbody2D _rigidBody;
    private Collider2D _playerGroundCollider;
    private Collider2D _interactingCollider;
    private List<Collider2D> _disabledColliders;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _playerGroundCollider = GetComponent<CircleCollider2D>();
        _disabledColliders = new List<Collider2D>();
        _interactingCollider = null;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.Space))
        {
            _anim.SetBool("IsGrounded", false);

            if (_interactingCollider != null)
            {
                Physics2D.IgnoreCollision(_playerGroundCollider, _interactingCollider, true);
                _disabledColliders.Add(_interactingCollider);
            }
        }
        else if (_grounded && Input.GetKeyDown(KeyCode.Space))
        {
            _anim.SetBool("IsGrounded", false);
            _rigidBody.velocity = new Vector2(0, 0);
            _rigidBody.AddForce(new Vector2(0, JumpForce));
        }
    }

    void FixedUpdate()
    {
        _grounded = Physics2D.OverlapCircle(GroundCheck.position, GroundRadius, WhatIsGround);
        var move = Input.GetAxis("Horizontal");

        _anim.SetBool("IsGrounded", _grounded);
        _anim.SetFloat("VSpeed", _rigidBody.velocity.y);
        _anim.SetBool("IsRuning", Math.Abs(move) > 0);

        _rigidBody.velocity = new Vector2(move * MaxSpeed, _rigidBody.velocity.y);

        if (Math.Abs(move) > 0)
        {
            transform.rotation = move > 0.0000001 ?
                new Quaternion(transform.rotation.x, 0, 0, 0)
                : new Quaternion(transform.rotation.x, 180, 0, 0);
        }
    }


    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Platform")
            _interactingCollider = collisionInfo.collider;
    }

    void OnCollisionExit2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Platform")
        {
            foreach (var collider2 in _disabledColliders)
                Physics2D.IgnoreCollision(_playerGroundCollider, collider2, false);

            _disabledColliders = new List<Collider2D>();
        }
    }
}
