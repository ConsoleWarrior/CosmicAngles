using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoldDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isHolding = false;
    public Acceleration acceleration;

    // �����, ���������� ��� ������� �� ������
    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
    }

    // �����, ���������� ��� ���������� ������
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
        Debug.Log("������ ������������ - ��������� ��������!");
        // ����� ��������� ���� ������: ��������, �������� ���������, ����� ������ � �.�.
    }

    void StopAction()
    {
        Debug.Log("������ �������� - ������������� ��������.");
        // ������������� �������� ��� ��������� ������ ����������� ��������
    }
}