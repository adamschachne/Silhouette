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
        
        for(int i = 3; i < levelManager.levels.Length; i++) {
            
            int levelNumber = i;
            GameObject newButton = Instantiate(buttonPrefab, buttonParent.transform);
    
    /*
            if (i <= 2) {
                
                string textForBtn = "Tutorial " + (i+1).ToString();
                
                newButton.GetComponent<LevelButton>().levelText.text = textForBtn;
                Debug.Log(newButton.GetComponent<LevelButton>().levelText.text + " " + i);
            }
            */
            
            string textForBtn = "Level " + (i-2).ToString();

            newButton.GetComponent<LevelButton>().levelText.text = textForBtn;
            Debug.Log(newButton.GetComponent<LevelButton>().levelText.text + " " + i);
            
            newButton.GetComponent<Button>().onClick.AddListener(() => {
                Debug.Log("Current level " + levelNumber);
                PlayerData.CurrentLevel = levelNumber;
                levelManager.LoadLevel();

            } );//SelectedLevel(i)

            
        }
    }

    private void SelectedLevel(int currLevel) {
        Debug.Log("Current level " + currLevel);
        PlayerData.CurrentLevel = currLevel;
        
        levelManager.LoadLevel();
    }
}
