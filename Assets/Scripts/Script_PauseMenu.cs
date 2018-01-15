using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_PauseMenu : MonoBehaviour {
    public Script_SceneMaster SMaster;

	void Start () {
        SMaster = GameObject.Find("SceneMaster").GetComponent<Script_SceneMaster>();
    }

    public void Continue()
    {
        SMaster.Pause = false;
        //SMaster.GameSpeed = 1f;
        Time.timeScale = 1f;
        //Object.Destroy(gameObject, 0.3f);
        Destroy(this.gameObject);
    }

    public void Load_Main()
    {
        SceneManager.LoadScene("Hauptmenu");
    }
}
