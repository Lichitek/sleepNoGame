using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombMeneg : MonoBehaviour
{
    public float waitForExplosion;
    public Animation anim;
    public GameObject gb;

    bool start;
    bool changCol;
    public float timer;
    void Start()
    {
        changCol = false;
        start = true;
    }

    
    void FixedUpdate()
    {
        if (timer < waitForExplosion && start && !changCol)
        {            
            StartCoroutine(aninExplosion());

        }
        else if(timer < waitForExplosion && start)
        {
            timer += Time.fixedDeltaTime;
            anim.Play();
        }
        else
        {
            Destroy(this.gameObject);
            timer = 0;
            start = false;
        }

    }

    IEnumerator aninExplosion()
    {
        changCol = true;
        yield return new WaitForSeconds(2f);
        gb.GetComponent<Renderer>().material.color = Color.red;       
        yield return new WaitForSeconds(1f);        
        gb.GetComponent<Renderer>().material.color = Color.white;
        changCol = false;
       //timer += 3;
        yield return null;


    }

    private void OnTriggerEnter(Collider other)
    {
       // Destroy(other.gameObject);
       // Destroy(this.gameObject);

    }
}
