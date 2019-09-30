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

    bool jump = false;

    void Awake()
    {
        input = GetComponent<PlayerInputManager>();
        characterControl = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateInput();
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        characterControl.Move(movement.x * Time.fixedDeltaTime * reglages.moveSpeed, false, jump);
        jump = false;
    }

    void UpdateInput()
    {
        movement = new Vector2(input.GetMovementInputX(),input.GetMovementInputY());
        if (input.GetJumpInputDown())
        {
            jump = true;
        }
        if (input.GetSpecialInput())
            print("special");
        if (input.GetTeleportInput())
            TeleportAction();
    }

    void UpdateAnimator()
    {
        anim.SetBool("Run", characterControl.IsGrounded() && movement.x != 0);
        anim.SetBool("Jump", jump);
        anim.SetBool("Falling", characterControl.IsFalling());
    }

    void MovementUpdate()
    {

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
