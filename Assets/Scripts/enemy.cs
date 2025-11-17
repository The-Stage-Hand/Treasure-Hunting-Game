using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
	public static int hp = 2;
	public static float movespeed = 2f;
	public Transform[] patrolPoints;
	public float waitTime;
	int currentPointIndex;

    // Use this for initialization
    bool once;
    void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
        //Enemy will walk in small pattern, waiting for player
        if (transform.position != patrolPoints[currentPointIndex].position)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, movespeed * Time.deltaTime);
        }
        else
        {
            if (once == false)
            {
                once = true;
                StartCoroutine(Wait());
            }
        }
    }

	void OnTriggerEnter2D(Collider2D other)
	{
        //FIXME: add parameter to die to sword swing-
	}

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        if (currentPointIndex + 1 < patrolPoints.Length)
        {
            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }
        once = false;
    }
}
