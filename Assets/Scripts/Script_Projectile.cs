using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Projectile : MonoBehaviour
{
    public int TowerTier;
    public float speed;
    public GameObject target, SMaster, Weapon;

    public int[] SlowPercentages = new int[3];

    void Start()
    {
        SMaster = GameObject.Find("SceneMaster");
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (Vector3.Distance(target.transform.position, this.transform.position) <= 0.5)
            {
                target.gameObject.GetComponent<Script_enemie01>().health -= (target.gameObject.GetComponent<Script_enemie01>().Damages[TowerTier - 1] * Weapon.GetComponent<Script_Weapon>().DamageMultiplier);
                if (TowerTier == 2) {
                    target.gameObject.GetComponent<Script_enemie01>().Slow(SlowPercentages[target.gameObject.GetComponent<Script_enemie01>().EnemieTier-1]);
                }
                
                Destroy(this.gameObject);
            }
            Vector3 AimAt = target.transform.position;
            AimAt.x -= this.transform.position.x;
            AimAt.y -= this.transform.position.y;

            float angle = (Mathf.Atan2(AimAt.y, AimAt.x) * Mathf.Rad2Deg)+180;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            //transform.Translate( - speed*Time.fixedDeltaTime, 0, 0);
            transform.Translate( - speed*Time.deltaTime, 0, 0);
        }
    }
}
