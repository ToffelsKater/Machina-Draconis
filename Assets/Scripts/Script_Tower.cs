using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//[System.Serializable]
public class Script_Tower : MonoBehaviour {
    [HideInInspector]
    public GameObject Foundation;
 

    [Header("Upgrade Settings")]
    public UpgradeValues[] UpgradePercent = new UpgradeValues[3];
    [Space(20)]

    [HideInInspector]
    public GameObject RangeIndicator;
    [HideInInspector]
    public GameObject TowerConfig;

    private bool ContinueCd;
    private float ToolTipDelay,Countdown;
    private Script_SceneMaster SMaster;
    private Script_Weapon WeaponScript;
    private Script_Variables Variables;

    public int[] Upgrades = new int[3];
    

    List<string> DropOptions = new List<string> { "Slow Verteilung" };


    [System.Serializable]
    public struct UpgradeValues
    {
        public int FirstUpgradePercent,
                   SecondUpgradePercent,
                   ThirdUpgradePercent;

    }


	void Start () {
        ToolTipDelay = 0.3f;
        SMaster= GameObject.Find("SceneMaster").GetComponent<Script_SceneMaster>();
        WeaponScript = this.transform.GetChild(0).GetComponent<Script_Weapon>();
        Variables = GameObject.Find("SceneMaster").GetComponent<Script_Variables>();
        RangeIndicator = null;
        Countdown = ToolTipDelay;


    }

    private void ShowRange()
    {
        if (RangeIndicator == null)
        {
            float Range = this.transform.GetChild(0).GetComponent<Script_Weapon>().range;
            RangeIndicator = Instantiate(Resources.Load("RangeIndicator"), this.transform.position, this.transform.rotation) as GameObject;
            RangeIndicator.transform.localScale = new Vector3(Range * 2, Range * 2, 0);
        }
    }

    public void HideRange()
    {
        Destroy(RangeIndicator.gameObject);
        RangeIndicator = null;
    }

    public void OnMouseDown()
    {
        if (SMaster.ActiveTower == null)
        {
            SMaster.ActiveTower = this.gameObject;
            TowerConfig = Instantiate(Resources.Load("Canvas TowerConfig")) as GameObject;

            if (this.transform.GetChild(0).GetComponent<Script_Weapon>().TowerTier == 2)
            {
                TowerConfig.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Dropdown>().AddOptions(DropOptions);
            }

            TowerConfig.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Dropdown>().value = this.transform.GetChild(0).GetComponent<Script_Weapon>().Preference;
            ShowRange();
        }
    }

    public void Sell()
    {
        Foundation.GetComponent<Script_Foundation>().Taken = false;
        Foundation.GetComponent<BoxCollider2D>().enabled = true;
        SMaster.Money += (int)(SMaster.TowerPrices[WeaponScript.TowerTier-1] * SMaster.Refund + 0.5f);
        Destroy(this.gameObject);
    }

