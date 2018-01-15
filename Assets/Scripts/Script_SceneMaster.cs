using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#pragma warning disable 0219

public class Script_SceneMaster : MonoBehaviour {

    public int Money, PlayerHealth, EnemyCount;
    public bool Pause,FoundationPlaceable;
    public float GameSpeed,Refund;
    public Text MoneyDisplay, HealthDisplay;
    public Slider TimeScale;
    public GameObject ActiveFoundation, ActiveTower, GMaster;

    public int[] TowerPrices = new int[3];


    public bool HasChanged;

    //public float cd,oldframe;

    private int i;


    void Start()
    {
        //cd = 1;
        Time.timeScale = 1f;
        Pause = false;
        ActiveFoundation = null;
        GMaster = GameObject.Find("GameMaster");
        //GameSpeed = 1f;
        FoundationPlaceable = true;
    }

    public void PauseMenu()
    {
        if (!Pause)
        {
            Debug.Log("pausiert");
            Pause = true;
            Time.timeScale = 0f;
            Time.fixedDeltaTime = 0f;
            //GameSpeed = 0;
            GameObject PauseMenu = Instantiate(Resources.Load("PauseCanvas")) as GameObject;
            Debug.Log("pause durchgelaufen");
        }
    }

    public void Continue()
    {
        Debug.Log("continue");
        Pause = false;
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
        GameSpeed = 1f;
    }

    public void SetHealth(int Amount)
    {
        PlayerHealth += Amount;
    }

    public void SetFoundation(GameObject AFoundation)
    {
        ActiveFoundation = AFoundation;
    }

    public void SetFoundationStatus()
    {
        ActiveFoundation.GetComponent<Script_Foundation>().SetTaken();
    }

	void Update () {
       // GameSpeed = TimeScale.value;
        //Time.timeScale = TimeScale.value;
        //Time.fixedDeltaTime = 0.02f*(1/TimeScale.value);
        //Time.timeScale = TimeScale.value;

        //if (cd <= 0)
        //{
        //    float frame = Time.frameCount-oldframe;
        //    Debug.Log("Sekunde " + i + " Frames " + frame);
        //    oldframe = Time.frameCount;
        //    cd += 1f;
        //    i++;
        //}
        //cd -= Time.deltaTime;

        if (Input.GetKeyDown("escape"))
        {
            PauseMenu();
        }

        MoneyDisplay.text = "Money : " + Money;
        HealthDisplay.text = "Health : " + PlayerHealth;

        if(PlayerHealth <= 0)
        {
            GMaster.GetComponent<Script_GameMaster>().LastGameWon = false;
            SceneManager.LoadScene("EndScene");
        }
        else if(EnemyCount<=0)
        {
            GMaster.GetComponent<Script_GameMaster>().LastGameWon = true;
            SceneManager.LoadScene("EndScene");
        }
    }
}
