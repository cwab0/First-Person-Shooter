using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform gunPoint;

    [SerializeField] LayerMask layerMask;

    [SerializeField] LineRenderer bulletTrace;

    Transform mainCam;

    void Start()
    {
        mainCam = Camera.main.transform;
    }

    public void Shoot()
    {
        if (Physics.Raycast(mainCam.position, mainCam.forward, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(mainCam.position, mainCam.forward * hit.distance, Color.red, 1);

            bulletTrace.gameObject.SetActive(true);

            //bulletTrace.positionCount = 2;
            bulletTrace.SetPosition(0, gunPoint.position);
            bulletTrace.SetPosition(1, hit.point);

            StartCoroutine(GunTraceWaitCor());
        }
    }

    IEnumerator GunTraceWaitCor()
    {
        yield return new WaitForFixedUpdate();
        bulletTrace.gameObject.SetActive(false);
    }
}
