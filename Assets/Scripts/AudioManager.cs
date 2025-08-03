using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] sound;
    public AudioSource a;

    public void SoundPlay0()
    {
        a.clip = sound[0];
        a.PlayOneShot(a.clip);
        //a.Play();
    }
    public void SoundPlay1()
    {
        a.clip = sound[1];
        //a.Play();
        a.PlayOneShot(a.clip);
    }
    public void SoundPlay2()
    {
        a.clip = sound[2];
        a.PlayOneShot(a.clip);
        //a.Play();
    }
    public void SoundPlay3()
    {
        a.clip = sound[3];
        a.Play();
    }
    public void Stop()
    {
        a.Stop();
    }
}