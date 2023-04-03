using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public bool IsAttacking { get; private set;}
    public bool IsStanding { get; private set;}
    public bool IsStanding2 { get; private set; }
    public bool IsDodge { get; private set;}
    public Vector2 MovementValue { get; private set; }

    public GameObject AtHand;
    public GameObject AtBack;

    public event Action JumpEvent;
    public event Action DodgeEvent;

    private Controls controls;

    

    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        

        controls.Player.Enable();
    }

    private void sword1_()
    {
        AtHand.SetActive(true);
        AtBack.SetActive(false);
    }
    private void sword2_()
    {
        AtHand.SetActive(false);
        AtBack.SetActive(true);
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            MovementValue=context.ReadValue<Vector2>()*2;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        
        
        MovementValue = context.ReadValue<Vector2>();
       
        
    }

    public void OnLook(InputAction.CallbackContext context)
    {

    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsAttacking = true;
            
        }
        else if (context.canceled)
        {
            IsAttacking = false;
        }

    }

    public void OnStanding(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsStanding = true;

        }
        else if (context.canceled)
        {
            IsStanding = false;
        }
    }

    public void OnStanding2(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            IsStanding2 = true;

        }
        else if (context.canceled)
        {
            IsStanding2 = false;
        }
    }
}

