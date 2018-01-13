using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_Dead_Enemie : MonoBehaviour {



    // Use this for initialization
    void Start () {
        StartCoroutine(ExistenceIsPain());
	}

    IEnumerator ExistenceIsPain()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(0, 1 * Time.deltaTime, 0);
	}
}
