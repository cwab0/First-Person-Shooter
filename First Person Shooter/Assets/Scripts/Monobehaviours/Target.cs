using UnityEngine;
using UnityEngine.UIElements;

public class Target : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    [SerializeField] int hitPoints = 30;
    [SerializeField] int bullseyeDistance = 1;
    [SerializeField] int bullseyeAmplifier = 3;

    [Header("Other")]
    [SerializeField] GameObject[] targetBits;


    public void Damage(float damageAmount, Vector3 hitPos)
    {
        // Covert damage to an int
        int i;
        i = Mathf.RoundToInt(damageAmount);

        // Check if bullseye and damage accordingly
        if (Vector3.Distance(gameObject.transform.position, hitPos) < bullseyeDistance)
        {
            hitPoints -= i;
        }
        else
        {
            hitPoints -= i * bullseyeAmplifier;
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
