using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health;
    public float damage;
    public float speed;
    public float PlayerCheckRadius;
    public float MinDistanceFromPlayer;

    [HideInInspector] public Transform target;
    [SerializeField]
    Rigidbody2D rb;

    private Vector3 startPos;

    private void Awake()
    {
        
        startPos = transform.position;
    }
    public void Move()
    {
        CheckForPlayer();
        if (target != null)
        {
            //Add animation
            if ((transform.position - target.position).magnitude > MinDistanceFromPlayer)
            {
                rb.velocity = (target.position - transform.position).normalized * speed;
            }
            else
            {
                rb.velocity = Vector3.zero;
            }

        }
        else
        {
            if ((transform.position - startPos).magnitude > 0.1)
            {
                rb.velocity = (startPos - transform.position).normalized * speed;
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    //Checks for player in sphere with given radius.
    public void CheckForPlayer()
    {
        foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, PlayerCheckRadius))
        {
            if (col.tag == "Player")
            {
                target = col.transform;
                return;
            }
        }

        target = null;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sword")
        {
            //health -= ...
            //Attack script damage
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color32(0, 0, 0, 100);
        Gizmos.DrawSphere(transform.position, PlayerCheckRadius);
    }
}