    public void Upgrade(int UpgradeKind)
    {
        switch (UpgradeKind)
        {
            case 0:
                if (Upgrades[0] <= 2)
                {                  
                    switch (Upgrades[0])
                    {
                        case 0:
                            Debug.Log("upgrade");
                            if (SMaster.Money >= Variables.TowerPrices[WeaponScript.TowerTier - 1] * (float)(Variables.UpgradePrices[Upgrades[0]] / 100f))
                            {
                                SMaster.Money -= (int)(Variables.TowerPrices[WeaponScript.TowerTier - 1] * (float)(Variables.UpgradePrices[Upgrades[0]] / 100f));
                                WeaponScript.range += (WeaponScript.range / 100) * UpgradePercent[0].FirstUpgradePercent;
                            }
                            break;
                        case 1:
                            if (SMaster.Money >= Variables.TowerPrices[WeaponScript.TowerTier - 1] * (float)(Variables.UpgradePrices[Upgrades[0]] / 100f))
                            {
                                SMaster.Money -= (int)(Variables.TowerPrices[WeaponScript.TowerTier - 1] * (float)(Variables.UpgradePrices[Upgrades[0]] / 100f));
                                WeaponScript.range += (WeaponScript.range / 100) * UpgradePercent[0].SecondUpgradePercent;
                            }
                            break;
                        case 2:
                            if (SMaster.Money >= Variables.TowerPrices[WeaponScript.TowerTier - 1] * (float)(Variables.UpgradePrices[Upgrades[0]] / 100f))
                            {
                                SMaster.Money -= (int)(Variables.TowerPrices[WeaponScript.TowerTier - 1] * (float)(Variables.UpgradePrices[Upgrades[0]] / 100f));
                                WeaponScript.range += (WeaponScript.range / 100) * UpgradePercent[0].ThirdUpgradePercent;
                            }
                            break;
                    }
                    Upgrades[0]++;
                }
                break;
            case 1:
                if (Upgrades[1] <= 2)
                {
                    
                    switch (Upgrades[1])
                    {
                        case 0:
                            if (SMaster.Money >= Variables.TowerPrices[WeaponScript.TowerTier - 1] * (float)(Variables.UpgradePrices[Upgrades[1]] / 100f))
                            {
                                SMaster.Money -= (int)(Variables.TowerPrices[WeaponScript.TowerTier - 1] * (float)(Variables.UpgradePrices[Upgrades[1]] / 100f));
                                WeaponScript.ShootsPerSec += (WeaponScript.ShootsPerSec / 100) * UpgradePercent[1].FirstUpgradePercent;
                            }
                            break;
                        case 1:
                            if (SMaster.Money >= Variables.TowerPrices[WeaponScript.TowerTier - 1] * (float)(Variables.UpgradePrices[Upgrades[1]] / 100f))
                            {
                                SMaster.Money -= (int)(Variables.TowerPrices[WeaponScript.TowerTier - 1] * (float)(Variables.UpgradePrices[Upgrades[1]] / 100f));
                                WeaponScript.ShootsPerSec += (WeaponScript.ShootsPerSec / 100) * UpgradePercent[1].SecondUpgradePercent;
                            }
                            break;
                        case 2:
                            if (SMaster.Money >= Variables.TowerPrices[WeaponScript.TowerTier - 1] * (float)(Variables.UpgradePrices[Upgrades[1]] / 100f))
                            {
                                SMaster.Money -= (int)(Variables.TowerPrices[WeaponScript.TowerTier - 1] * (float)(Variables.UpgradePrices[Upgrades[1]] / 100f));
                                WeaponScript.ShootsPerSec += (WeaponScript.ShootsPerSec / 100) * UpgradePercent[1].ThirdUpgradePercent;
                            }
                            break;
                    }
                    Upgrades[1]++;
                }
                break;
            case 2:
                if (Upgrades[2] <= 2)
                {
                    
                    switch (Upgrades[2])
                    {
                        case 0:
                            if (SMaster.Money >= Variables.TowerPrices[WeaponScript.TowerTier - 1] * (float)(Variables.UpgradePrices[Upgrades[2]] / 100f))
                            {
                                SMaster.Money -= (int)(Variables.TowerPrices[WeaponScript.TowerTier - 1] * (float)(Variables.UpgradePrices[Upgrades[2]] / 100f));
                                WeaponScript.DamageMultiplier += (WeaponScript.DamageMultiplier / 100) * UpgradePercent[2].FirstUpgradePercent;
                            }
                            break;
                        case 1:
                            if (SMaster.Money >= Variables.TowerPrices[WeaponScript.TowerTier - 1] * (float)(Variables.UpgradePrices[Upgrades[1]] / 100f))
                            {
                                SMaster.Money -= (int)(Variables.TowerPrices[WeaponScript.TowerTier - 1] * (float)(Variables.UpgradePrices[Upgrades[1]] / 100f));
                                WeaponScript.DamageMultiplier += (WeaponScript.DamageMultiplier / 100) * UpgradePercent[2].SecondUpgradePercent;
                            }
                            break;
                        case 2:
                            if (SMaster.Money >= Variables.TowerPrices[WeaponScript.TowerTier - 1] * (float)(Variables.UpgradePrices[Upgrades[1]] / 100f))
                            {
                                SMaster.Money -= (int)(Variables.TowerPrices[WeaponScript.TowerTier - 1] * (float)(Variables.UpgradePrices[Upgrades[1]] / 100f));
                                WeaponScript.DamageMultiplier += (WeaponScript.DamageMultiplier / 100) * UpgradePercent[2].ThirdUpgradePercent;
                            }
                            break;
                    }
                    Upgrades[2]++;
                }
                break;
        }

    }

