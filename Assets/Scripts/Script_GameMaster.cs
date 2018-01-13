using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class Script_GameMaster : MonoBehaviour {
    public static Script_GameMaster Instance;

    public bool LastGameWon;

    private int TestRun;



    private void Awake()
    {

        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }


    }

    void Start () {
        LastGameWon = false;
        TestRun = 1;
        bool ContinueSearch = true;

        while (ContinueSearch)
        {
            if (File.Exists(Application.dataPath+"/LogFiles/LogFile_" + TestRun + ".txt"))
                {
                TestRun++;
            }
            else
            {
                ContinueSearch = false;
            }
        }
    }
	
	void Update () {
        if (Input.GetKeyDown("return")&& SceneManager.GetActiveScene().name.Contains("Level_"))
        {
            GameObject TestBot = Instantiate(Resources.Load("TestBot")) as GameObject;
            TestBot.gameObject.GetComponent<Script_Bot>().TestRun = TestRun;
        }
    }
}
