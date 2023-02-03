using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private InputActionReference movement, attack, pointerPosition;

    private Vector2 pointerInput, movementInput;
    public Vector2 PointerInput => pointerInput;
    private PlayerMovement playerMovement;
    private WeaponParent weaponParent;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        weaponParent = GetComponentInChildren<WeaponParent>();
    }
    private void OnEnable()
    {
        attack.action.performed += PerformAttack;
    }
    private void OnDisable()
    {
        attack.action.performed -=PerformAttack;
    }
    private void PerformAttack(InputAction.CallbackContext obj)
    {
        weaponParent.Attack();
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
