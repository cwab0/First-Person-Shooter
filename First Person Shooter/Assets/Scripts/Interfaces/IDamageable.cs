using UnityEngine;

public interface IDamageable
{
    // Use this on every script that handles damage for any object
    void Damage(int damageAmount, Vector3 hitPos);
}