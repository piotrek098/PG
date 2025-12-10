using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public SlidingDoor door;
    public enum Side { Left, Right }
    public Side side;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (side == Side.Left) door.OpenLeft();
        else door.OpenRight();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (side == Side.Left) door.CloseLeft();
        else door.CloseRight();
    }
}
