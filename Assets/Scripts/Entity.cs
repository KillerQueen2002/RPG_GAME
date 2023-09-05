using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }


    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] LayerMask whatIsGround;


    public int facingdir { get; private set; } = 1;
    protected bool facingRight = true;


    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }


    protected virtual void Update()
    {
        
    }

    #region Velocity
    public void ZeroVelocity() => rb.velocity = new Vector2(0, 0);

    public void setVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    #endregion
    #region Collision
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround); // Hàm này có chức năng kiểm tra xem có đất (hoặc bề mặt đáy khác) được phát hiện ở dưới một vị trí cụ thể hay không, bằng cách thực hiện một tia raycast từ một vị trí cho trước và theo hướng xuống (Vector2.down) để kiểm tra xem có va chạm với một đối tượng định trước (whatIsGround) trong phạm vi cụ thể (wallCheckDistance).
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingdir, wallCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
    #endregion
    #region Flip
    public virtual void Flip()
    {
        facingdir = facingdir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public virtual void FlipController(float _x)//Hàm FlipController() này được sử dụng để kiểm tra tốc độ di chuyển của đối tượng (có lưu trữ trong rb.velocity.x) và hướng hiện tại mà đối tượng đang đối diện (được lưu trữ trong biến facingRight). Nếu tốc độ di chuyển là dương (tức là đang di chuyển sang phải) mà đồng thời đối tượng đang không đối diện về phía phải (facingRight là false), hoặc tốc độ di chuyển là âm (tức là đang di chuyển sang trái) mà đối tượng đang đối diện về phía phải (facingRight là true), thì hàm sẽ thực hiện hàm Flip() để đảo ngược hướng của đối tượng.
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }
}
#endregion

