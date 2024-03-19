using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource music;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        music = GetComponent<AudioSource>();
    }
    public void PlayMusic()
    {
        if (music.isPlaying) return;
        music.Play();
    }
    public void StopMusic()
    {
        music.Stop();
    }
}
