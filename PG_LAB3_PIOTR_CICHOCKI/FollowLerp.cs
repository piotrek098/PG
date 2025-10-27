using UnityEngine;


public class FollowLerp : MonoBehaviour
{
    public Transform target;
    public float lerpSpeed = 3f;     
    public Vector3 offset = Vector3.zero;

    void Update()
    {
        if (target == null) return;

        Vector3 targetPos = target.position + offset;
      
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
    }
}
