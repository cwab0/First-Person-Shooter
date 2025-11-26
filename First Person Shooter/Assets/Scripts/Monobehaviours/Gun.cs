using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("General")]
    Transform mainCam;
    [SerializeField] GunData gunData;

    [Header("Shooting")]
    [SerializeField] Transform gunPoint;
    [SerializeField] LayerMask layerMask;
    [SerializeField] LineRenderer bulletTrace;

    [SerializeField] GameObject hitImpact;
    void Start()
    {
        mainCam = Camera.main.transform;
    }

    public void Shoot()
    {
        if (Physics.Raycast(mainCam.position, mainCam.forward, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(mainCam.position, mainCam.forward * hit.distance, Color.red, 1);
            Debug.Log("Shoot");

            bulletTrace.gameObject.SetActive(true);

            // Make a small visible object for hitImpact to debug if hit.point is correct
            // Can later be used for particle effect
            Instantiate(hitImpact, hit.point, Quaternion.identity);

            //bulletTrace.positionCount = 2;
            bulletTrace.SetPosition(0, gunPoint.position);
            bulletTrace.SetPosition(1, hit.point);

            StartCoroutine(GunTraceWaitCor());
        }
    }

    IEnumerator GunTraceWaitCor()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        bulletTrace.gameObject.SetActive(false);
    }
}
