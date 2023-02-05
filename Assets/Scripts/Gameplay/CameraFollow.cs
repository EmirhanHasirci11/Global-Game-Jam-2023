using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;

    public RoomManager roomManager;
    public GameObject ninjaBoss;
    public float leftEdge;
    public float rightEdge;

    private bool changingScreen;
    private bool bossActivated = false;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        leftEdge = -8.9f;
        rightEdge = 8.9f;
    }

    // Update is called once per frame
    void Update()
    {
        if(ninjaBoss == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        if(target.position.x > rightEdge && !changingScreen)
        {
            changingScreen = true;
            rightEdge = transform.position.x + 17.82f + 8.9f;
            leftEdge = transform.position.x + 17.82f - 8.9f;
            target.position += new Vector3(1f, 0, 0);
            Time.timeScale = 0;
            Debug.Log("right");
            StartCoroutine(GoNextMap(17.82f));
        }
        else if(target.position.x < leftEdge && !changingScreen)
        {
            changingScreen = true;
            leftEdge = transform.position.x - 17.82f - 8.9f;
            rightEdge = transform.position.x - 17.82f + 8.9f;
            target.position += new Vector3(-1f, 0, 0);
            Time.timeScale = 0;
            Debug.Log("left");
            StartCoroutine(GoNextMap(-17.82f));
        }
    }

    private IEnumerator GoNextMap(float newPos)
    {
        Vector3 newPosition = new Vector3(newPos, 0, 0);
        Vector3 currentPos = transform.position;

        if(newPos > 0)
        {
            while (transform.position.x < (currentPos + newPosition).x)
            {
                transform.position += new Vector3(0.09f, 0, 0);
                yield return new WaitForSecondsRealtime(0.001f);
            }
        }
        else
        {
            while (transform.position.x > (currentPos + newPosition).x)
            {
                transform.position -= new Vector3(0.09f, 0, 0);
                yield return new WaitForSecondsRealtime(0.001f);
            }
        }
        changingScreen = false;
        Time.timeScale = 1;
        if(bossActivated == false && roomManager.currentRoom == roomManager.roomBlocks.Length)
        {
            StartCoroutine(SpawnFire());
            ninjaBoss.SetActive(true);
            bossActivated = true;
        }
        yield break;
    }
    public GameObject[] fires;
    IEnumerator SpawnFire()
    {
        fires[0].SetActive(true);
        fires[1].SetActive(true);
        yield return new WaitForSeconds(0.7f);
        fires[2].SetActive(true);
        fires[3].SetActive(true);
        yield return new WaitForSeconds(0.7f);
        fires[4].SetActive(true);
        fires[5].SetActive(true);
    }
}
