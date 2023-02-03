using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public SpriteRenderer weaponRenderer;

    private SpriteRenderer characterRenderer;
    public GameObject playerObject;
    private Vector3 defaultLocalScale;
    public Vector2 PointerPosition { get; set; }
    public Animator animator;
    public float delay = 0.1f;
    public bool attackBlocked;
    public bool IsAttacking { get; private set; }

    public void ResetIsAttacking() 
    {
        IsAttacking = false;
     }
    private void Awake()
    {
        characterRenderer = playerObject.GetComponent<SpriteRenderer>();
        defaultLocalScale = playerObject.transform.localScale;
    }

    private void Update()
    {
        if (IsAttacking)
            return;
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;
        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            playerObject.transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);

            scale.y = -1;
            scale.x = -1;
        }
        else
        {
            playerObject.transform.localScale = new Vector3(defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
            scale.y = 1;
            scale.x = 1;
        }
        transform.localScale = scale;
        //Changes orderInLayer for hiding weapon in some angles
        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {

            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        }
        else
        {

            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }
    }
    public void Attack()
    {
        if (attackBlocked)
            return;
        animator.SetTrigger("Attack");
        IsAttacking = true;
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }
}
