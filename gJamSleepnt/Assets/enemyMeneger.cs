using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMeneger : MonoBehaviour
{
    public qteGen qte;
    public float speed;
    public Collider triggerZone;
    float preSpeed;


    void Start()
    {
        preSpeed = speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position -= new Vector3(0, 0, speed);
        if(qte.end)
            speed = preSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Player"))
        {
            qte.start = true;
            qte.end = false;
            speed = speed / 3000;
        }
    }
}
