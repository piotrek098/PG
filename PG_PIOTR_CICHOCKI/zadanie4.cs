using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.81f;
    private Vector3 velocity;

    void Update()
    {
        float h = Input.GetAxis("Horizontal"); 
        float v = Input.GetAxis("Vertical");   

        Vector3 move = new Vector3(h, 0f, v);

        
        transform.position += move.normalized * speed * Time.deltaTime;

        
        if (transform.position.y > 0.5f)
        {
            velocity.y += gravity * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0f;
            
            if (transform.position.y < 0.5f)
                transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }
    }
}
