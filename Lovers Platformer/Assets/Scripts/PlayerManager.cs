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

    bool jumpTriggerAction = false;
    bool isJumping = false;
    bool jumpRelease = true;

    void Awake()
    {
        input = GetComponent<PlayerInputManager>();
        characterControl = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
        characterControl.InitSettings(reglages);
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
        if (input.GetTeleportInput())
            print("Teleport");
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
        characterControl.Move(movement.x * Time.fixedDeltaTime * reglages.moveSpeed, jumpTriggerAction);
    }

    void TeleportAction()
    {
        Vector2 currentPosition = transform.position;
        if (isPlayer1)
        {
            GameObject otherPlayer = GameManager.Instance.GetPlayer2().gameObject;
            transform.position = otherPlayer.transform.position;
            GameManager.Instance.GetPlayer2().transform.position = currentPosition;
        }
        else
        {
            GameObject otherPlayer = GameManager.Instance.GetPlayer1().gameObject;
            transform.position = otherPlayer.transform.position;
            GameManager.Instance.GetPlayer1().transform.position = currentPosition;
        }
    }
}
