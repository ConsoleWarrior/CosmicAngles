using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoldDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isHolding = false;
    public Acceleration acceleration;

    // Метод, вызываемый при нажатии на кнопку
    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
    }

    // Метод, вызываемый при отпускании кнопки
    public void OnPointerUp(PointerEventData eventData)
    {
        acceleration.PushUpAndroidButton();
        isHolding = false;
    }

    void Update()
    {
        if (isHolding)
        {
            acceleration.PushDownAndroidButton();
        }
    }

    void PerformAction()
    {
        Debug.Log("Кнопка удерживается - выполняем действие!");
        // Здесь поместите вашу логику: например, движение персонажа, заряд навыка и т.д.
    }

    void StopAction()
    {
        Debug.Log("Кнопка отпущена - останавливаем действие.");
        // Останавливаем движение или выполняем другие завершающие действия
    }
}