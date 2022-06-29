using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    public List<PageDefinition> Pages;
    public GameObject NextButton;
    public GameObject PreviousButton;

    [SerializeField]private int _currentPageIndex;
    
    void Start()
    {
        _currentPageIndex=PlayerPrefs.GetInt("LastPage");
        GetPageAtIndex(_currentPageIndex);
    }

    public void NextPage()
    {
        

        _currentPageIndex++;
        GetPageAtIndex(_currentPageIndex);
        PreviousButton.SetActive(true);

       
    }

    public void PreviousPage()
    {
       

        _currentPageIndex--;
        GetPageAtIndex(_currentPageIndex);
        NextButton.SetActive(true);

        
    }

    private void GetPageAtIndex(int index)
    {
        SetAllPagesInactive();
        Pages[index].Panel.SetActive(true);


        PlayerPrefs.SetInt("LastPage",_currentPageIndex);

        NextButton.SetActive(_currentPageIndex < Pages.Count - 1);


        PreviousButton.SetActive(_currentPageIndex > 0);
    }




    private void SetAllPagesInactive()
    {
        foreach (var page in Pages)
        {
            page.Panel.gameObject.SetActive(false);
        }
    }


}

[Serializable]
public class PageDefinition
{
    public int Index;
    public GameObject Panel;
}

