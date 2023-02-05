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
}
