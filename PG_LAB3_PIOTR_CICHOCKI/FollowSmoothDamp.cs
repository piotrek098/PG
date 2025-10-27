using UnityEngine;


public class FollowSmoothDamp : MonoBehaviour
{
    public Transform target;        
    public float smoothTime = 0.3f; 
    public Vector3 offset = Vector3.zero; 

    private Vector3 velocity = Vector3.zero; 

    void Update()
    {
        if (target == null) return;

        Vector3 targetPos = target.position + offset;
        
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
