using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public GameObject title;
    public GameObject credits;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Title Manager was loaded!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SeeCredits()
    {
        title.SetActive(false);
        credits.SetActive(true);
    }

    public void SeeTitle()
    {
        title.SetActive(true);
        credits.SetActive(false);
    }

    public void LoadLevels()
    {
        
    }
}
