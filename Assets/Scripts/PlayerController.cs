using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : StarterAssets.ThirdPersonController
{
    [SerializeField] float startingMoveSpeed;
    [SerializeField] float startingSprintSpeed;
    [SerializeField] float speedReduction;
    [SerializeField] bool canJump;

    void OnEnable()
    {
        Challenge.OnDisableJump += DisablePlayerJump;
        Challenge.OnEnableJump += EnablePlayerJump;
        Challenge.OnReducePlayerSpeed += ReducePlayerSpeed;
        Challenge.OnRestorePlayerSpeed += RestorePlayerSpeed;
        HintTrigger.OnReducePlayerSpeed += ReducePlayerSpeed;
        HintTrigger.OnRestorePlayerSpeed += RestorePlayerSpeed;
    }

    void OnDisable()
    {
        Challenge.OnDisableJump -= DisablePlayerJump;
        Challenge.OnEnableJump -= EnablePlayerJump;
        Challenge.OnReducePlayerSpeed -= ReducePlayerSpeed;
        Challenge.OnRestorePlayerSpeed -= RestorePlayerSpeed;
        HintTrigger.OnReducePlayerSpeed -= ReducePlayerSpeed;
        HintTrigger.OnRestorePlayerSpeed -= RestorePlayerSpeed;
    }

    void DisablePlayerJump() => canJump = false;

    void EnablePlayerJump() => canJump = true;

    protected override void JumpAndGravity()
    {
        if (!canJump)
        {
            Grounded = true;
        }
        else
        {
            base.JumpAndGravity();
        }
    }

    void ReducePlayerSpeed()
    {
        startingMoveSpeed = MoveSpeed;
        startingSprintSpeed = SprintSpeed;
        MoveSpeed = startingMoveSpeed / speedReduction;
        SprintSpeed = startingSprintSpeed / speedReduction;
    }

    void RestorePlayerSpeed()
    {
        MoveSpeed = startingMoveSpeed;
        SprintSpeed = startingSprintSpeed;
    }
}
