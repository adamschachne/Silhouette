using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBrowser : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject buttonParent;

    private LevelManager levelManager;
    
    private void OnEnable() {
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        
        for (int i = 1; i < levelManager.levels.Length; i++) 
        {    
            int levelNumber = i;
            GameObject newButton = Instantiate(buttonPrefab, buttonParent.transform);

            string textForBtn = "Level " + (i).ToString();

            newButton.GetComponent<LevelButton>().levelText.text = textForBtn;
            newButton.GetComponent<Button>().onClick.AddListener(() => 
            {
                Debug.Log("Current level " + levelNumber);
                PlayerData.CurrentLevel = levelNumber;
                levelManager.LoadLevel();
            });            
        }
    }

    private void SelectedLevel(int currLevel) {
        Debug.Log("Current level " + currLevel);
        PlayerData.CurrentLevel = currLevel;
        
        levelManager.LoadLevel();
    }
}
