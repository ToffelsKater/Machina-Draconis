using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirectionWay { left, right}

public class Waypoint : MonoBehaviour
{
    public DirectionWay Facing;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Facing == DirectionWay.left)
        {
            other.GetComponent<Script_enemie01>().TurnAntiClock();
        }
        else
        {
            other.GetComponent<Script_enemie01>().TurnClock();
        }


        if (other.GetComponent<Script_enemie01>().Facing == Script_enemie01.Direction.up  || other.GetComponent<Script_enemie01>().Facing == Script_enemie01.Direction.down)
        {
            other.transform.position = new Vector3(other.transform.position.x, Mathf.Round(other.transform.position.y*10f)/10f, 0f);
        }
        if (other.GetComponent<Script_enemie01>().Facing == Script_enemie01.Direction.right || other.GetComponent<Script_enemie01>().Facing == Script_enemie01.Direction.left)
        {
            other.transform.position = new Vector3(Mathf.Round(other.transform.position.x*10f)/10f,other.transform.position.y, 0f);
        }


    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
