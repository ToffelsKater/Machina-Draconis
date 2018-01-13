using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_TestObject : MonoBehaviour {

    private Script_SceneMaster SMaster;

    public void OnMouseEnter()
    {
        SMaster.HasChanged = true; 
    }

    // Use this for initialization
    void Start () {
        SMaster = GameObject.Find("SceneMaster").GetComponent<Script_SceneMaster>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