    public void OnMouseEnter()
    {
        ContinueCd = true;
    }

    public void OnMouseExit()
    {
        ContinueCd = false;
        Countdown = ToolTipDelay;
        if (TowerConfig == null&&RangeIndicator!= null)
        { 
        HideRange();
        }
    }

    public void Update()
    {
        if (Countdown <= 0)
        {
            ShowRange();
            ContinueCd = false;
            Countdown = ToolTipDelay;
        }
        else if (ContinueCd)
        {
            Countdown -= Time.deltaTime;
        }

    }
}
//public void Upgrade(int UpgradeKind)
//{
//    switch (UpgradeKind)
//    {
//        case 0:
//            if (Upgrades[0] <= 2)
//            {
//                switch (Upgrades[0])
//                {
//                    case 0:
//                        WeaponScript.range += (WeaponScript.range / 100f) * UpgradePercent[0].FirstUpgradePercent;
//                        break;
//                    case 1:
//                        this.transform.GetChild(0).GetComponent<Script_Weapon>().range += (this.transform.GetChild(0).GetComponent<Script_Weapon>().range / 100) * UpgradePercent[0].SecondUpgradePercent;
//                        break;
//                    case 2:
//                        this.transform.GetChild(0).GetComponent<Script_Weapon>().range += (this.transform.GetChild(0).GetComponent<Script_Weapon>().range / 100) * UpgradePercent[0].ThirdUpgradePercent;
//                        break;
//                }
//                Upgrades[0]++;
//            }
//            break;
//        case 1:
//            if (Upgrades[1] <= 2)
//            {

//                switch (Upgrades[1])
//                {
//                    case 0:
//                        this.transform.GetChild(0).GetComponent<Script_Weapon>().ShootsPerSec += (this.transform.GetChild(0).GetComponent<Script_Weapon>().ShootsPerSec / 100) * UpgradePercent[1].FirstUpgradePercent;
//                        break;
//                    case 1:
//                        this.transform.GetChild(0).GetComponent<Script_Weapon>().ShootsPerSec += (this.transform.GetChild(0).GetComponent<Script_Weapon>().ShootsPerSec / 100) * UpgradePercent[1].SecondUpgradePercent;
//                        break;
//                    case 2:
//                        this.transform.GetChild(0).GetComponent<Script_Weapon>().ShootsPerSec += (this.transform.GetChild(0).GetComponent<Script_Weapon>().ShootsPerSec / 100) * UpgradePercent[1].ThirdUpgradePercent;
//                        break;
//                }
//                Upgrades[1]++;
//            }
//            break;
//        case 2:
//            if (Upgrades[2] <= 2)
//            {

//                switch (Upgrades[2])
//                {
//                    case 0:
//                        this.transform.GetChild(0).GetComponent<Script_Weapon>().DamageMultiplier += (this.transform.GetChild(0).GetComponent<Script_Weapon>().DamageMultiplier / 100) * UpgradePercent[2].FirstUpgradePercent;
//                        break;
//                    case 1:
//                        this.transform.GetChild(0).GetComponent<Script_Weapon>().DamageMultiplier += (this.transform.GetChild(0).GetComponent<Script_Weapon>().DamageMultiplier / 100) * UpgradePercent[2].SecondUpgradePercent;
//                        break;
//                    case 2:
//                        this.transform.GetChild(0).GetComponent<Script_Weapon>().DamageMultiplier += (this.transform.GetChild(0).GetComponent<Script_Weapon>().DamageMultiplier / 100) * UpgradePercent[2].ThirdUpgradePercent;
//                        break;
//                }
//                Upgrades[2]++;
//            }
//            break;
//    }

//}