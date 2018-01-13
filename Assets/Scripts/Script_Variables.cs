using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Variables : MonoBehaviour {

    [Header("Tower Prices")]
    public int[] TowerPrices = new int[3];
    [Space(20)]

    [Header("Upgrade Prices in Percent")]
    [Range(0,100)]
    public int[] UpgradePrices = new int[3];
    //[Space(20)]

    //[Header("Upgrade Settings")]
    //public UpgradeValues[] UpgradePercent = new UpgradeValues[3];

}
