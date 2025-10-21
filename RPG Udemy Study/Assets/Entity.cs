using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;

    protected int facingDir = 1;
    protected bool facingRight = true;

    [Header("Collision Info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float GroundCheckDistance;
    [Space]
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float WallCheckDistance;
    [SerializeField] protected LayerMask WhatIsGround;

    protected bool isGrounded;
    protected bool isWalled;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        if (wallCheck == null)
        {
            wallCheck = transform;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CollisionCheck();
    }

    protected virtual void CollisionCheck()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, GroundCheckDistance, WhatIsGround);
        isWalled = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, WallCheckDistance, WhatIsGround);
    }

    protected virtual void Filp()
    {
        facingDir = facingDir * -1;

        facingRight = !facingRight;

        transform.Rotate(0, 180, 0);
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - GroundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + (WallCheckDistance * facingDir), wallCheck.position.y));
    }
}
