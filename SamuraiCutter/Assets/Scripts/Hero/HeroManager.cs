using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : ObjectManager
{
    [SerializeField]
    protected HeroSettings m_settings;

    protected PlayerInputManager m_input;
    protected HeroMovement m_movement;

    protected bool m_isJumping=false;
    protected bool m_isJumpInputRelease = true;
    protected Vector2 m_inputMovement;

    protected void Awake()
    {
        m_input = GetComponent<PlayerInputManager>();
        m_movement = GetComponent<HeroMovement>();
    }

    protected void Start()
    {
        ChangeState(new HeroWaitState(this));
       // ChangeState(new HeroPlayState(this));
    }

    protected void Update()
    {
        m_currentState.Execute();
    }

    protected void FixedUpdate()
    {
        UpdateMovement();
    }

    public void UpdateInput()
    {
        m_inputMovement = new Vector2(m_input.GetMovementInputX(), m_input.GetMovementInputY());
        if (m_input.GetJumpInput() && m_isJumpInputRelease)
        {
            m_isJumping = true;
            //anim.SetTrigger("Jump");
            m_isJumpInputRelease = false;
            m_movement.JumpAction(m_inputMovement.x);
        }
        if (m_input.GetJumpInputUp())
        {
            m_isJumping = false;
            m_isJumpInputRelease = true;
        }
    }

    protected void UpdateMovement()
    {
        if (m_movement.IsFalling)
        {
            m_isJumping = false;
        }
        if (m_settings.AirControl)
        {
            if (m_isJumping || m_movement.IsFalling)
            {
                m_inputMovement = new Vector2(m_inputMovement.x * m_settings.AirControlMultiplicator, m_inputMovement.y);
            }
        }
        m_movement.Move(m_inputMovement.x * Time.fixedDeltaTime * m_settings.moveSpeed, m_isJumping && !m_isJumpInputRelease);
    }

    #region ACCESSOR

    public HeroSettings Settings
    {
        get=> m_settings;
    }

    #endregion
}
