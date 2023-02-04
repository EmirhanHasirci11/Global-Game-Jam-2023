using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float speed;
    public float damage;
    public float lifeTime;

    [SerializeField] private Rigidbody2D rb;
    private float currentLifeTime;

    private void Awake()
    {
        currentLifeTime = lifeTime;
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
