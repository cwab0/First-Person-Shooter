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
        if (Physics.Raycast(gunPoint.position, mainCam.forward, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(gunPoint.position, mainCam.forward * hit.distance, Color.red, 1);
            Debug.Log("SHOOT");
            //hit.point;

            bulletTrace.positionCount = 2;
            bulletTrace.SetPositions(new[] { gunPoint.position, hit.point });
        }
    }
}
