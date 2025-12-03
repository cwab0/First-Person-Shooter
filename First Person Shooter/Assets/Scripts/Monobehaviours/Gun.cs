using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GunData gunData;

    [Header("Shooting")]
    [SerializeField] Transform gunPoint;
    [SerializeField] LayerMask layerMask;
    [SerializeField] LineRenderer bulletTrace;


    public void Shoot()
    {
        Transform mainCam = Camera.main.transform;
        if (Physics.Raycast(mainCam.position, mainCam.forward, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(mainCam.position, mainCam.forward * hit.distance, Color.red, 1);
            Debug.Log("Shoot");

            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(gunData.damage, hit.point);
            }

            // Can later be used for particle effect
            // Instantiate(hitImpact, hit.point, Quaternion.identity);

            bulletTrace.gameObject.SetActive(true);
            //bulletTrace.positionCount = 2;
            bulletTrace.SetPosition(0, gunPoint.position); // Start pos of line
            bulletTrace.SetPosition(1, hit.point); // End pos of line
            StartCoroutine(GunTraceWaitCor());
        }
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