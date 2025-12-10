using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable, IDamageable
{
    [SerializeField] GameObject lamp;

    public void Interact()
    {
        if (lamp.activeSelf)
        {
            lamp.SetActive(false);
        }
        else
        {
            lamp.SetActive(true);
        }
    }

    public void Damage(float damage, Vector3 hitPos)
    {
        if (lamp.activeSelf)
        {
            lamp.SetActive(false);
        }
        else
        {
            lamp.SetActive(true);
        }
    }
}
