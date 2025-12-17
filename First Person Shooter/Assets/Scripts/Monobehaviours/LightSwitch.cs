using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable, IDamageable
{
    [SerializeField] Light lamp;

    public void Interact()
    {
        if (lamp.enabled == true)
        {
            lamp.enabled = false;
        }
        else
        {
            lamp.enabled = true;
        }
    }

    public void Damage(int damage, Vector3 hitPos)
    {
        if (lamp.enabled == true)
        {
            lamp.enabled = false;
        }
        else
        {
            lamp.enabled = true;
        }
    }
}