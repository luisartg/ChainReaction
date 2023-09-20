using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public string levelName;
    public bool checkIfLevelRequired = false;
    public string levelNameRequired;
    private Button button;
    
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadSceneByName);

        if (checkIfLevelRequired)
        {
            if (GameData.Instance.IsThisLevelComplete(levelNameRequired))
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSceneByName()
    {
        SceneManager.LoadScene(levelName);
    }
}
