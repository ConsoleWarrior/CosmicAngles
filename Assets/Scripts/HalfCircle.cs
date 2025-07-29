using UnityEngine;

public class HalfCircle : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] float thickness;
    [SerializeField] int segments; // Количество сегментов для отрисовки. Больше - плавнее, но ресурсозатратнее.
    [SerializeField] Color color;
    [SerializeField] LineRenderer lineRenderer;


    void Start()
    {
        //lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.startWidth = thickness;
        lineRenderer.endWidth = thickness;
        lineRenderer.positionCount = segments + 1; // Добавляем одну точку для замыкания круга.
        UpdateCircle();
    }

    void UpdateCircle()
    {
        float angleStep = 180f / segments;
        for (int i = 0; i <= segments; i++)
        {
            float angle = i * angleStep;
            float x = radius * Mathf.Cos(Mathf.Deg2Rad * angle);
            float y = radius * Mathf.Sin(Mathf.Deg2Rad * angle);
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }

    // Метод для изменения толщины
    public void SetThickness(float newThickness)
    {
        thickness = newThickness;
        if (lineRenderer == null) Debug.Log("lineRenderer null");
        lineRenderer.startWidth = thickness;
        lineRenderer.endWidth = thickness;
    }

    // Пример изменения толщины через инспектор.
    //void OnValidate()
    //{
    //    if (lineRenderer != null)
    //    {
    //        UpdateCircle();
    //        lineRenderer.startWidth = thickness;
    //        lineRenderer.endWidth = thickness;
    //    }
    //}
}