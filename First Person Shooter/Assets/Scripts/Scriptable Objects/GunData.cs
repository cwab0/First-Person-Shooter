using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Gun Data")]
public class GunData : ScriptableObject
{
    public string gunName;
    public int damage;
    public int maxAmmo;
    public int clipSize;
    public float recoil;
    public GameObject GunGFX;

}
