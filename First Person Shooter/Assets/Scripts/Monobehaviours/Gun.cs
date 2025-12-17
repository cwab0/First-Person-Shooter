using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GunData gunData;

    [Header("Shooting")]
    [SerializeField] Transform gunPoint;
    [SerializeField] LayerMask layerMask;
    bool gunOnCooldown = false;

    [Header("Visual")]
    [SerializeField] LineRenderer bulletTrace;


    public void Shoot()
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
}