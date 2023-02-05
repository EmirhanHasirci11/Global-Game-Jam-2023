using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int currentRoom;
    public float radius;
    public Health playerHealth;
    public GameObject[] roomBlocks;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }
    private void FixedUpdate()
    {
        if(AreThereEnemies() == false && currentRoom < roomBlocks.Length)
        {
            if(roomBlocks[currentRoom].activeSelf == true)
            {
                roomBlocks[currentRoom].SetActive(false);
                transform.position += new Vector3(17.8f, 0, 0);
                currentRoom++;
                playerHealth.currentHealth += 15;
                if (playerHealth.currentHealth > playerHealth.maxHealth)
                    playerHealth.currentHealth = playerHealth.maxHealth;
            }
            
        }
    }

    private bool AreThereEnemies()
    {
        foreach (Collider2D item in Physics2D.OverlapBoxAll(transform.position,new Vector2(radius,radius),0))
        {
            if(item.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color32(255, 255, 255, 50);
        Gizmos.DrawCube(transform.position, new Vector2(radius, radius));
    }
}
