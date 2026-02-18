using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool isOpen;
    [SerializeField] float doorSpeed;

    [SerializeField] GameObject door1;
    [SerializeField] GameObject door2;

    [SerializeField] Transform closePos1;
    [SerializeField] Transform closePos2;
    [SerializeField] Transform openPos1;
    [SerializeField] Transform openPos2;

    public void Interact()
    {
        if (!isOpen)
        {
            // Opens the door if closed
            isOpen = true;
        }
        else
        {
            // Closes the door if open
            isOpen = false;
        }
    }

    void Update()
    {
        if (isOpen)
        {
            door1.transform.position = Vector3.Lerp(door1.transform.position, openPos1.position, doorSpeed);
            door2.transform.position = Vector3.Lerp(door2.transform.position, openPos2.position, doorSpeed);
        }
        else
        {
            door1.transform.position = Vector3.MoveTowards(door1.transform.position, closePos1.position, doorSpeed);
            door2.transform.position = Vector3.MoveTowards(door2.transform.position, closePos2.position, doorSpeed);
        }
    }
}
