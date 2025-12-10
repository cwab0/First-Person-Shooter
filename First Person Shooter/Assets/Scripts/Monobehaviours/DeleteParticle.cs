using System.Collections;
using UnityEngine;

public class DeleteParticle : MonoBehaviour
{
    void Start()
    {
        DeleteCor();
    }

    IEnumerator DeleteCor()
    {
        // check duration of particle system and destroy gameobject
        yield return null;
    }
}
