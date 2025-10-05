using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float damage;
    public ObjectPool<GameObject> gunBulletPool;

    public virtual void ReturnToPool(float time)
    {
        StartCoroutine(ReturnCoro(time));
    }
    IEnumerator ReturnCoro(float time)
    {
        yield return new WaitForSeconds(time);
        gunBulletPool.Release(this.gameObject);
        //Debug.Log(gameObject.name + " = "+ gunBulletPool.CountAll);
    }
}
