using UnityEngine;
using System.Collections;

public class CreateField : MonoBehaviour
{
    public GameObject prefab;    // Префаб для размещения
    public int gridSize = 150;    // Размер сетки (150x150x150)
    public float spacing = 10f;    // Расстояние между объектами

    void Start()
    {
        // Генерируем сетку
        GenerateGrid();
    }

    void GenerateGrid()
    {
        // Проверяем наличие префаба
        if (prefab == null)
        {
            Debug.LogError("Префаб не назначен!");
            return;
        }

        // Создаем объекты в трех циклах
        for (int z = 0; z < gridSize; z++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    // Создаем экземпляр префаба
                    GameObject instance = Instantiate(prefab) as GameObject;

                    // Рассчитываем позицию
                    Vector3 position = new Vector3(
                        x * spacing,    // По оси X
                        y * spacing,    // По оси Y
                        z * spacing     // По оси Z
                    );

                    // Устанавливаем позицию
                    instance.transform.position = position;

                    // Опционально: можно установить родительский объект
                    instance.transform.SetParent(transform);
                }
            }
        }
    }
}