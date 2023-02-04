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
        leftEdge = -8.4f;
        rightEdge = 8.4f;
    }

    // Update is called once per frame
    void Update()
    {
        if(target.position.x > rightEdge)
        {
            GoNextMap(17.8f);
            rightEdge = transform.position.x + 8.4f;
        }
        else if(target.position.x < leftEdge)
        {
            GoNextMap(-17.8f);
            leftEdge = transform.position.x - 8.4f;
        }
    }

    private void GoNextMap(float newPos)
    {
        transform.position += new Vector3(newPos,0,0);
    }
}
