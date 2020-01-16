using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : ObjectManager
{
    [SerializeField]
    protected HeroSettings m_settings;

    [SerializeField]
    protected Rigidbody2D m_body;

    [SerializeField]
    private bool m_debugTest;

    protected PlayerInputManager m_input;
    protected HeroMovement m_movement;

    protected bool m_isJumping = false;
    protected bool m_isJumpInputRelease = true;
    protected Vector2 m_inputMovement;

    protected void Awake()
    {
        m_input = GetComponent<PlayerInputManager>();
        m_movement = GetComponent<HeroMovement>();
    }

    protected void Start()
    {
        if(!m_debugTest)
            ChangeState(new HeroWaitState(this));
        else
            ChangeState(new HeroPlayState(this));
    }

    protected void Update()
    {
        m_currentState.Execute();
    }

    protected void FixedUpdate()
    {
        UpdateMovement();
    }

    public void UpdateMovementInput()
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

    public PlayerInputManager Inputs
    {
        get => m_input;
    }

    public Rigidbody2D Body
    {
        get => m_body;
    }

    #endregion
}
