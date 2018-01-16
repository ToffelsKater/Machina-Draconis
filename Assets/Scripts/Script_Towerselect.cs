using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]

#pragma warning disable 0219
public class Script_Towerselect : MonoBehaviour {
    public static Script_Towerselect instance;

    public float cd;
    public Vector3 pos;
    public Transform[] Towers = new Transform[3];

    private bool StartClick;
    private GameObject RangeIndicator;

    private Script_Variables Variables;
    private Script_SceneMaster SMaster;

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
        SMaster = GameObject.Find("SceneMaster").GetComponent<Script_SceneMaster>();
        Variables = GameObject.Find("SceneMaster").GetComponent<Script_Variables>();
        cd = 0.01f;

        for (int i = 0; i <= 2; i++)
        {
            ButtonInteractable(i);
        }

    }

    private void ButtonInteractable ( int TowerTier)
    {
        if (SMaster.Money < Variables.TowerPrices[TowerTier])
        {
            this.gameObject.transform.GetChild(0).GetChild(TowerTier + 1).GetComponent<Button>().interactable = false;

        }
    }

    public void DestroyPanel()
    {
        SMaster.ActiveFoundation.GetComponent<Script_Foundation>().anim.SetBool("Pressed", false);
        SMaster.SetFoundation(null);
        HideRange();
        //Object.Destroy(gameObject, 0.3f);
        Destroy(this.gameObject);
    }

    public void BuildTower (int Tier)
    {
        if (SMaster.Money >= SMaster.TowerPrices[Tier-1])
        {
            Transform Tower = Instantiate(Towers[Tier-1], pos, new Quaternion(0, 0, 0, 0));
            
            SMaster.SetFoundationStatus();
            SMaster.Money -= SMaster.TowerPrices[Tier - 1];
            Tower.GetComponent<Script_Tower>().Foundation = SMaster.ActiveFoundation;
            //Object.Destroy(gameObject, 0.3f);
            DestroyPanel();

        }
    }

    public void ShowRange(int Tier)
    {
        float Range = Towers[Tier].transform.GetChild(0).GetComponent<Script_Weapon>().range;
        RangeIndicator = Instantiate(Resources.Load("RangeIndicator"), SMaster.ActiveFoundation.transform.position, SMaster.ActiveFoundation.transform.rotation) as GameObject;
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
