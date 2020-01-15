using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    private PlayerSettings reglages;
    private PlayerManager manager;
    //[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    private float m_jumpTimeCounter;

    private Vector2 velocity;

    private Vector2 DashDirection = Vector2.zero;

    private float saveMovementOnJump = 0f;

    private float GravityScale;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        GravityScale = m_Rigidbody2D.gravityScale;
        manager = GetComponent<PlayerManager>();
        velocity = Vector2.zero;

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                saveMovementOnJump = 0;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move,  bool jumpInput)
    {
        //only control the player if grounded or airControl is turned on
        if (m_Grounded || reglages.AirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            if (saveMovementOnJump != 0)
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x + saveMovementOnJump * Time.fixedDeltaTime*10f, m_Rigidbody2D.velocity.y);

            
            // If the input is moving the player right and the player is facing left...
            if ((move > 0 && !m_FacingRight) || (move < 0 && m_FacingRight))
            {
                // ... flip the player.
                // Otherwise if the input is moving the player left and the player is facing right...
                Flip();
            }
            
        }
        if (!jumpInput && m_jumpTimeCounter > 0)
        {
            m_jumpTimeCounter = 0;
        }
        // If the player should jump...
        if (reglages.JumpNuance && (jumpInput && m_jumpTimeCounter>0))
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x,reglages.jumpForce * Time.fixedDeltaTime*5f);
            m_jumpTimeCounter -= Time.fixedDeltaTime;
        }
    }

    public void JumpAction(float moveX)
    {
        if (m_Grounded)
        {
            m_Grounded = false;
            saveMovementOnJump = moveX*50f;
            // Add a vertical force to the player.
            if (reglages.JumpNuance)
            {
                m_jumpTimeCounter = reglages.jumpInputTime;

                m_Rigidbody2D.AddForce(new Vector2(saveMovementOnJump, reglages.jumpForce),ForceMode2D.Impulse);
               // m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, reglages.jumpForce*Time.fixedDeltaTime);
            }
            else
            {
                m_Rigidbody2D.AddForce(new Vector2(0f, reglages.jumpForce));
            }
        }
    }

    public IEnumerator DashAction(Vector2 dirVector, float forceMultiplicator)
    {
        DashDirection = dirVector;
        Vector2 tempDirection =dirVector;
        tempDirection.Normalize();

        m_Rigidbody2D.gravityScale = 0f;
        m_Rigidbody2D.velocity = new Vector2(tempDirection.x * reglages.moveSpeed * forceMultiplicator, tempDirection.y * reglages.moveSpeed* forceMultiplicator) * reglages.DashForce;
        /*float angle = Vector2.SignedAngle(Vector2.right, tempDirection);
        GetComponentInChildren<SpriteRenderer>().transform.Rotate(new Vector3(0.0f, 0.0f, angle));*/
        //attendre la fin du dash
        
        float dash = reglages.DashDuration;
        while (dash > 0)
        {
            dash -= Time.deltaTime;
            yield return new WaitForSeconds(0.001f);
        }
        EndDash();
    }

    public void EndDash()
    {
        StopCoroutine("DashAction");
        m_Rigidbody2D.velocity = Vector2.zero;
        m_Rigidbody2D.gravityScale = GravityScale;
        manager.IsDashing = false;
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        manager.PlayerSprite.flipX=!m_FacingRight;
    }

    public bool IsGrounded()
    {
        return m_Grounded;
    }

    public bool IsFalling()
    {
        return (m_Rigidbody2D.velocity.y < 0f && !m_Grounded);
    }

    public void InitSettings(PlayerSettings regl)
    {
        reglages = regl;
    }

    public Vector2 GetDashDirection()
    {
        return DashDirection;
    }

    public bool FacingRight
    {
        get
        {
            return m_FacingRight;
        }
        set
        {
            m_FacingRight = value;
        }
    }
}
