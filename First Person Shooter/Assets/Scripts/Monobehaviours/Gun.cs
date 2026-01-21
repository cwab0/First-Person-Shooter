using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GunData gunData;

    [Header("Shooting")]
    [SerializeField] Transform gunPoint;
    [SerializeField] LayerMask layerMask;
    bool gunOnCooldown = false;
    bool canShoot = true;
    InputAction shootAction;

    [Header("Reload")]
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] int clipAmmo;
    [SerializeField] int totAmmo;
    InputAction reloadAction;

    [Header("Visual")]
    [SerializeField] LineRenderer bulletTrace;

    void Start()
    {
        reloadAction = InputSystem.actions.FindAction("Reload");
        shootAction = InputSystem.actions.FindAction("Attack");

        clipAmmo = gunData.clipSize;
        totAmmo = gunData.maxAmmo;
    }

    void Update()
    {
        OnShoot();
        HandleReloading();
    }

    #region Shooting
    void OnShoot()
    {
        if (shootAction.IsPressed())
        {
            Shoot();
            Debug.Log("Shoot is pressed");
        }
    }

    public void Shoot()
    {
        if (canShoot)
        {
            Transform mainCam = Camera.main.transform;
            if (Physics.Raycast(mainCam.position, mainCam.forward, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                if (!gunOnCooldown)
                {
                    StartCoroutine(GunCooldownCor());

                    // Debug ray
                    Debug.DrawRay(mainCam.position, mainCam.forward * hit.distance, Color.red, 1);

                    // Damages whatever it hits if it can be damaged
                    IDamageable damageable = hit.collider.GetComponent<IDamageable>();
                    if (damageable != null)
                    {
                        damageable.Damage(gunData.damage, hit.point);
                    }

                    clipAmmo--;

                    // Muzzle flash using particles
                    Instantiate(gunData.muzzleFlash, gunPoint.position, transform.rotation);
                    Instantiate(gunData.hitImpact, hit.point, transform.rotation);

                    bulletTrace.gameObject.SetActive(true);
                    //bulletTrace.positionCount = 2;
                    bulletTrace.SetPosition(0, gunPoint.position); // Start pos of line
                    bulletTrace.SetPosition(1, hit.point); // End pos of line
                    StartCoroutine(GunTraceWaitCor());
                }
            }
        }
    }

    IEnumerator GunCooldownCor()
    {
        gunOnCooldown = true;
        yield return new WaitForSeconds(gunData.fireRate);
        gunOnCooldown = false;
    }

    IEnumerator GunTraceWaitCor()
    {
        for (int i = 0; i <= 3; i++)
        {
            yield return new WaitForFixedUpdate();
            bulletTrace.SetPosition(0, gunPoint.position);
        }
        bulletTrace.gameObject.SetActive(false);
    }
    #endregion

    #region Reload
    void HandleReloading()
    {
        if (reloadAction.WasPressedThisFrame())
        {
            StartCoroutine("ReloadCor");
        }

        if (clipAmmo <= 0)
        {
            canShoot = false;
        }

        ammoText.text = "Ammo: " + clipAmmo + "/" + totAmmo;
    }

    IEnumerator ReloadCor()
    {
        canShoot = false;
        yield return new WaitForSeconds(gunData.reloadTime);
        totAmmo -= (gunData.clipSize - clipAmmo);
        clipAmmo = gunData.clipSize;
        canShoot = true;
    }
    #endregion
}