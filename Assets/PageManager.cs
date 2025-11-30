using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PageManager : MonoBehaviour
{
    public GameObject currentPage;
    public GameObject nextPage;
    public GameObject previousPage;

    public void NextPage()
    {
        currentPage.SetActive(false);
        nextPage.SetActive(true);

    }

    public void PreviousPage()
    {
        currentPage.SetActive(false);
        previousPage.SetActive(true);
    }

}

