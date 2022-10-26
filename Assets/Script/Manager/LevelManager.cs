using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{   

    int levelsUnlocked;
    

    public Button[] buttons;


    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 2);

        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < levelsUnlocked; i++)
        {
            buttons[i].interactable = true;
        }
        Debug.Log(levelsUnlocked);
    }

    public void nextlvl(int index)
    {
        SceneManager.LoadScene(index);
    }
    
}
