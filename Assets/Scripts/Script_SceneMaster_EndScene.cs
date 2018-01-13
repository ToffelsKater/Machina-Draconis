using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Script_SceneMaster_EndScene : MonoBehaviour {

    public GameObject GMaster;
    public Text Status;

	// Use this for initialization
	void Start () {
        GMaster = GameObject.Find("GameMaster");
        if (GMaster.GetComponent<Script_GameMaster>().LastGameWon == true)
        {
            Status.text = "Gewonnen";
        }
        else
        {
            Status.text = "Game Over";
        }
	}

    public void Load_Main()
    {
        SceneManager.LoadScene("Hauptmenu");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
