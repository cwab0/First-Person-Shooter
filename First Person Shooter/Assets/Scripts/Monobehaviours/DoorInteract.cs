using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteractable, IDamageable
{
    [SerializeField] Door doorScript;

    public void Interact()
    {
        doorScript.Interact();
    }

    public void Damage(int damageAmount, Vector3 hitPos)
    {
        doorScript.Interact();
    }
}
