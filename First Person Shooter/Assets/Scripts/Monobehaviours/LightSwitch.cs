using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable, IDamageable
{
    [SerializeField] Light lamp;

    public void Interact()
    {
        ToggleLight();
    }

    public void Damage(int damage, Vector3 hitPos)
    {
        ToggleLight();
    }

    void ToggleLight()
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