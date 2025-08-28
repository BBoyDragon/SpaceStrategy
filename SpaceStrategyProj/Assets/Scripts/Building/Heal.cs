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
        // Проверяет захваченна ли она, кем, ищет корабли и хилит их.
        // Радиус поиска (20 единиц)
        float searchRadius = 20f;

        // Позиция для поиска (позиция текущего объекта)
        Vector3 searchPosition = transform.position;

        // Список для хранения найденных объектов
        List<GameObject> foundObjects = new List<GameObject>();

        // Получаем все активные объекты в сцене
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        // Проходим по всем объектам
        foreach (GameObject obj in allObjects)
        {
            // Пропускаем неактивные объекты
            if (!obj.activeSelf) continue;

            // Пропускаем сам объект лечения
            if (obj == gameObject) continue;

            // Вычисляем расстояние до объекта
            float distance = Vector3.Distance(searchPosition, obj.transform.position);

            // Если объект находится в радиусе поиска, добавляем его в список
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
