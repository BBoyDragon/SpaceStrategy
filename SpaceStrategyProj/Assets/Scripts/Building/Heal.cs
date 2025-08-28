using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Heal : StandardBuilding
{
    public int DistanceOfHealing;
    public int HpHeal;
    public int EnergyHeal;
    public int ShieldHealing;
    public void Healing()
    {
        // ��������� ���������� �� ���, ���, ���� ������� � ����� ��.
        // ������ ������ (20 ������)
        float searchRadius = 20f;

        // ������� ��� ������ (������� �������� �������)
        Vector3 searchPosition = transform.position;

        // ������ ��� �������� ��������� ��������
        List<GameObject> foundObjects = new List<GameObject>();

        // �������� ��� �������� ������� � �����
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        // �������� �� ���� ��������
        foreach (GameObject obj in allObjects)
        {
            // ���������� ���������� �������
            if (!obj.activeSelf) continue;

            // ���������� ��� ������ �������
            if (obj == gameObject) continue;

            // ��������� ���������� �� �������
            float distance = Vector3.Distance(searchPosition, obj.transform.position);

            // ���� ������ ��������� � ������� ������, ��������� ��� � ������
            if (distance <= searchRadius)
            {
                ShipModel shipModel = obj.GetComponent<ShipModel>();

                
                if (shipModel != null)
                {
                    
                    shipModel.shield += 10;

                    
                    shipModel.shield = Mathf.Min(shipModel.shield, shipModel.shieldmax);
                }
            }
        }

        
    }
}
