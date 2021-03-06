﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Script_TowerConfig : MonoBehaviour {
    public static Script_TowerConfig Instance;

    public bool StartClick;
    public Script_SceneMaster SMaster;
    public Script_Tower TowerScript;
    public Script_Weapon WeaponScript;
    public GameObject dropdown;

    private Script_Variables Variables;

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
        SMaster= GameObject.Find("SceneMaster").GetComponent<Script_SceneMaster>();
        TowerScript = SMaster.ActiveTower.GetComponent<Script_Tower>();
        WeaponScript = SMaster.ActiveTower.transform.GetChild(0).GetComponent<Script_Weapon>();
        Variables= GameObject.Find("SceneMaster").GetComponent<Script_Variables>();

        

    }

    private void ButtonInteractable (int UpgradeKind)
    {
        if (SMaster.Money < Variables.TowerPrices[WeaponScript.TowerTier - 1] * (float)(Variables.UpgradePrices[TowerScript.Upgrades[UpgradeKind]] / 100f))
        {
            this.gameObject.transform.GetChild(0).GetChild(UpgradeKind + 2).GetComponent<Button>().interactable = false;
        }
        else
        {
            this.gameObject.transform.GetChild(0).GetChild(UpgradeKind + 2).GetComponent<Button>().interactable = true;
        }
    }

    public void DestroyPanel()
    {
        SMaster.ActiveTower.GetComponent<Script_Tower>().HideRange();
        SMaster.ActiveTower.GetComponent<Script_Tower>().TowerConfig=null;
        SMaster.ActiveTower = null;
        SMaster.FoundationPlaceable = true;
        Destroy(this.gameObject);
    }
	
    public void SetTowerPreference()
    {
        SMaster = GameObject.Find("SceneMaster").GetComponent<Script_SceneMaster>();
        SMaster.ActiveTower.transform.GetChild(0).GetComponent<Script_Weapon>().Preference = 
            this.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Dropdown>().value;
    }

    public void SellTower()
    {
        
        SMaster.ActiveTower.GetComponent<Script_Tower>().Sell();
        DestroyPanel();
    }

    public void UpgradeTower(int UpgradeKind)
    {
        SMaster.ActiveTower.GetComponent<Script_Tower>().Upgrade(UpgradeKind);
        DestroyPanel();
    }

	void Update () {
        for (int i = 0; i <= 2; i++)
        {
            if ((TowerScript.Upgrades[i] < 3))
            {
                ButtonInteractable(i);
            }
            else
            {
                this.gameObject.transform.GetChild(0).GetChild(i + 2).GetComponent<Button>().interactable = false;
            }
        }
        if (EventSystem.current.IsPointerOverGameObject())
        {
            SMaster.FoundationPlaceable = false;
        }
        else
        {
            SMaster.FoundationPlaceable = true;
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
