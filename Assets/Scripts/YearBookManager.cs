using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class YearBookManager : MonoBehaviour
{
    [SerializeField] private YearBookScript book;
    [SerializeField] private TextMeshProUGUI yearText;

    public void OpenBook()
    {
        book.OpenBook();
        Invoke("BookFinishedOpening", 4f);
    }

    private void BookFinishedOpening()
    {
        yearText.DOFade(1f, 1f);
    }

}
