using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public string[] audioName;
    public AudioClip[] audioClip;
    public bool clipFound;

    public void Play (string clipName)
    {
       for(int i=0; i < audioName.Length; i++)
        {
            if (clipName == audioName[i])
            {

                gameObject.GetComponent<AudioSource>().clip = audioClip[i];
                gameObject.GetComponent<AudioSource>().Play();
                clipFound = true;
                break;

            }
            else clipFound = false;
            {


            }
        }

       if(!clipFound)
        {
            Debug.Log("Audio Clip Not found!");
        }

    }
}
