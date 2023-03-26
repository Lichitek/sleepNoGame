using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float _rotationAngle;
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0f, _rotationAngle, 0f));
    }

    protected void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
