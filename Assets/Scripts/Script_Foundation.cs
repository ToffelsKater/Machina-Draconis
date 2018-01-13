using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Foundation : MonoBehaviour {

    public GameObject PanelPre, SMaster;
    public bool Taken;
    public Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
        Taken = false;
        SMaster = GameObject.Find("SceneMaster");
    }

    public void OnMouseDown()
    {
        //Debug.Log("try to build");
        if (Taken == false &&
            !SMaster.gameObject.GetComponent<Script_SceneMaster>().Pause&&
            SMaster.gameObject.GetComponent<Script_SceneMaster>().ActiveFoundation==null && 
            SMaster.gameObject.GetComponent<Script_SceneMaster>().FoundationPlaceable)
        {
            GameObject Panel = Instantiate(PanelPre, PanelPre.transform.position, PanelPre.transform.rotation);
            SMaster.GetComponent<Script_SceneMaster>().SetFoundation(this.gameObject);
            Panel.GetComponent<Script_Towerselect>().pos = this.transform.position;
            anim.SetBool("Pressed", true);
        }
    }

    public void SetTaken()
    {
        Taken = true;
        this.GetComponent<BoxCollider2D>().enabled = false;
        anim.SetBool("Pressed", false);
    }

    void Update() {


    }

}
