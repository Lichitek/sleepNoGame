using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBooster : Pickup
{
    protected void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        FindObjectOfType<Health>().TakeHeal();
    }
}
