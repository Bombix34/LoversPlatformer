using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerInputManager : MonoBehaviour
{
    // The Rewired player id of this character
    public int playerId = 1;

    private Player player; // The Rewired Player

    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
    }

    //MOVEMENT INPUTS__

    public float GetMovementInputX()
    {
        return player.GetAxis("Move Horizontal");
    }

    public float GetMovementInputY()
    {
        return player.GetAxis("Move Vertical");
    }

    public Vector3 GetMovementInput()
    {
        Vector3 move = Vector3.zero;
        move.x = player.GetAxis("Move Horizontal");
        move.y = player.GetAxis("Move Vertical");
        return move;
    }

    //RIGHT STICK INPUTS

    public float GetRightStickX()
    {
        return player.GetAxis("Slash Horizontal");
    }

    public float GetRightStickY()
    {
        return player.GetAxis("Slash Vertical");
    }

    public Vector2 GetRightStickInput()
    {
        return new Vector2(player.GetAxis("Slash Horizontal"), player.GetAxis("Slash Vertical"));
    }

    #region BUTTONS_INPUTS

    public bool GetJumpInputDown()
    {
        return player.GetButtonDown("Jump");
    }

    public bool GetJumpInput()
    {
        return player.GetButton("Jump");
    }

    public bool GetJumpInputUp()
    {
        return player.GetButtonUp("Jump");
    }

    /// <summary>
    /// skill input start with 1 not 0
    /// </summary>
    /// <param name="skillNb"></param>
    /// <returns></returns>
    public bool GetSkillInputDown(int skillNb)
    {
        return player.GetButtonDown("Skill0"+skillNb);
    }

    public bool GetSkillInput(int skillNb)
    {
        return player.GetButton("Skill0" + skillNb);
    }

    public bool GetSkillInputUp(int skillNb)
    {
        return player.GetButtonUp("Skill0" + skillNb);
    }

    public bool GetStartInput()
    {
        return player.GetButton("Start");
    }

    public bool GetStartInputUp()
    {
        return player.GetButtonUp("Start");
    }

    public bool GetPressAnyButton()
    {
        return player.GetAnyButton();
    }

    #endregion

}