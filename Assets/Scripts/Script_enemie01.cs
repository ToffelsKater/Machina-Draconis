using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#pragma warning disable 0219
public class Script_enemie01 : MonoBehaviour
{

    public Direction Facing;
    public enum Direction { up, right, down, left };

    public int Index,EnemieTier;
    public bool Slowed;
    public float speed,health,reward,SlowTimer;

    public int[] Damages = new int[3];
    public Animator AniMouse;
    public GameObject SMaster;

    private float MaxHealth,Wait,ActualSpeed;
    private GameObject Healthbar;

    void Start()
    {
        Slowed = false;
        Facing = Direction.right;
        AniMouse.SetInteger("Facing", 1);
        SMaster = GameObject.Find("SceneMaster");

        Healthbar = this.gameObject.transform.GetChild(0).gameObject;
        Healthbar.transform.localScale = new Vector3(3f, 2f, 0);
        MaxHealth = health;
        ActualSpeed = speed;
    }

    public void SetIndex(int index)
    {
        Index = index;
    }

    //public IEnumerator Slow(int SlowPercentage)
    //{

    //    if (!Slowed)
    //    {
    //        Slowed = true;
    //        Wait = 2f;
    //        ActualSpeed= speed*(float)((100f - SlowPercentage) / 100f);
    //        yield return new WaitForSeconds(Wait);
    //        Slowed = false;
    //        ActualSpeed = speed;
    //    }
    //    else
    //    {

    //        Wait = 2f;
    //        yield break;
    //    }
    //}

    public void Slow(int SlowPercentage)
    {
        SlowTimer = 2f;
        ActualSpeed = speed * (float)((100f - SlowPercentage) / 100f);
    }

    public void TurnClock()
    {
        if (Facing == Direction.left)
        {
            Facing = Direction.up;
        }
        else
        {
            Facing++;
        }
    }
    public void TurnAntiClock()
    {
        if (Facing == Direction.up)
        {
            Facing = Direction.left;
        }
        else
        {
            Facing--;
        }
    }

    void Update()
    {
        if (SlowTimer > 0)
        {
            SlowTimer -= Time.deltaTime;
        }
        else
        {
            ActualSpeed = speed;
        }
        //for (int i = 0; i < SMaster.GetComponent<Script_SceneMaster>().GameSpeed; i++)
        {
            //GameSpeed = SMaster.gameObject.GetComponent<Script_SceneMaster>().GameSpeed;
            Healthbar.transform.localScale = new Vector3(3f * health / MaxHealth, 2f, 0);
            if (health <= 0)
            {
                string deadName = "Dead_Enemie0" + EnemieTier;
                SMaster.GetComponent<Script_SceneMaster>().Money += (int)reward;
                SMaster.GetComponent<Script_SceneMaster>().EnemyCount--;
                GameObject Spirit = Instantiate(Resources.Load(deadName), this.transform.position, new Quaternion()) as GameObject;
                Destroy(this.gameObject);
            }
            else
            {
                float movement = ActualSpeed * Time.deltaTime;
                switch (Facing)
                {
                    case Direction.up: transform.Translate(0, movement, 0); AniMouse.SetInteger("Facing", 0); break;
                    case Direction.right: transform.Translate( movement, 0, 0); AniMouse.SetInteger("Facing", 1); break;
                    case Direction.down: transform.Translate(0, -movement, 0); AniMouse.SetInteger("Facing", 2); break;
                    case Direction.left: transform.Translate(-movement, 0, 0); AniMouse.SetInteger("Facing", 3); break;
                }
            }
        }
    }
}
