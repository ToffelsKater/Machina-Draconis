using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_PauseMenu : MonoBehaviour {
    public GameObject SMaster;
	// Use this for initialization
	void Start () {
        SMaster = GameObject.Find("SceneMaster");
    }

    public void Continue()
    {
        SMaster.gameObject.GetComponent<Script_SceneMaster>().Pause = false;
        SMaster.gameObject.GetComponent<Script_SceneMaster>().GameSpeed = 1f;
        Time.timeScale = 1f;
        Object.Destroy(gameObject, 0.3f);
        //Destroy(this.gameObject);
    }

    public void Load_Main()
    {
        SceneManager.LoadScene("Hauptmenu");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
