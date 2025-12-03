using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Gun Data")]
public class GunData : ScriptableObject
{
    [Header("Visual & Effects")]
    public string gunName;
    public GameObject GunGFX;
    public AudioClip soundEffectShoot;
    public AudioClip soundEffectReload;
    public float bulletTraceWidth;
    public int ADS;

    [Header("Main Stats")]
    public int damage;
    public float fireRate;
    public float cooldownTimer;

    [Header("Ammo")]
    public int clipSize;
    public int maxAmmo;
    public int reloadTime;

    [Header("Gun Spread & Recoil")]
    public float spreadIncreasePerShot;
    public float spreadRecoverySpeed;
    public float maxSpread;
    public float recoil;
    public int raising;

}
