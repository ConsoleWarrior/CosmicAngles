using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float damage;
    public ObjectPool<GameObject> gunBulletPool;
    public bool inFly = false;

    public void ReturnToPool(float time)
    {
        StartCoroutine(ReturnCoro(time));
        //Invoke("Return", time);
    }
    void Return()
    {
        if (inFly) gunBulletPool.Release(this.gameObject);
        Debug.Log(gunBulletPool.CountAll);
    }
    IEnumerator ReturnCoro(float time)
    {
        yield return new WaitForSeconds(time);
        gunBulletPool.Release(this.gameObject);
        Debug.Log(gunBulletPool + " = "+ gunBulletPool.CountAll);
    }
}
