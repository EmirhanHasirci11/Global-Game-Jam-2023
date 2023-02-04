using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;
    private Transform target;
    private Health targetHealth;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetHealth = target.gameObject.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!targetHealth.isDead)
            Follow(target);
    }

    private void Follow(Transform target)
    {
        transform.position = target.position + offset;
    }
}
