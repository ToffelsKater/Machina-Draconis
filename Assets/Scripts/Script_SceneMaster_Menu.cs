using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_SceneMaster_Menu : MonoBehaviour {

    public GameObject GMaster;
    

	// Use this for initialization
	void Start () {
        GMaster.GetComponent<Script_GameMaster>().LastGameWon = false;
	}

    public void Play()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Load_Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    // Update is called once per frame
    void Update () {

	}
}
