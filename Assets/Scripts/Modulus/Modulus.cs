using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Modulus : MonoBehaviour
{
    public virtual string GetCharacter()
    {
        return "0";
    }
}
public class Guns : Modulus
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float reload;
    [SerializeField] protected float damage;
    protected Transform target;
    protected List<Transform> targets = new();
    [SerializeField] protected AudioManager audioManager;
    protected bool flag = false;
    [SerializeField] protected Transform sprite;
    protected ObjectPool<GameObject> gunBulletPool;

    void Start()
    {
        audioManager.a.volume = 0.15f;
        gunBulletPool = GameObject.Find("PoolManager").GetComponent<PoolManager>().gunBulletPool;
    }
    public override string GetCharacter()
    {
        return "damage= " + damage + "\nbulletSpeed= " + bulletSpeed + "\nreload= " + reload;
    }
}

public class LazerGuns : Modulus
{
    [SerializeField] protected float reload;
    [SerializeField] protected float damage;
    protected Transform target;
    protected List<Transform> targets = new();
    [SerializeField] protected AudioManager audioManager;
    protected bool flag = false;
    [SerializeField] protected Transform sprite;


    void Start()
    {
        audioManager.a.volume = 0.15f;
    }
    public override string GetCharacter()
    {
        return "damage= " + damage + "\nreload= " + reload;
    }
}
