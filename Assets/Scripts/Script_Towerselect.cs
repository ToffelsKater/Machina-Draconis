using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]

#pragma warning disable 0219
public class Script_Towerselect : MonoBehaviour {
    public static Script_Towerselect instance;

    public float cd;
    public GameObject SMaster;
    public Vector3 pos;
    public Transform[] Towers = new Transform[3];

    private bool StartClick;
    private GameObject RangeIndicator;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            //Object.Destroy(gameObject, 0.3f);
            //Destroy(gameObject);
            DestroyPanel();
        }
    }

	void Start () {
        StartClick = false;
        SMaster = GameObject.Find("SceneMaster");
        cd = 0.01f;

    }

    public void DestroyPanel()
    {
        SMaster.GetComponent<Script_SceneMaster>().ActiveFoundation.GetComponent<Script_Foundation>().anim.SetBool("Pressed", false);
        SMaster.GetComponent<Script_SceneMaster>().SetFoundation(null);
        HideRange();
        //Object.Destroy(gameObject, 0.3f);
        Destroy(this.gameObject);
    }

    public void BuildTower (int Tier)
    {
        if (SMaster.GetComponent<Script_SceneMaster>().Money >= SMaster.GetComponent<Script_SceneMaster>().TowerPrices[Tier-1])
        {
            Transform Tower = Instantiate(Towers[Tier-1], pos, new Quaternion(0, 0, 0, 0));
            
            SMaster.GetComponent<Script_SceneMaster>().SetFoundationStatus();
            SMaster.GetComponent<Script_SceneMaster>().Money -= SMaster.GetComponent<Script_SceneMaster>().TowerPrices[Tier - 1];
            Tower.GetComponent<Script_Tower>().Foundation = SMaster.GetComponent<Script_SceneMaster>().ActiveFoundation;
            //Object.Destroy(gameObject, 0.3f);
            //Debug.Log("buildtower");
            DestroyPanel();

        }
    }

    public void ShowRange(int Tier)
    {
        float Range = Towers[Tier].transform.GetChild(0).GetComponent<Script_Weapon>().range;
        RangeIndicator = Instantiate(Resources.Load("RangeIndicator"), SMaster.GetComponent<Script_SceneMaster>().ActiveFoundation.transform.position, SMaster.GetComponent<Script_SceneMaster>().ActiveFoundation.transform.rotation) as GameObject;
        RangeIndicator.transform.localScale = new Vector3(Range * 2, Range * 2, 0);
    }

    public void HideRange()
    {
        if (RangeIndicator != null)
        {
            Destroy(RangeIndicator.gameObject);
            RangeIndicator = null;
        }
    }

    void Update () {
        if (cd > 0)
        {
            cd -= 0.01f;
        }
        if (Input.GetMouseButtonDown(0) && (!(EventSystem.current.IsPointerOverGameObject())))
        {
            if (StartClick==true)
            //if (cd<=0)
            {
                //Object.Destroy(gameObject, 0.3f);
                DestroyPanel();
            }
            else
            {
                StartClick=true;
            }
        }
	}
}
