using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] PlayerSettings reglages;
    PlayerInputManager input;
    CharacterController2D characterControl;
    LinecastCutterBehaviour lineCutter;
    Animator anim;
    Vector2 movement;
    Vector2 slashDirection;
    private Rigidbody2D rigidbody;

    bool isJumping = false;
    bool jumpRelease = true;

    bool isDashing = false;
    [SerializeField] float forceDash;
    float curCooldownDash;

    bool isSlashing = false;
    float slashAmount = 0f;

    void Awake()
    {
        input = GetComponent<PlayerInputManager>();
        characterControl = GetComponent<CharacterController2D>();
        lineCutter = GetComponent<LinecastCutterBehaviour>();
        anim = GetComponent<Animator>();
        characterControl.InitSettings(reglages);
        rigidbody = this.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
    }

    void Update()
    {
        UpdateInput();
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        UpdatePhysics();
    }

    void UpdateInput()
    {
        //dash
        if (isDashing)
        {
            return;
        }
        if (curCooldownDash > 0)
        {
            curCooldownDash -= Time.fixedDeltaTime;
        }

        movement = new Vector2(input.GetMovementInputX(),input.GetMovementInputY());
        UpdateSlash();
        if (input.GetJumpInput()&&jumpRelease)
        {
            isJumping = true;
            anim.SetTrigger("Jump");
            jumpRelease = false;
            characterControl.JumpAction(movement.x);
        }
        if(input.GetJumpInputUp())
        {
            isJumping = false;
            jumpRelease = true;
        }
        if (input.GetSpecialInput())
        {
            Application.LoadLevel(0);
        }
    }

    void UpdateAnimator()
    {
        anim.SetBool("Run", characterControl.IsGrounded() && movement.x != 0);
        anim.SetBool("Falling", characterControl.IsFalling());
    }

    void UpdatePhysics()
    //fixed update
    {
        if (characterControl.IsFalling())
        {
            isJumping = false;
        }
        if(reglages.AirControl)
        {
            if(isJumping || characterControl.IsFalling())
            {
                movement = new Vector2(movement.x * reglages.AirControlMultiplicator, movement.y);
            }
        }
        if (isDashing)
        {
            return;
        }
        characterControl.Move(movement.x * Time.fixedDeltaTime * reglages.moveSpeed, isJumping&&!jumpRelease);
    }

    private void UpdateSlash()
    {
        Vector2 slashTest = input.GetRightStickInput();
        if (slashTest != Vector2.zero && !isSlashing)
        {
            slashDirection = slashTest.normalized;
            isSlashing = true;
        }
        else if (slashTest != Vector2.zero && isSlashing)
        {
            if (slashTest.magnitude >0.25f)
            {
                slashDirection = slashTest.normalized;
            }
            if (slashAmount < 1f)
            {
                slashAmount += Time.deltaTime * reglages.slashChargeModificator;
            }

        }
        if (slashTest == Vector2.zero && isSlashing)
        {
            if (slashAmount < 0.2f)
            {
                lineCutter.TryLineCastCut(slashDirection, reglages.slashRange);
            }
            else
            {
                float range = Mathf.Max(reglages.maxSlashRange * slashAmount, reglages.slashRange);
                lineCutter.TryLineCastCut(slashDirection, range);
            }
            slashAmount = 0f;
            isSlashing = false;
            slashDirection = Vector2.zero;
        }
    }

    public void DashActionWithDirection(Vector2 dirVector, float dashForceMultiplicator)
    {
        isDashing = true;
        curCooldownDash = reglages.DashCooldown;
        StartCoroutine(characterControl.DashAction(dirVector, dashForceMultiplicator));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
    }

    private void OnCollisionExit2D(Collision2D other)
    {
    }

    public bool IsDashing
    {
        get
        {
            return IsDashing;
        }
        set
        {
            IsDashing = value;
        }
    }
}
