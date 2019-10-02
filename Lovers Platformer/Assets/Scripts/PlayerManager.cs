using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] bool isPlayer1;
    [SerializeField] PlayerSettings reglages;
    PlayerInputManager input;
    CharacterController2D characterControl;
    Animator anim;
    Vector2 movement;
    private Rigidbody2D rigidbody;
    GameObject otherPlayer;

    bool jumpTriggerAction = false;
    bool isJumping = false;
    bool jumpRelease = true;
    bool collision = false;

    bool isDashing = false;
    [SerializeField] float forceDash;
    float curCooldownDash;


    void Awake()
    {
        input = GetComponent<PlayerInputManager>();
        characterControl = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
        characterControl.InitSettings(reglages);
        rigidbody = this.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (isPlayer1)
        {
            otherPlayer = GameManager.Instance.GetPlayer2().gameObject;
        }
        else
        {
            otherPlayer = GameManager.Instance.GetPlayer1().gameObject;
        }
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
            return;
        if (curCooldownDash > 0)
            curCooldownDash -= Time.fixedDeltaTime;

        movement = new Vector2(input.GetMovementInputX(),input.GetMovementInputY());
        if (input.GetJumpInput()&&jumpRelease)
        {
            jumpTriggerAction = true;
            isJumping = true;
            jumpRelease = false;
            characterControl.JumpAction(movement.x);
        }
        if(input.GetJumpInputUp())
        {
            jumpTriggerAction = false;
            jumpRelease = true;
        }
        if (input.GetSpecialInput())
            print("special");
        if (input.GetTeleportInputDown()&&curCooldownDash<=0)
        {
            DashAction();
        }
    }

    void UpdateAnimator()
    {
        anim.SetBool("Run", characterControl.IsGrounded() && movement.x != 0);
        anim.SetBool("Jump", isJumping);
        anim.SetBool("Falling", characterControl.IsFalling());
    }

    void UpdatePhysics()
    //fixed update
    {
        if (characterControl.IsFalling())
            isJumping = false;
        if(reglages.AirControl)
        {
            if(isJumping || characterControl.IsFalling())
                movement = new Vector2(movement.x * reglages.AirControlMultiplicator, movement.y);
        }
        if (isDashing)
            return;
        characterControl.Move(movement.x * Time.fixedDeltaTime * reglages.moveSpeed, jumpTriggerAction);
    }

    void DashAction()
    {
        //Switch
        isDashing = true;
        curCooldownDash = reglages.DashCooldown;
        Vector2 dirVector =new Vector2 (otherPlayer.transform.position.x - this.transform.position.x , otherPlayer.transform.position.y-this.transform.position.y);
        StartCoroutine(characterControl.DashAction(dirVector,1f));
    }

    public void DashActionWithDirection(Vector2 dirVector, float dashForceMultiplicator)
    {
        isDashing = true;
        curCooldownDash = reglages.DashCooldown;
        StartCoroutine(characterControl.DashAction(dirVector, dashForceMultiplicator));
    }

    public void ContactWithOtherPlayerOnDash(Vector2 dashDir)
    {
        PlayerManager otherPlayerManager = otherPlayer.GetComponent<PlayerManager>();
        //print("dash collision");
       // if (otherPlayerManager.IsDashing())
         //   DashActionWithDirection(otherPlayer.GetComponent<CharacterController2D>().GetDashDirection() * 2f);
        otherPlayerManager.DashActionWithDirection(dashDir,1.3f);
       /* if (otherPlayerManager.IsDashing())
        {
            if (!isPlayer1)
                return;
            DashActionWithDirection(otherPlayer.GetComponent<CharacterController2D>().GetDashDirection()*5f);
            otherPlayerManager.DashActionWithDirection(dashDir * 5f);
        }
        else
        {
            otherPlayerManager.DashActionWithDirection(dashDir * 2f);
        }*/
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        collision = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        collision = false;
    }

    public void SetIsDashing(bool newVal)
    {
        isDashing = newVal;
    }

    public bool IsDashing()
    {
        return isDashing;
    }

    public bool ObjectIsOtherPlayer(GameObject other)
    {
        return other == otherPlayer.gameObject;
    }
}
