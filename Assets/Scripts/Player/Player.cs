using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private InputActionReference movement, attack, pointerPosition, spin, dash, thrust;

    private Vector2 pointerInput, movementInput;
    public Vector2 PointerInput => pointerInput;
    private PlayerMovement playerMovement;
    private WeaponParent weaponParent;
    public bool toCont = true;
    public Image skillcooldown;

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
        thrust.action.performed += PerformThrust;
    }
    private void OnDisable()
    {
        attack.action.performed -= PerformAttack;
        spin.action.performed -= PerformSpin;
        dash.action.performed -= PerformDash;
        thrust.action.performed -= PerformThrust;
    }
    private void PerformAttack(InputAction.CallbackContext obj)
    {
        weaponParent.Attack();
    }
    private void PerformSpin(InputAction.CallbackContext obj)
    {
        weaponParent.Spin();
    }
    private void PerformThrust(InputAction.CallbackContext obj)
    {
        weaponParent.Thrust();
    }
    public void PerformDash(InputAction.CallbackContext obj)
    {
        if (toCont)
        {

            Debug.Log("PerformDash");
            skillcooldown.fillAmount = 1;
            playerMovement.changeBool();
            StartCoroutine(SkillFill(1,skillcooldown));
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

    IEnumerator SkillFill(float time,Image skill)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < time)
        {
            yield return new YieldInstruction();
            elapsedTime += Time.unscaledDeltaTime;
            skill.fillAmount = 1-Mathf.Clamp01(elapsedTime / time);
        }
        skillcooldown.fillAmount = 0;
    }
}
