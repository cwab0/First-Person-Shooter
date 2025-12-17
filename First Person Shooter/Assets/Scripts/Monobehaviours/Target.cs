using UnityEngine;
using UnityEngine.UIElements;

public class Target : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    [SerializeField] int hitPoints = 30;
    [SerializeField] int bullseyeDistance = 1;
    [SerializeField] int bullseyeAmplifier = 3;

    [Header("Other")]
    [SerializeField] GameObject targetFragmentLarge;
    [SerializeField] GameObject targetFragmentSmall;


    public void Damage(int damageAmount, Vector3 hitPos)
    {
        // Check if bullseye and damage accordingly
        if (Vector3.Distance(gameObject.transform.position, hitPos) < bullseyeDistance)
        {
            hitPoints -= damageAmount;
        }
        else
        {
            hitPoints -= damageAmount * bullseyeAmplifier;
        }

        if (hitPoints <= 0)
        {
            Break();
        }
    }

    void Break()
    {
        Destroy(gameObject);
    }
}
