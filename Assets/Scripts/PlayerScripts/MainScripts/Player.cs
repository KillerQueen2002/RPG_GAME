using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attack details")]
    public Vector2[] attackMovement;
    public bool isBusy { get; private set; }
    [Header("Move info")]
    public float moveSpeed = 8f;
    public float jumpForce;

    [Header("Dash info")]
    [SerializeField] float dashColldown;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] LayerMask whatIsGround;

    public int facingdir { get; private set; } = 1;
    private bool facingRight = true;

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallSideState wallSide { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerPrimaryAttackState primaryAttack { get; private set; }

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idel");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSide = new PlayerWallSideState(this, stateMachine, "WallSide");
        wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
    }
     
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine.Initialize(idleState);
    }

    public float timer;
    public float cooldown;

    private void Update()
    {
        stateMachine.currenState.Update();
        timer -= Time.deltaTime;
        //Debug.Log(IsWallSideDetected());
        CheckForDashInput();
    }

    //giup delay mot doan code
    //ham BusyFor se set isBusy = true sau do goi ham WaitforSecond de delay 
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;  
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    //dung ham animation trigger de goi den ham animation finish trigger
    public void AnimationTrigger() => stateMachine.currenState.AnimationFinishTrigger();
  

   


    private void CheckForDashInput()
    {
        if (IsWallSideDetected())// neu dang truot tuong thi ko dc dash
            return;

        dashUsageTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse1) && dashUsageTimer <0)
        {
            dashUsageTimer = dashColldown;
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingdir;


            stateMachine.ChangeState(dashState);
        }
    }
    #region Velocity
    public void ZeroVelocity() => rb.velocity = new Vector2 (0, 0);
    public void setVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion
    #region Collision
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround); // Hàm này có chức năng kiểm tra xem có đất (hoặc bề mặt đáy khác) được phát hiện ở dưới một vị trí cụ thể hay không, bằng cách thực hiện một tia raycast từ một vị trí cho trước và theo hướng xuống (Vector2.down) để kiểm tra xem có va chạm với một đối tượng định trước (whatIsGround) trong phạm vi cụ thể (wallCheckDistance).
    public bool IsWallSideDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, whatIsGround);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
    #endregion
    #region Flip
    public void Flip()
    {
        facingdir = facingdir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float _x)//Hàm FlipController() này được sử dụng để kiểm tra tốc độ di chuyển của đối tượng (có lưu trữ trong rb.velocity.x) và hướng hiện tại mà đối tượng đang đối diện (được lưu trữ trong biến facingRight). Nếu tốc độ di chuyển là dương (tức là đang di chuyển sang phải) mà đồng thời đối tượng đang không đối diện về phía phải (facingRight là false), hoặc tốc độ di chuyển là âm (tức là đang di chuyển sang trái) mà đối tượng đang đối diện về phía phải (facingRight là true), thì hàm sẽ thực hiện hàm Flip() để đảo ngược hướng của đối tượng.
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }
}
#endregion
