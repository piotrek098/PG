using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    [Header("Door parts")]
    public Transform leftDoor;    /
    public Transform rightDoor;   

    [Header("Settings")]
    public float openAmount = 1.5f; 
    public float speed = 2f;

    
    private Vector3 leftClosedLocal;
    private Vector3 leftOpenLocal;
    private Vector3 rightClosedLocal;
    private Vector3 rightOpenLocal;

    private bool leftTargetOpen = false;
    private bool rightTargetOpen = false;

    void Start()
    {
        if (leftDoor == null || rightDoor == null)
        {
            Debug.LogError("SlidingDoor: przypisz leftDoor i rightDoor.");
            enabled = false;
            return;
        }

      
        leftClosedLocal = leftDoor.localPosition;
        rightClosedLocal = rightDoor.localPosition;

       
        leftOpenLocal = leftClosedLocal + Vector3.left * Mathf.Abs(openAmount);
        rightOpenLocal = rightClosedLocal + Vector3.right * Mathf.Abs(openAmount);
    }

    void Update()
    {
        Vector3 desiredLeft = leftTargetOpen ? leftOpenLocal : leftClosedLocal;
        leftDoor.localPosition = Vector3.MoveTowards(leftDoor.localPosition, desiredLeft, speed * Time.deltaTime);

        
        Vector3 desiredRight = rightTargetOpen ? rightOpenLocal : rightClosedLocal;
        rightDoor.localPosition = Vector3.MoveTowards(rightDoor.localPosition, desiredRight, speed * Time.deltaTime);
    }

  
    public void OpenLeft()  => leftTargetOpen = true;
    public void CloseLeft() => leftTargetOpen = false;
    public void OpenRight() => rightTargetOpen = true;
    public void CloseRight()=> rightTargetOpen = false;
}
