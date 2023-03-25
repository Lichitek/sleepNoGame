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
    public Camera cameraQte;

    public int numQte;
    public bool start;
    public bool end;
    public Animation anim;

    string pressedKey;
    int countQte;
    string randKey;
    bool push;

    void FixedUpdate()
    {
        if(start)
        {
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
        cameraQte.gameObject.SetActive(true);
        start = false;        
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
        }
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
        }
        else
        {
            start = true;
            
        }        
    }
}
