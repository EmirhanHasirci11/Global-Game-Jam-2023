using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
//my code is betiful
public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    public Options o;
    public Pause p;
    public float musicmultiplier;

    private void Awake()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source = gameObject.AddComponent<AudioSource>();
            sounds[i].source.volume = sounds[i].volume;
            sounds[i].source.pitch = sounds[i].pitch;
            sounds[i].source.loop = sounds[i].loop;
            if (sounds[i].isMusic)
            {
                Debug.Log("imepe");
                sounds[i].source.clip = sounds[i].clip[0];
                sounds[i].source.Play();
            }
        }
    }

    public void PlaySound(string soundname)
    {
        Sound s = null;
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == soundname)
            {
                s = sounds[i];
            }
        }
        if (s != null)
        {
            s.source.clip = s.clip[Random.Range(0, s.clip.Length)];
            s.source.Play();
        }
        else
        {
            Debug.LogWarning(soundname + " Sound Name Not Valid");
        }
    }

    public void SetVolume()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].isMusic)
            {
                if (p != null)
                {
                    sounds[i].source.volume = sounds[i].volume * JsonUtility.FromJson<OptionsSave>(o.JSON).music * p.musicmultiplier;
                }
                else
                {
                    sounds[i].source.volume = sounds[i].volume * JsonUtility.FromJson<OptionsSave>(o.JSON).music;
                }
            }
            else
            {
                sounds[i].source.volume = sounds[i].volume * JsonUtility.FromJson<OptionsSave>(o.JSON).volume;
            }

        }
    }

}
