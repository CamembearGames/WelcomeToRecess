using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class YearBookScript : MonoBehaviour
{
    [SerializeField] private BlendShapeLoop []pageList;
    [SerializeField] private float intervalBetweenPages;
    [SerializeField] private int turnToPage = 0;

    private int currentPage = 0;    


    private void Start() {
        pageList = GetComponentsInChildren<BlendShapeLoop>();
        StartCoroutine(turnPages(intervalBetweenPages));
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
