using UnityEngine;

public class TurelPlatform : MonoBehaviour
{

    void Update()
    {
        transform.Rotate(0, 0, -Time.deltaTime*50);
    }
}
