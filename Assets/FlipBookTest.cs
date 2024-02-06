using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlipBookTest : MonoBehaviour
{
    [SerializeField] private AutoFlip bookAutoNode;
    [SerializeField] private int pagesToFlip;

    //[SerializeField] private AudioSource bookNoise;


    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        pagesToFlip = 0;
        //Invoke("turnPage",1f);
        //coroutine = WaitAndPrint(0.1f);
        //StartCoroutine(coroutine);
    }


    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (pagesToFlip<3)
        {
            yield return new WaitForSeconds(waitTime);
            pagesToFlip ++;
            bookAutoNode.FlipRightPage();
        }
    }
}
