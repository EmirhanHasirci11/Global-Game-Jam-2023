using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float LifeTime;
    public float damage;


    private void Start()
    {
        Invoke("kill", LifeTime);
    }

    public void kill()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().GetHit(damage, gameObject);
            kill();
        }
        else if(collision.collider.tag == "Wall")
        {
            kill();
        }
    }
}
