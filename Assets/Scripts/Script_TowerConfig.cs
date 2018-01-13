using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Script_TowerConfig : MonoBehaviour {
    public static Script_TowerConfig Instance;

    public bool StartClick;
    public GameObject SMaster;
    public GameObject dropdown;
    //public EventSystem eventSystem;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            DestroyPanel();
        }
    }

    void Start () {
        dropdown = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        StartClick = false;
        SMaster= GameObject.Find("SceneMaster");
    }

    public void DestroyPanel()
    {

        //Debug.Log("HideRange from Panel");
        SMaster.GetComponent<Script_SceneMaster>().ActiveTower.GetComponent<Script_Tower>().HideRange();
        SMaster.GetComponent<Script_SceneMaster>().ActiveTower.GetComponent<Script_Tower>().TowerConfig=null;
        SMaster.GetComponent<Script_SceneMaster>().ActiveTower = null;
        SMaster.GetComponent<Script_SceneMaster>().FoundationPlaceable = true;
        Destroy(this.gameObject);

    }
	
    public void SetTowerPreference()
    {
        SMaster = GameObject.Find("SceneMaster");
        SMaster.GetComponent<Script_SceneMaster>().ActiveTower.transform.GetChild(0).GetComponent<Script_Weapon>().Preference = 
            this.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Dropdown>().value;
    }

    public void SellTower()
    {
        
        SMaster.GetComponent<Script_SceneMaster>().ActiveTower.GetComponent<Script_Tower>().Sell();
        DestroyPanel();
    }

    public void UpgradeTower(int UpgradeKind)
    {
        //Debug.Log("Tower Upgrded.TowerConfig");
        SMaster.GetComponent<Script_SceneMaster>().ActiveTower.GetComponent<Script_Tower>().Upgrade(UpgradeKind);
        DestroyPanel();
    }

	void Update () {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            SMaster.GetComponent<Script_SceneMaster>().FoundationPlaceable = false;
        }
        else
        {
            SMaster.GetComponent<Script_SceneMaster>().FoundationPlaceable = true;
        }
        if (Input.GetMouseButtonDown(0) && (!(EventSystem.current.IsPointerOverGameObject())))
        {
            if (StartClick == true)
            //if (cd<=0)
            {
                //Object.Destroy(gameObject, 0.3f);
                DestroyPanel();
            }
            else
            {
                StartClick = true;
            }
        }
    }
}
