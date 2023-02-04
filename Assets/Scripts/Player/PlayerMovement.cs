using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    [SerializeField]
    private float maxSpeed = 3, acceleration = 50, deacceleration = 100;
    [SerializeField]
    private Player player;
    private float currentSpeed = 0;
    private Vector2 oldMovementInput;
    public Vector2 MovementInput;
    [SerializeField] private float dashPower;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCoolDown;
    public bool controlBool;
    private bool isDashing;
    private bool canDash = true;
    void Awake()
    {
        player = GetComponent<Player>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {

        if (MovementInput.magnitude > 0 && currentSpeed >= 0)
        {
            oldMovementInput = MovementInput;
            currentSpeed += acceleration * maxSpeed * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deacceleration * maxSpeed * Time.deltaTime;
        }

        if(!isDashing)
        {
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
            rigidbody.velocity = oldMovementInput * currentSpeed;
        }
        if (canDash && controlBool) { StartCoroutine(performDash()); }
    }
    public void changeBool()
    {
        controlBool = true;
    }
    public IEnumerator performDash()
    {
        Debug.Log("PlayerMovementDash");

        player.toCont = false;
        canDash = false;
        controlBool = false;
        isDashing = true;
        Physics2D.IgnoreLayerCollision(6, 7, true);
        Physics2D.IgnoreLayerCollision(6, 9, true);

        Debug.Log(new Vector2(oldMovementInput.x * dashPower, oldMovementInput.y * dashPower));
        rigidbody.velocity = new Vector2(oldMovementInput.x * dashPower, oldMovementInput.y * dashPower);
        Debug.Log(dashTime);
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        Debug.Log(dashCoolDown);
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Physics2D.IgnoreLayerCollision(6, 9, false);

        yield return new WaitForSeconds(dashCoolDown);
        player.toCont = true;
        canDash = true;

    }

}
