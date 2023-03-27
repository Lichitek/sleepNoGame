using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class qteGen : MonoBehaviour
{

    public GameObject countDownImage;
    public GameObject qtePanel;
    public float countDown;
    public List<char> charList=new List<char>();
    public Text currentKey;
    [SerializeField] Camera cameraQte;

    public int numQte;
    public bool start;
    public bool end;
    public Animation anim;

    string pressedKey;
    int countQte;
    string randKey;
    bool push;
    Health healthPlayer;

    private void Start()
    {
        //healthPlayer=(Health)FindObjectOfType(typeof(Health));
        //cameraQte = GameObject.Find("qteCamera").GetComponent<Camera>();
    }


    void FixedUpdate()
    {
        if(start)
        {
            //animator = GameObject.Find("enemy").transform.GetChild(0).GetComponent<Animator>();
            healthPlayer = (Health)FindObjectOfType(typeof(Health));
            GameObject.Find("Player").GetComponent<Player>().enabled = false;
            //cameraQte = GameObject.Find("qteCamera").GetComponent<Camera>();
            qtePanel.SetActive(true);
            StartCoroutine(qteAttack());
        }
        if(push) 
        {
            Debug.Log("push");
            if (Input.GetKey(randKey))
            {
                push = false;
                pressedKey = randKey;
                Debug.Log("DSDSA");

            }
            else 
            {

                push = false;
                Debug.Log("NOOOO"); 
            }
        }
    }


    IEnumerator qteAttack()
    {
        start = false;
        cameraQte.gameObject.SetActive(true);
        List<Animator> animators = new List<Animator>();
        foreach (enemyMeneger enemy in FindObjectsOfType<enemyMeneger>())
        {
            animators.Add(enemy.transform.GetChild(0).GameObject().GetComponent<Animator>());
            //enemy.transform.GetChild(0).GameObject().SetActive(false);
        }

        int randNum=Random.Range(0,charList.Count);
        randKey = charList[randNum].ToString();
        currentKey.text="["+randKey+"]";
        Debug.Log("anim_start");
        anim.Play(); 
        yield return new WaitForSeconds(1f);
        push = true;
        yield return new WaitForSeconds(1.5f);
        push = false;
        Debug.Log("checked");
        if (pressedKey == randKey)
        {
            countDownImage.GetComponent<Image>().color = Color.green;
        }
        else
        {
            countDownImage.GetComponent<Image>().color = Color.red;
            healthPlayer.TakeDamage();
            if (!healthPlayer.IsAlive())
            {
                qtePanel.SetActive(false);
                cameraQte.gameObject.SetActive(false);
                countDownImage.GetComponent<Image>().color = Color.blue;
                yield break;
            }
            foreach (Animator animator in animators)
                animator.SetBool("isAttacking", true);
            yield return new WaitForSeconds(2.5f);
        }
        pressedKey = null;
        yield return new WaitForSeconds(0.5f);
        countQte++;
        Debug.Log(countQte);
        countDownImage.GetComponent<Image>().color = Color.blue;
        if (countQte >= numQte)
        {
            start = false;
            end = true;
            qtePanel.SetActive(false);
            cameraQte.gameObject.SetActive(false);
            countQte = 0;
            foreach (Road road in FindObjectsOfType(typeof(Road)))
            {
                road.resumeRoad();
            }
            FindObjectOfType(typeof(Player)).GameObject().transform.GetChild(0).GetComponent<Animator>().enabled = true;
            GameObject.Find("Player").GetComponent<Player>().enabled = true;

            foreach (Animator animator in animators)
                animator.SetBool("isAttacking", false);
            foreach (enemyMeneger enemy in FindObjectsOfType<enemyMeneger>())
            {
                enemy.transform.GetChild(0).GameObject().SetActive(false);
            }
            yield return new WaitForSeconds(3f);
            foreach (enemyMeneger enemy in FindObjectsOfType<enemyMeneger>())
            {
                enemy.transform.GetChild(0).GameObject().SetActive(true);
            }
        }
        else
        {
            start = true;
        }        
    }
}
