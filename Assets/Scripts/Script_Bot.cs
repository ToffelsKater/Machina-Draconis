using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;


#pragma warning disable 0219
public class Script_Bot : MonoBehaviour {
    public static Script_Bot Instance;
    public int TestRun,Level;
    public string LvlName;


    private int Round;
    private string LastScene;
    private GameObject SMaster;


    StreamWriter writer;
    public GameObject[] Foundations;
    public Transform[] Towers = new Transform[3];





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

    void Start() {
        LastScene = null;

            LvlName = SceneManager.GetActiveScene().name;


        Foundations = GameObject.FindGameObjectsWithTag("Base");
        writer = File.AppendText(Application.dataPath+"/LogFiles/LogFile_" + TestRun + ".txt");
        writer.WriteLine("Testdurchlauf " + TestRun);
        writer.Close();
    }

	
	void Update () {
        writer = File.AppendText(Application.dataPath+"/LogFiles/LogFile_" + TestRun + ".txt");
        System.Random random = new System.Random();
        int number = random.Next(Foundations.Length);
        

        if(SceneManager.GetActiveScene().name != LastScene)
        {
            SMaster = GameObject.Find("SceneMaster");

            if(SceneManager.GetActiveScene().name.Contains("Level"))
            {
                writer.WriteLine("   Runde " + Round);
                Foundations = GameObject.FindGameObjectsWithTag("Base");
            }
            if (SceneManager.GetActiveScene().name == "EndScene")
            {
                Round++;
                SceneManager.LoadScene("Hauptmenu");
            }
            if (SceneManager.GetActiveScene().name == "Hauptmenu")
            {

                    SceneManager.LoadScene(LvlName);
                
            }
        }
            LastScene = SceneManager.GetActiveScene().name;
        

        if (SceneManager.GetActiveScene().name.Contains("Level_"))
        {
            //SMaster.gameObject.GetComponent<Script_SceneMaster>().GameSpeed = 16f;
            if (!Foundations[number].gameObject.GetComponent<Script_Foundation>().Taken && SMaster.gameObject.GetComponent<Script_SceneMaster>().Money>= SMaster.gameObject.GetComponent<Script_SceneMaster>().TowerPrices[0])
            {
                Foundations[number].gameObject.GetComponent<Script_Foundation>().Taken = true;
                Transform Tower = Instantiate(Towers[0], Foundations[number].gameObject.transform.position, Foundations[number].gameObject.transform.rotation);
                SMaster.gameObject.GetComponent<Script_SceneMaster>().Money -= SMaster.gameObject.GetComponent<Script_SceneMaster>().TowerPrices[0];
                writer.WriteLine("      Turm platziert auf " + Foundations[number].gameObject.name);
            }
        }
        writer.Close();
	}
}
