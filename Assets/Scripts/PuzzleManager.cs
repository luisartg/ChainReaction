using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public Button resetButton;
    public Button nextButton;
    public GameObject puzzleScreen;
    public GameObject successScreen;
    public string nextLevelName;

    private int concreteBlockCounter = 0;
    private int destroyedBlocks = 0;

    private string[] successMessages = new string[]{
        "Way to go!",
        "Concrete results!",
        "Bomb voyage!",
        "You are the bomb!",
        "You are on fire!",
        "What a blast!"
    };

    private bool ignited = false;
    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(LoadNextLevel);
        resetButton.onClick.AddListener(ResetLevel);
        successScreen.SetActive(false);
        puzzleScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }

    public bool AllowedToIgnite()
    {
        if (!ignited)
        {
            ignited = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RegisterConcreteBlock()
    {
        concreteBlockCounter++;
    }

    public void RegisterConcreteBlockDestroyed()
    {
        destroyedBlocks++;
        if (concreteBlockCounter == destroyedBlocks)
        {
            GameData.Instance.CompleteLevel(SceneManager.GetActiveScene().name);
            puzzleScreen.SetActive(false);
            successScreen.GetComponentInChildren<TextMeshProUGUI>().text = GetRandomMessage();
            successScreen.SetActive(true);
        }
    }

    private string GetRandomMessage()
    {
        int index = UnityEngine.Random.Range(0, successMessages.Length);
        return successMessages[index];
    }
}
