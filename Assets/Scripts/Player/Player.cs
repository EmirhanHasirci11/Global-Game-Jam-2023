using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private InputActionReference movement, attack, pointerPosition, spin, dash;

    private Vector2 pointerInput, movementInput;
    public Vector2 PointerInput => pointerInput;
    private PlayerMovement playerMovement;
    private WeaponParent weaponParent;
    public bool toCont=true;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        weaponParent = GetComponentInChildren<WeaponParent>();
    }
    private void OnEnable()
    {
        attack.action.performed += PerformAttack;
        spin.action.performed += PerformSpin;
        dash.action.performed += PerformDash;
    }
    private void OnDisable()
    {
        attack.action.performed -= PerformAttack;
        spin.action.performed -= PerformSpin;
        dash.action.performed -= PerformDash;
    }
    private void PerformAttack(InputAction.CallbackContext obj)
    {
        weaponParent.Attack();
    }
    private void PerformSpin(InputAction.CallbackContext obj)
    {
        weaponParent.Spin();
    }
    public void PerformDash(InputAction.CallbackContext obj)
    {
        if (toCont)
        {

        Debug.Log("PerformDash");
        playerMovement.changeBool();
        Debug.Log("PerformDash after");
        }
    }

    void Update()
    {
        pointerInput = GetPointerInput();
        weaponParent.PointerPosition = pointerInput;
        movementInput = movement.action.ReadValue<Vector2>();
        playerMovement.MovementInput = movementInput;



    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
