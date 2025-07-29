using System.Collections;
using UnityEngine;

public class FoodRespawn : MonoBehaviour
{
    private float time = 0.05f;

    void Start()
    {
        StartCoroutine("RespawnCoro");
    }

    void Update()
    {
    }

    public void Respawn()
    {
        GameObject c = (GameObject)Instantiate(Resources.Load("Scrap", typeof(GameObject)), new Vector2(Random.Range(-100, 100), Random.Range(-100, 100)), Quaternion.identity);
        c.transform.SetParent(transform, true);
        GameObject b = (GameObject)Instantiate(Resources.Load("Scrap", typeof(GameObject)), new Vector2(Random.Range(-100, 100), Random.Range(-200, -100)), Quaternion.identity);
        c.transform.SetParent(transform, true);
    }

    IEnumerator RespawnCoro()
    {
        while (true)
        {
            Respawn();
            if (transform.childCount > 6000 && time != 0.3f)
            {
                time = 0.3f;
            }
            if (transform.childCount < 6000 && transform.childCount > 3000 && time != 0.15f)
            {
                time = 0.15f;
            }
            else if (transform.childCount <= 3000 && time != 0.05f)
            {
                time = 0.05f;
            }
            yield return new WaitForSeconds(time);
        }
    }
}
