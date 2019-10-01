﻿using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    private PlayerSettings reglages;
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

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
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
            // Add a vertical force to the player.
            if (reglages.JumpNuance)
            {
                m_jumpTimeCounter = reglages.jumpInputTime;

                m_Rigidbody2D.AddForce(new Vector2(0f, reglages.jumpForce* reglages.jumpForce));
                StartCoroutine(TestCoroutine(moveX));
               // m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, reglages.jumpForce*Time.fixedDeltaTime);
            }
            else
            {
                m_Rigidbody2D.AddForce(new Vector2(0f, reglages.jumpForce));
            }
        }
    }

   IEnumerator TestCoroutine(float moveX)
    {
        yield return new WaitForSeconds(0.2f);
        m_Rigidbody2D.AddForce(new Vector2(Mathf.Pow(moveX * 10f, 2), 0f));
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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
}
