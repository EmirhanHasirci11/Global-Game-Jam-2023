using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float speed;
    public float damage;
    public float lifeTime;
    public int bounceCount;

    [SerializeField] private Rigidbody2D rb;
    private float currentLifeTime;
    private int currentBounceCount;

    private void Awake()
    {
        currentLifeTime = lifeTime;
        currentBounceCount = bounceCount;
    }

    private void Update()
    {
        currentLifeTime -= Time.deltaTime;
        if(currentLifeTime < 0)
        {
            kill();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().GetHit(damage,gameObject);
            kill();
        }
        else if(collision.collider.tag == "Wall")
        {
            if (currentBounceCount <= 0)
            {
                kill();
                return;
            }

            //Add bounce here;
        }
    }
    public void kill()
    {
        //Add particles maybe
        Destroy(gameObject);
    }
    public void GiveSpeed(Vector3 dir)
    {
        rb.velocity = dir.normalized * speed;
    }
}
