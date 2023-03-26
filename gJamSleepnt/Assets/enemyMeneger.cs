using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyMeneger : MonoBehaviour
{
    qteGen qte;



    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        qte = (qteGen)FindObjectOfType(typeof(qteGen));
        qte.start = true;
        foreach (Road road in FindObjectsOfType(typeof(Road)))
        {
            road.StopRoad();
        }
       FindObjectOfType(typeof(Player)).GameObject().transform.GetChild(0).GetComponent<Animator>().enabled = false;
        
        qte.end = false;

    }
}
