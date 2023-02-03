using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public SpriteRenderer weaponRenderer;

    private SpriteRenderer characterRenderer;
    public GameObject playerObject;
    private Vector3 defaultLocalScale;
    public Vector2 PointerPosition { get; set; }

    private void Awake()
    {
        characterRenderer = playerObject.GetComponent<SpriteRenderer>();
        defaultLocalScale = playerObject.transform.localScale;
    }

    private void Update()
    {
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;
        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            playerObject.transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);

            scale.y = -1;
            scale.x = -1;
        }
        else
        {
            playerObject.transform.localScale = new Vector3(defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
            scale.y = 1;
            scale.x = 1;
        }
        transform.localScale = scale;
        //Changes orderInLayer for hiding weapon in some angles
        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {

            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        }
        else
        {

            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }
    }

}
