using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_SceneMaster_LevelSelect : MonoBehaviour {

    public void LoadLevel(int Level)
    {
        if (Level <= 9)
        {
            SceneManager.LoadScene("Level_0" + Level);
        }
        else
        {
            SceneManager.LoadScene("Level_" + Level);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Hauptmenu");
    }

    public void Welt2()
    {
        SceneManager.LoadScene("Welt_2");
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
