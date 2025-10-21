using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    [Header("Move Info")]
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float JumpForce;
    private float XInput;

    [Header("Dash Info")]
    [SerializeField] private float DashSpeed;
    [SerializeField] private float DashDuration;
    private float DashTime;
    [SerializeField] private float DashCooldown;
    private float DashCooldownTime;

    [Header("Attack Info")]
    [SerializeField] private float comboTime;
    private float comboTimeWindow;
    private bool isAttacking;
    private int comboCounter;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        Movement();

        CheckInput();

        

        DashTime -= Time.deltaTime;
        DashCooldownTime -= Time.deltaTime;
        comboTimeWindow -= Time.deltaTime;



        FilpController();

        AnimatorContorllers();

    }

    public void AttackOver()
    {
        isAttacking = false;

        comboCounter++;

        if (comboCounter > 2)
        {
            comboCounter = 0;
        }
    }

    private void CheckInput()
    {
        XInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttackEvent();
        }
    }

    private void StartAttackEvent()
    {
        if (!isGrounded || DashTime > 0)
        {
            return;
        }

        isAttacking = true;

        if (comboTimeWindow <= 0)
        {
            comboCounter = 0;
        }

        comboTimeWindow = comboTime;
    }

    private void DashAbility()
    {
        if (DashCooldownTime <= 0 && !isAttacking)
        {
            DashTime = DashDuration;
            DashCooldownTime = DashCooldown;
        }

    }

    private void Movement()
    {
        if (isAttacking)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if (DashTime > 0)
        {
            rb.velocity = new Vector2(facingDir * DashSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(XInput * MoveSpeed, rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (isGrounded && !isAttacking)
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
    }

    private void AnimatorContorllers()
    {
        bool isMoving = rb.velocity.x != 0;

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", DashTime > 0);

        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetFloat("DashTime", DashTime);

        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("comboCounter", comboCounter);
    }

    private void FilpController()
    {
        if (XInput > 0 && !facingRight)
        {
            Filp();
        }
        else if (XInput < 0 && facingRight)
        {
            Filp();
        }
    }

    protected override void CollisionCheck()
    {
        base.CollisionCheck();
    }
}
