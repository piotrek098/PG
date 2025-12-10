using UnityEngine;
using System.Collections;

public class HorizontalPlatform : MonoBehaviour
{
    public Transform pointB;
    public float speed = 2f;
    public float waitAtPoint = 1f; 

    private Vector3 startPos;
    private Vector3 endPos;
    private bool active = false;
    private bool goingForward = true;

    void Start()
    {
        startPos = transform.position;
        endPos = pointB.position;
    }

    void Update()
    {
        if (!active) return;

        Vector3 target = goingForward ? endPos : startPos;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            
            active = false;
            StartCoroutine(WaitAndReverse());
        }
    }

    IEnumerator WaitAndReverse()
    {
        yield return new WaitForSeconds(waitAtPoint);
        goingForward = !goingForward;
        active = true;
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            active = true;
    }

    
    public void OnPlayerEnterTrigger(Collider other)
    {
        active = true;
    }
}
