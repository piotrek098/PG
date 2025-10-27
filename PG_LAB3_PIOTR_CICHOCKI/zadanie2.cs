using UnityEngine;

public class MoveAlongX : MonoBehaviour
{
    public float speed = 3f;       
    public float distance = 10f;   
    
    private Vector3 startPos;
    private Vector3 targetPos;
    private int direction = 1;    

    private void Start()
    {
        startPos = transform.position;
        targetPos = startPos + Vector3.right * distance;
    }

    private void Update()
    {
       
        Vector3 moveDir = (targetPos - transform.position).normalized;
        transform.position += moveDir * speed * Time.deltaTime;

       
        if ((transform.position - targetPos).sqrMagnitude <= 0.001f)
        {
            
            if (direction == 1)
            {
                targetPos = startPos;
                direction = -1;
            }
            else
            {
                targetPos = startPos + Vector3.right * distance;
                direction = 1;
            }
        }
    }
}
