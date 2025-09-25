using UnityEngine;
using UnityEngine.Pool;

public class AnimDestroy : MonoBehaviour
{
    public ObjectPool<GameObject> animPool;
    [SerializeField] AudioManager audioManager;

    void OnEnable()
    {
        audioManager.SoundPlay0();
        Invoke("ReturnToPool", 0.5f);
    }

    void ReturnToPool()
    {
        animPool.Release(this.gameObject);
    }
}
