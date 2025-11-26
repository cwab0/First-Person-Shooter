using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Gun Data")]
public class GunData : ScriptableObject
{
    string gunName;
    int damage;
    int maxAmmo;
    int clipSize;
    float recoil;
    GameObject GunGFX;

}
