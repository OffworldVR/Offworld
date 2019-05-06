using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    public AudioSource[] sounds;
    public AudioSource[] music;

    void Start()
    {
        music[0].Play();
    }
    // Update is called once per frame
    void Update () {
		for(int i = 0; i < sounds.Length; i++)
        {
            sounds[i].volume = ((float)PlayerPrefs.GetInt("sfxVolume"))/100.0f;
        }
        for (int i = 0; i < music.Length; i++)
        {
            music[i].volume = ((float)PlayerPrefs.GetInt("musicVolume")) / 100.0f;
        }
    }
    public void PlayMusic(int i)
    {
        music[i].Play();
    }
    public void PlaySound(int i)
    {
        sounds[i].Play();
    }
}
