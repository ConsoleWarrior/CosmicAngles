using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public LinkedList<AudioClip> tracks = new LinkedList<AudioClip>();
    public AudioClip[] sound;
    [SerializeField] AudioSource a;
    bool button;


    void Start()
    {
        foreach (AudioClip clip in sound)
        {
            tracks.AddLast(clip);
        }
    }
    public void OnClickButtonMusic()
    {
        if (!button)
        {
            button = true;
            StartPlay();
        }
        else
        {
            StopPlay();
            button = false;
        }
    }
    void StartPlay()
    {
        StartCoroutine(Playing());
    }
    void StopPlay()
    {
        StopAllCoroutines();
        a.Stop();
    }
    IEnumerator Playing()
    {
        while (true)
        {
            Do();
            yield return new WaitForSeconds(5);
        }
    }
    void Do()
    {
        if (!a.isPlaying)
        {
            NextPlay();
        }
    }
    void NextPlay()
    {
        //var firstNode = tracks.First.Value;
        tracks.AddLast(tracks.First.Value);
        tracks.RemoveFirst();
        SoundPlay0();
    }
    void SoundPlay0()
    {
        a.clip = tracks.First.Value;
        a.PlayOneShot(a.clip);
        //a.Play();
    }


}
