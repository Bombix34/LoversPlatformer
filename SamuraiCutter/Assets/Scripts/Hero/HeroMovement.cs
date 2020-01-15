using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeroMovement : MonoBehaviour
{ 
    private HeroSettings m_reglages;
    private HeroManager m_manager;
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

    private Vector2 m_velocity;

    private float m_saveMovementOnJump = 0f;

    private float m_GravityScale;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_GravityScale = m_Rigidbody2D.gravityScale;
        m_manager = GetComponent<HeroManager>();
        m_velocity = Vector2.zero;

        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
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
                m_saveMovementOnJump = 0;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move, bool jumpInput)
    {
        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_reglages.AirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            if (m_saveMovementOnJump != 0)
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x + m_saveMovementOnJump * Time.fixedDeltaTime * 10f, m_Rigidbody2D.velocity.y);


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
        if (m_reglages.JumpNuance && (jumpInput && m_jumpTimeCounter > 0))
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_reglages.jumpForce * Time.fixedDeltaTime * 5f);
            m_jumpTimeCounter -= Time.fixedDeltaTime;
        }
    }

    public void JumpAction(float moveX)
    {
        if (m_Grounded)
        {
            m_Grounded = false;
            m_saveMovementOnJump = moveX * 50f;
            // Add a vertical force to the player.
            if (m_reglages.JumpNuance)
            {
                m_jumpTimeCounter = m_reglages.jumpInputTime;

                m_Rigidbody2D.AddForce(new Vector2(m_saveMovementOnJump, m_reglages.jumpForce), ForceMode2D.Impulse);
                // m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, reglages.jumpForce*Time.fixedDeltaTime);
            }
            else
            {
                m_Rigidbody2D.AddForce(new Vector2(0f, m_reglages.jumpForce));
            }
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        //m_manager.PlayerSprite.flipX = !m_FacingRight;
    }

    public bool IsGrounded
    {
        get=> m_Grounded;
    }

    public bool IsFalling
    {
        get => (m_Rigidbody2D.velocity.y < 0f && !m_Grounded);
    }

    public HeroSettings Reglages
    {
        set
        {
            m_reglages = value;
        }
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