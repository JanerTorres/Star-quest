using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraPlanetas : MonoBehaviour
{
    public Transform target;
    public float velocidad;


    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, velocidad*Time.deltaTime);
    }

    public void SeguirPlaneta()
    {

    }
}
