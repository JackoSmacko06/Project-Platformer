using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Other")]
    private Rigidbody2D rb;
    private Animator anim;
    public SpriteRenderer Spr;
    public CinemachineVirtualCamera VirCam;

    public float transitionDur;
    private float elapsedTime;

    private float percentageComplete;

    public Transform butterflyPos;

    [Header("Movement")]
    [HideInInspector]
    public float speed;

    public float maxSpeed;
    public float acceleration;
    public float decelleration;
    public float linearDrag;

    public float stoppingLinearDrag;
    public float velPower;
    private float moveInput;
    public bool facingright = true;
    public bool canMove = true;

    public bool changingDirection => (rb.velocity.x > 0f && moveInput < 0f) || (rb.velocity.x < 0f && moveInput > 0f);


    [Header("Jumping")]
    public float maxJumpForce;

    [HideInInspector]
    public float jumpForce;
    public float stillJuumpForce;

    public float airLinearDrag;

    public float fallMultiplier;
    public float lowJumpFallMultiplier;
    public float regularFallMultiplier;

    public bool isGrounded = false;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public float coyoteTime;
    public float coyoteTimeCounter;
    private float jBuffer;

    public bool jumped;

    [Header("WallJump")]
    public bool isTouchingFront;
    public Transform fronCheck;
    bool wallSliding;
    public float wallSlidingSpeed;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;
    public bool walljumpUnlocked = false;

    [Header("Dash")]
    IEnumerator dashCoroutine;
    bool isDashing;
    bool canDash = true;
    public bool dashUnlocked = true;
    public float dashForce;
    float normalGravity;
    public float dashDistance;
    public float dashDuration;
    public float dashCooldown;



    [Header("DoubleJump")]
    [HideInInspector] public float extraJumps;
    public float extraJumpsValue;
    public bool doubleJumpUnlocked;
    public float variableJumpHeightMultiplier;
    public float doubleJumpForce;


    [Header("Attacking")]
    public bool isAttacking;
    public bool attackDown;

    public int keys;

    public GameObject door;
    public GameObject door2;
    public GameObject door3;
    [HideInInspector] public bool doorUnlocked;

    public bool canTP = true;



    void Start()
    {
        instance = this;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        canTP = true;

        extraJumps = extraJumpsValue;

    }

    void Update()
    {

        moveInput = Input.GetAxisRaw("Horizontal");

        isTouchingFront = Physics2D.OverlapCircle(fronCheck.position, checkRadius, whatIsGround);



        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && dashUnlocked)
        {
            if (dashCoroutine != null)
            {
                StopCoroutine(dashCoroutine);
            }
            dashCoroutine = Dash(dashDuration, dashCooldown);
            StartCoroutine(dashCoroutine);
        }


        if (isTouchingFront == true && !isGrounded && moveInput != 0)
        {
            wallSliding = true;
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            wallSliding = false;
        }

        if (wallSliding == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));

        }


        if (Input.GetKeyDown(KeyCode.Space) && wallSliding == true)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }

        if (wallJumping == true && coyoteTimeCounter > 0)
        {
            rb.velocity = new Vector2(xWallForce * -moveInput, yWallForce);
            coyoteTimeCounter = 0;
        }


        if (!isGrounded)
        {
            coyoteTimeCounter -= coyoteTimeCounter > 0 ? Time.deltaTime : 0;
            jBuffer -= jBuffer > 0 ? Time.deltaTime : 0;
        }
        else
        {
            coyoteTimeCounter = coyoteTime;
        }

        if (isGrounded || isTouchingFront)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jBuffer = 0.085f;

            if (isGrounded)
            {
                coyoteTimeCounter = 0f;
            }
        }



        if (coyoteTimeCounter > 0 && jBuffer > 0)
        {
            Jump(maxJumpForce);
        }
        else if (doubleJumpUnlocked && !isGrounded && jBuffer > 0 && extraJumps > 0)
        {
            DoubleJump();

            extraJumps--;
        }

        if (!isGrounded)
        {
            jumped = true;
        }

        if (isGrounded && jumped == true)
        {
            //landed
            jumped = false;
        }



    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);


        if (canMove && wallJumping != true)
        {
            Run();
        }

        if (isGrounded)
        {
            ApplyLinearDrag();

        }else if(moveInput == 0 && isGrounded)
        {
            rb.drag = stoppingLinearDrag;
        }    
        else
        {
            ApplyLinearAirDrag();
            FallMultiplier();
        }



        if (facingright == false && moveInput > 0 && !isAttacking)
        {
            Flip();
        }
        else if (facingright == true && moveInput < 0 && !isAttacking)
        {
            Flip();
        }


        if (isDashing)
        {
            if (facingright)
            {
                rb.AddForce(new Vector2(dashForce, 0), ForceMode2D.Impulse);

            }
            else
            {

                rb.AddForce(new Vector2(-dashForce, 0), ForceMode2D.Impulse);
            }

            speed = 0f;
        }
        else
        {
            speed = maxSpeed;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "doubleJump")
        {
            doubleJumpUnlocked = true;
            Destroy(collision.gameObject);

            anim.SetTrigger("effect");
        }

        if (collision.gameObject.tag == "door")
        {
            if (keys >= 2)
            {
                door.GetComponent<Animator>().SetTrigger("open");
                doorUnlocked = true;
                Invoke("doorUnlockedfalse", 5f);
                keys = 0;

            }
        }

        if (collision.gameObject.tag == "door2")
        {
            if (keys >= 2)
            {
                door2.GetComponent<Animator>().SetTrigger("open");
                doorUnlocked = true;
                Invoke("doorUnlockedfalse", 5f);
                keys = 0;

            }
        }

        if (collision.gameObject.tag == "door3")
        {
            if (keys >= 2)
            {
                door3.GetComponent<Animator>().SetTrigger("open");
                doorUnlocked = true;
                Invoke("doorUnlockedfalse", 5f);
                keys = 0;

            }
        }

        if (collision.gameObject.tag == "collectable")
        {
            Destroy(collision.gameObject);
        }



    }

    public void Run()
    {

        rb.AddForce(new Vector2(moveInput, 0f) * acceleration);

        if (Mathf.Abs(rb.velocity.x) > speed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * speed, rb.velocity.y);
        }
    }

    void ApplyLinearDrag()
    {
        if (Mathf.Abs(moveInput) < 0.04f || changingDirection)
        {
            rb.drag = linearDrag;
        }
        else
        {
            rb.drag = 0;
        }


    }

    void ApplyLinearAirDrag()
    {
        rb.drag = airLinearDrag;
    }

    public void Jump(float force)
    {
        jBuffer = 0; 
        coyoteTimeCounter = 0;

        rb.velocity = new Vector2(rb.velocity.x, 0f);

        if(moveInput != 0)
        {
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }
        else if(moveInput == 0)
        {
            rb.AddForce(Vector2.up * stillJuumpForce, ForceMode2D.Impulse);
        }
        
    }

    public void PortalJump(float force)
    {
        rb.AddForce(Vector2.up * rb.velocity.y * force, ForceMode2D.Impulse);
    }

    public void DoubleJump()
    {
        jBuffer = 0; ;
        coyoteTimeCounter = 0;

        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);
    }

    void FallMultiplier()
    {
        if (rb.velocity.y < 0f)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.gravityScale = lowJumpFallMultiplier;
        }
        else
        {
            rb.gravityScale = regularFallMultiplier;
        }
    }

  

    IEnumerator Dash(float dashDuration, float dashCooldown)
    {
        isDashing = true;
        canDash = false;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    IEnumerator WaitWallJump(float time)
    {
        wallSliding = true;
        yield return new WaitForSeconds(time);
        wallSliding = true;
    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }

    public void isAttackingTrue()
    {
        isAttacking = true;
    }
    public void isAttackingFalse()
    {
        isAttacking = false;
    }

    public void doorUnlockedfalse()
    {
        doorUnlocked = false;
    }

    public void shake()
    {
        cameraShake.instance.Shake(7, 8, 1);
    }

    public void zoomIn()
    {


        //VirCam.m_Lens.OrthographicSize = Mathf.Lerp(13.5f, 8f, transitionDur += Time.deltaTime);
        VirCam.m_Lens.OrthographicSize += Time.deltaTime;


    }

    public void zoomOut()
    {
        VirCam.m_Lens.OrthographicSize = 13.5f;
    }

    public void Freeze()
    {
        
        
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        
      
        
    }

    public void FreezeStop()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }


    void Flip()
    {

        facingright = !facingright;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}


