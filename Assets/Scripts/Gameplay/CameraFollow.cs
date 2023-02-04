using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;

    private float leftEdge;
    private float rightEdge;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(target.position.x > rightEdge)
        {

        }
        else if(target.position.x < leftEdge)
        {

        }
    }

    private void GoNextMap(Vector3 newPos)
    {
        transform.position += newPos;
    }
}
