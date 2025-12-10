using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPlatform : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();
    public float speed = 2f;
    public float waitAtPoint = 0.5f;
    public float arriveThreshold = 0.05f;
    private int index = 0;
    private int direction = 1; 
    private bool moving = false;
    private Transform savedParent;

    void Start()
    {
        if (points.Count == 0)
        {
            Debug.LogWarning("WaypointPlatform: brak punktów w liście");
            enabled = false;
            return;
        }

        transform.position = points[0].position; 
        index = 0;
    }

    void Update()
    {
        if (!moving) return;
        Transform target = points[index];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) <= arriveThreshold)
        {
            moving = false;
            StartCoroutine(Arrived());
        }
    }

    IEnumerator Arrived()
    {
        yield return new WaitForSeconds(waitAtPoint);

        
        if (points.Count > 1)
        {
            if (index == points.Count - 1) direction = -1;
            else if (index == 0) direction = 1;
            index += direction;
        }
        moving = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        savedParent = other.transform.parent;
        other.transform.SetParent(transform);
        moving = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.transform.SetParent(savedParent);
    }
}
