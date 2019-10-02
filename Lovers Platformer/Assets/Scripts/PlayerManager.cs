﻿using System.Collections;
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
    [SerializeField] float forceDash;
    void Awake()
    {
        input = GetComponent<PlayerInputManager>();
        characterControl = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
        characterControl.InitSettings(reglages);
        rigidbody = this.GetComponent<Rigidbody2D>();

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

        Debug.DrawLine (otherPlayer.transform.position, this.transform.position, Color.red, 0.01f);
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
        if (input.GetTeleportInputDown())
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
        characterControl.Move(movement.x * Time.fixedDeltaTime * reglages.moveSpeed, jumpTriggerAction);
    }

    void DashAction()
    {

        StartCoroutine("MaCoroutine");
        /*directionDash = (otherPlayer.transform.position - this.GetComponentInParent<Transform>().transform.position).normalized;

        while(!collision)
        {
            
            rigidbody.AddForce(directionDash * forceDash);
        }*/
        
        
        
        






       /*
        //Switch
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
        }*/
    }

    IEnumerator MaCoroutine()
            {
                Vector2 directionDash = directionDash = (otherPlayer.transform.position - this.GetComponentInParent<Transform>().transform.position).normalized;
                this.rigidbody.isKinematic = true;
                rigidbody.gravityScale = 0f;
                this.rigidbody.isKinematic = false;

                while(!collision)
                {
                    
                    rigidbody.AddForce(directionDash * forceDash);
                    yield return new WaitForSeconds(0.01f);
                }

               rigidbody.gravityScale = 2f;
            }

    private void OnCollisionEnter2D(Collision2D other)
    {
        collision = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        collision = false;
    }
}
