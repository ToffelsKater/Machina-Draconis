using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Despawner : MonoBehaviour {
    public GameObject SMaster;

	void Start () {
        SMaster = GameObject.Find("SceneMaster");
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        SMaster.GetComponent<Script_SceneMaster>().PlayerHealth -= 5;
        SMaster.GetComponent<Script_SceneMaster>().EnemyCount--;
    }

    void Update () {
		
	}
}
