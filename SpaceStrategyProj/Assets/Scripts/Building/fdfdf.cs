using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class HealTrigger : Heal, IPointerClickHandler
{
    // ����� ���� ��� ������
    public LayerMask clickableLayer;

    // ���� ������ �������
    private bool isSelected;

    private void Start()
    {
        // ������������� ������ �� ���������, ���� �� �����
        if (DistanceOfHealing == 0)
            DistanceOfHealing = 20;
    }

    // �����, ���������� ��� ����� �� ������
    public void OnPointerClick(PointerEventData eventData)
    {
        // ���������, ��� �� ���� ����������� ����� ������� ����
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // ���������� �������
            Healing();

            // ���������� �������� �����
            Debug.Log("������� ������������!");
        }
    }

    // ��� ������ � ������� ����� �������� ���������
    private void OnEnable()
    {
        // ������������� �� ������� ������
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    // ������������ ������� ��� ����������� ����������� �������
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DistanceOfHealing);
    }
}