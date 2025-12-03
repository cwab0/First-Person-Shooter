using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    int hitPoints;
    GameObject centerPoint;
    GameObject[] targetBits;
    Gun gunScript;

    void Start()
    {
        gunScript = GetComponent<Gun>();
    }

    public void Damage(float damageAmount)
    {
        int i;
        i = Mathf.RoundToInt(damageAmount);
        hitPoints -= i;
        if (hitPoints <= 0)
        {
            Break();
        }
    }

    void Break()
    {

    }
}
