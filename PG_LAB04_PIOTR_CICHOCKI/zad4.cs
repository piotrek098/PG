using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float multiplier = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var mover = other.GetComponent<MoveWithCharacterController>();
        if (mover != null)
        {
            mover.ApplyExternalJumpMultiplier(multiplier);
            Debug.Log("JumpPad: boost applied (CharacterController).");
            return;
        }

        
        var rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(1f * multiplier * 2f), rb.velocity.z);
            Debug.Log("JumpPad: boost applied (Rigidbody).");
        }
    }
}
