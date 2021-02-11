using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAI : MonoBehaviour
{
    public Path path;

    public Transform target;
    [SerializeField][Range(0.0f, 100.0f)]
    public float speed = 0.5f;

    public float nextPointDis = 3f;

    int currentPoint = 0;
    bool reachedEnd = false;

    // Seeker
    Seeker seeker;

    [SerializeField]
    public float timeToCalNewPath = 0.0f;
    public float repeatRate = 0.5f;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", timeToCalNewPath, repeatRate);
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath
            (transform.position, target.position, onPathComplete);
        }
    }

    private void onPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentPoint = 0;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null) return; // If find no path: skip
        print("Path not null");

        if (currentPoint >= path.vectorPath.Count)
        {
            reachedEnd = true;
            return;

        }else
        {
            print("Moving");
            reachedEnd = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentPoint] - (Vector2)transform.position).normalized;
        //Vector3 vector = new Vector3(direction.x, direction.y, 0);
        ////transform.position += vector * Time.deltaTime * speed;
        //Vector2 v = new Vector2(path.vectorPath[currentPoint].x, path.vectorPath[currentPoint].y);
        //transform.position = new Vector3(v.x, v.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, path.vectorPath[currentPoint], speed * Time.deltaTime);

        float distance = Vector2.Distance(transform.position, path.vectorPath[currentPoint]);

        if (distance < nextPointDis)
        {
            currentPoint++;
        }



    }
}
