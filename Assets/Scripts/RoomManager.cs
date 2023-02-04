using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomManager : MonoBehaviour
{
    public int currentRoom;
    public float radius;
    public GameObject[] roomBlock;

    private void FixedUpdate()
    {
        
    }

    public bool CheckForEnemies()
    {
        foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, radius))
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                return false;
            }
        }

        return true;
    }
}
