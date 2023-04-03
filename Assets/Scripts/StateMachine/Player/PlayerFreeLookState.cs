using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    
    private const float AnimatorDampTime = 0.1f;

    

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private bool standing = true;
    private bool attacking= false;
    public bool sword1;
    public bool sword2;

    public override void Enter()
    {

    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.FreeLookMovementSpeed,deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }

        if (stateMachine.InputReader.IsAttacking == true && attacking)
        {
            stateMachine.Animator.SetBool("Attack1", true);
            attacking = true;
        }
        else
        {
            stateMachine.Animator.SetBool("Attack1", false);
            attacking = true;
        }

        if (stateMachine.InputReader.IsStanding == true)
        {
            stateMachine.Animator.SetBool("Standing",true);
            stateMachine.Animator.SetBool("Standing2", false);
        }

        
        if (stateMachine.InputReader.IsStanding2 == true )
        {
            stateMachine.Animator.SetBool("Standing", false);
            stateMachine.Animator.SetBool("Standing2",true);
            
            attacking =false;
           
        }

        
        


        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);

        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {

    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y +
            right * stateMachine.InputReader.MovementValue.x;
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * stateMachine.RotationDamping);
    }
}
