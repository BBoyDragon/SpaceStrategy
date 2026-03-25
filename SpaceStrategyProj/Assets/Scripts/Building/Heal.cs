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
    public GameObject lol;

    
    public AudioClip heal_sound;
    public AudioSource source;

    private void Start()
    {
        source.clip = heal_sound;
    }

    public void Healing()
    {
        // ��������� ���������� �� ���, ���, ���� ������� � ����� ��.
        // ������ ������ (20 ������)
        float searchRadius = 20f;
        Debug.Log("���������");

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


            // ��������� ���������� �� �������
            float distance = Vector3.Distance(searchPosition, obj.transform.position);

            // ���� ������ ��������� � ������� ������, ��������� ��� � ������
            if (distance <= searchRadius)
            {
                ShipModel shipModel = obj.GetComponent<ShipModel>();

                
                if (shipModel != null && shipModel.capturer == capturer)
                {
                    
                    shipModel.shield += 10;
                    source.Play();
                    StartCoroutine(wenom(obj));

                    
                    shipModel.shield = Mathf.Min(shipModel.shield, shipModel.shieldmax);
                }
            }
        }
        

}
    IEnumerator wenom(GameObject target) {
        GameObject a = Instantiate(lol, target.transform);
        yield return new WaitForSeconds(1);
        Destroy(a);
    }
}
