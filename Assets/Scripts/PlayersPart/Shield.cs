using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] float shieldCapacity;
    [SerializeField] float shieldCurrentVolume;
    [SerializeField] float shieldSpeedRecovery;
    [SerializeField] float shieldVolumeRecovery;
    [SerializeField] HalfCircle lineRenderer;
    [SerializeField] PolygonCollider2D shieldCollider;
    [SerializeField] AudioManager audioManager;

    void Start()
    {
        StartCoroutine(ShieldRecovery());
        audioManager.a.volume = 0.4f;
    }
    void Update()
    {
    }

    public void TakeDamage(float damage)
    {
        shieldCurrentVolume -= damage;
        if (shieldCurrentVolume <= 0)
        {
            shieldCurrentVolume = 0;
            shieldCollider.enabled = false;
        }
        lineRenderer.SetThickness(shieldCurrentVolume / shieldCapacity);
        var a = Random.Range(0, 3);
        switch (a)
        {
            case 0: audioManager.SoundPlay0();break;
            case 1: audioManager.SoundPlay1();break;
            case 2: audioManager.SoundPlay2();break;
        }
        
    }
    IEnumerator ShieldRecovery()
    {
        while (true)
        {
            shieldCollider.enabled = true;
            shieldCurrentVolume += shieldVolumeRecovery;
            if (shieldCurrentVolume > shieldCapacity)
            {
                shieldCurrentVolume = shieldCapacity;
            }
            lineRenderer.SetThickness(shieldCurrentVolume / shieldCapacity);
            yield return new WaitForSeconds(shieldSpeedRecovery);
        }
    }
}
