using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip default_bgm, shoot_sfx, monster_sfx, button_sfx;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Awake()
    {
        default_bgm = Resources.Load<AudioClip>("BGM_Default");
        monster_sfx = Resources.Load<AudioClip>("Monster");
        shoot_sfx = Resources.Load<AudioClip>("Shoot");
        button_sfx = Resources.Load<AudioClip>("Button");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "BGM_Default":
                audioSrc.PlayOneShot(default_bgm);
                break;
            case "Monster":
                audioSrc.PlayOneShot(monster_sfx);
                break;
            case "Shoot":
                audioSrc.PlayOneShot(shoot_sfx);
                break;
            case "Button":
                audioSrc.PlayOneShot(button_sfx);
                break;
        }
    }
}
