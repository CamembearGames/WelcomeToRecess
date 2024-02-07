using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class YearBookScript : MonoBehaviour
{
    [SerializeField] private BlendShapeLoop []pageList;
    [SerializeField] private float intervalBetweenPages;
    [SerializeField] private int turnToPage = 0;

    [SerializeField] private GameObject bookCover;

    private int currentPage = 0;    


    private void Start() {
        pageList = GetComponentsInChildren<BlendShapeLoop>();
        System.Array.Reverse(pageList);
    }


    public void OpenBook()
    {
        bookCover.GetComponent<Animator>().SetTrigger("OpenCover");
        Invoke("FlipPages", 1f);
    }

    public void FlipPages()
    {
        bookCover.GetComponent<Animator>().SetTrigger("OpenCover");
        StartCoroutine(turnPages(intervalBetweenPages));
        transform.DOMoveX(-0.076f, 2f);
    }
        
        //StartCoroutine(coroutine);
    private IEnumerator turnPages(float waitTime){
        while(currentPage < turnToPage)
        {
            yield return new WaitForSeconds(waitTime);
            pageList[currentPage].startPageTurn();
            currentPage ++;
        }
    }
}
