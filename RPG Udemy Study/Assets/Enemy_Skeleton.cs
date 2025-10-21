using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Entity
{
    bool isAttacking;

    [Header("Move Info")]
    [SerializeField] private float MoveSpeed;

    [Header("Player Detection")]
    [SerializeField] private float PlayerCheckDistance;
    [SerializeField] private LayerMask WhatIsPlayer;

    private RaycastHit2D isPlayerDetected;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (isPlayerDetected)
        {
            if (isPlayerDetected.distance > 1)
            {
                rb.velocity = new Vector2(MoveSpeed * 20 * facingDir, rb.velocity.y);

                Debug.Log("I see you!" + rb.velocity);

                isAttacking = false;

            }
            else
            {
                Debug.Log("Attack" + isPlayerDetected.collider.gameObject.name);

                isAttacking = true;
            }
        }
        

        if (!isGrounded || isWalled)
        {
            Filp();
        }

        Movement();
    }

    private void Movement()
    {
        if (!isAttacking)
            rb.velocity = new Vector2(MoveSpeed * facingDir, rb.velocity.y);
    }

    protected override void CollisionCheck()
    {
        base.CollisionCheck();

        isPlayerDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, PlayerCheckDistance, WhatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + (PlayerCheckDistance * facingDir), transform.position.y));
    }
}
