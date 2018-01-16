using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Weapon : MonoBehaviour {

    public int TowerTier, Preference, Price;
    public float range, ShootsPerSec, DamageMultiplier;
    public Animator AniWeapon;
    public GameObject ProjectilePre,SMaster,target;

    [Range(0,1)]
    public float RangeSearchReduction;

    private float countdown, angle, lookangle;
    private List<GameObject> Projectiles = new List<GameObject>();

	void Start ()
    {
        if (TowerTier == 2)
        {
            Preference = 4;
        }
        else
        {
            Preference = 0;
        }
        target = null;
        SMaster = GameObject.Find("SceneMaster");
        countdown = 0;
        lookangle = 0;
        DamageMultiplier = 1f;
    }

    public void Shoot()
    {
        GameObject Projectile = Instantiate(ProjectilePre, new Vector3( this.transform.position.x,this.transform.position.y+0.15f,this.transform.position.z), this.transform.rotation);
        Projectiles.Add(Projectile);
        Projectiles[0].gameObject.GetComponent<Script_Projectile>().target = target;
        Projectiles[0].gameObject.GetComponent<Script_Projectile>().TowerTier = TowerTier;
        Projectiles[0].gameObject.GetComponent<Script_Projectile>().Weapon = this.gameObject;
        Projectiles.Clear();
        countdown = 1/ShootsPerSec;
    }

    private bool Checkrot(float thisrot, float targetrot,float range)
    {
        thisrot += 360;
        targetrot += 360;
        if ((thisrot <= (targetrot +range)) && (thisrot >= (targetrot - range)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetTarget()
    {
        float HealthOld = 0, HealthNew;
        int IndexOld = 0, IndexNew;
        GameObject tar = null;
        Collider2D[] InRange = Physics2D.OverlapCircleAll(this.transform.position, range*RangeSearchReduction);
        List<GameObject> Enemies = new List<GameObject>();
        foreach (Collider2D other in InRange)
        {
            if (other.gameObject.tag == "Enemie")
            {
                Enemies.Add(other.gameObject);
            }
        }

        switch (Preference)
        {
            case 0:
                foreach (GameObject Enemie in Enemies)
                {
                    IndexNew = Enemie.GetComponent<Script_enemie01>().Index;
                    if (IndexOld == 0 || IndexOld > IndexNew)
                    {
                        IndexOld = IndexNew;
                        tar = Enemie;
                    }

                }
                break;
            case 1:
                if (Enemies.Count != 0)
                {
                    System.Random rnd = new System.Random();
                    int selected = rnd.Next(0, Enemies.Count);
                    tar = Enemies[selected];
                }
                break;
            case 2:
                
                foreach(GameObject Enemie in Enemies)
                {
                    HealthNew = Enemie.GetComponent<Script_enemie01>().health;
                    if (HealthNew > HealthOld)
                    {
                        HealthOld = HealthNew;
                        tar = Enemie;
                    }
                }
                break;
            case 3:
                if(Enemies.Count != 0)
                {
                    HealthOld = Enemies[0].GetComponent<Script_enemie01>().health;
                }
                foreach (GameObject Enemie in Enemies)
                {
                    HealthNew = Enemie.GetComponent<Script_enemie01>().health;
                    if (HealthNew < HealthOld)
                    {
                        HealthOld = HealthNew;
                        tar = Enemie;
                    }
                }
                break;
            case 4:
                GameObject AlternativeTar=null;
                foreach (GameObject Enemie in Enemies)
                {
                    IndexNew = Enemie.GetComponent<Script_enemie01>().Index;
                    if (IndexOld == 0 || IndexOld > IndexNew)
                    {
                        if (Enemie.GetComponent<Script_enemie01>().SlowTimer <= 0)
                        {
                            tar = Enemie;
                        }
                        IndexOld = IndexNew;
                        AlternativeTar = Enemie;
                    }

                }
                if(tar== null)
                {
                    tar = AlternativeTar;
                }
                break;

        }


        target = tar;
    }

    void Update ()
    {
        if (countdown > 0)
        {
            countdown -= 1 * Time.deltaTime * SMaster.GetComponent<Script_SceneMaster>().GameSpeed;
        }
        if (countdown < 0)
        {
            countdown = 0;
        }

        if ((target == null))
        {
            
            SetTarget();
        }
        else if (Preference==4 && target.gameObject.GetComponent<Script_enemie01>().SlowTimer > 0)
        {
            
            SetTarget();
        }
        else
        {
            if (Vector3.Distance(target.transform.position, this.transform.parent.transform.position) <= range)
            {
                Vector3 AimAt = target.transform.position;
                AimAt.x -= this.transform.position.x;
                AimAt.y -= this.transform.position.y;

                angle = (Mathf.Atan2(AimAt.x, AimAt.y) * Mathf.Rad2Deg);
                if (angle < 0)
                {
                    angle += 360;
                }


                if (Checkrot(lookangle, angle, 5f))
                {
                    if (countdown <= 0)
                    {
                        Shoot();
                    }
                }
                else
                {
                    float diff = angle - lookangle;
                    if (diff < 0f)
                    {
                        diff += 360f;
                    }
                    if (diff >= 180f)
                    {
                        lookangle -= 5;
                        if (lookangle <= -1f)
                        {
                            lookangle = 359f;
                        }
                    }
                    else
                    {
                        lookangle += 5;
                        if (lookangle >= 360f)
                        {
                            lookangle = 0f;
                        }
                    }
                }
            }
            else
            {
                target = null;
            }


        }


        for (int i = 0; i <= 7; i++)
        {
            
            if (Checkrot(lookangle, ((float)i * 45), 22.5f))
            {
                AniWeapon.SetInteger("Facing", i);
            }
        }
    }
}
