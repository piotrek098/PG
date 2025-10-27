using UnityEngine;

public class MoveInSquare : MonoBehaviour
{
    public float sideLength = 10f;
    public float speed = 3f;
    public float reachThreshold = 0.05f; 

    private Vector3[] corners = new Vector3[4];
    private int currentCorner = 0;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    
        corners[0] = startPos + new Vector3(sideLength, 0f, 0f);
        corners[1] = corners[0] + new Vector3(0f, 0f, sideLength);
        corners[2] = corners[1] + new Vector3(-sideLength, 0f, 0f);
        corners[3] = corners[2] + new Vector3(0f, 0f, -sideLength);
        currentCorner = 0;
    }

    private void Update()
    {
        Vector3 target = corners[currentCorner];
        Vector3 toTarget = target - transform.position;
        Vector3 move = toTarget.normalized * speed * Time.deltaTime;

      
        if (move.sqrMagnitude >= toTarget.sqrMagnitude)
            transform.position = target;
        else
            transform.position += move;

        if ((transform.position - target).sqrMagnitude <= reachThreshold * reachThreshold)
        {
            
            transform.Rotate(0f, 90f, 0f, Space.World);

           
            currentCorner = (currentCorner + 1) % 4;
        }
    }
}
