using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Script_Spawner : MonoBehaviour {


    public float Factor;
    public Text Wave_Display;
    public Wave[] Waves = new Wave[10];
    public GameObject SMaster;


    private int index,WaveNum;
    private bool SpawningWave;
    private float countdown;
    private GameObject[] Enemies = new GameObject[1];

    [System.Serializable]
    public struct Wave
    {
        public GameObject EnemieType;
        public float Pause;
        public int Amount;
    }


    void Start () {
        index = 0;
        SMaster = GameObject.Find("SceneMaster");
        SpawningWave = false;
        countdown = Waves[0].Pause;
        for (int i=0; i < Waves.Length; i++)
        {
            SMaster.gameObject.GetComponent<Script_SceneMaster>().EnemyCount += Waves[i].Amount;
        }

    }

    void NextWave()
    {
        if (WaveNum < Waves.Length - 1)
        {
            WaveNum++;
            countdown = Waves[WaveNum].Pause;
            SpawningWave = false;
        }
    }

    IEnumerator SpawnWave(int WaveNum)
    {
        for (int i = 0; i < Waves[WaveNum].Amount; i++)
        {
            GameObject Enemie = Instantiate(Waves[WaveNum].EnemieType, this.transform.position, this.transform.rotation);
            Enemie.name = "ThisEnemie " + (index + 1);
            Enemies[0] = Enemie;
            Enemies[0].gameObject.GetComponent<Script_enemie01>().Index = index;
            Enemies[0].gameObject.GetComponent<Script_enemie01>().health = (int)(Enemies[0].gameObject.GetComponent<Script_enemie01>().health + Enemies[0].gameObject.GetComponent<Script_enemie01>().health*Factor * WaveNum);
            Enemies[0].gameObject.GetComponent<Script_enemie01>().reward *= (1f + Factor * WaveNum);
            //Enemies[0].gameObject.GetComponent<Script_enemie01>().speed = Waves[WaveNum].Speed;
            index++;
            yield return new WaitForSeconds(1.5f);
        }
        NextWave();
    }

    void Update()
    {
        Wave_Display.text = "Wave " + WaveNum;
        if (WaveNum < Waves.Length)
        {
            if (!SpawningWave)
            {
                if (countdown <= 0)
                {
                    SpawningWave = true;
                    StartCoroutine(SpawnWave(WaveNum));
                }
                else
                {
                    countdown -= Time.deltaTime*SMaster.gameObject.GetComponent<Script_SceneMaster>().GameSpeed;
                }
            }
        }

    }
}
