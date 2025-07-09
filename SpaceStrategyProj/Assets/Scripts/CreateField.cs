using UnityEngine;
using System.Collections;

public class CreateField : MonoBehaviour
{
    public GameObject prefab;    // ������ ��� ����������
    public int gridSize = 150;    // ������ ����� (150x150x150)
    public float spacing = 10f;    // ���������� ����� ���������

    void Start()
    {
        // ���������� �����
        GenerateGrid();
    }

    void GenerateGrid()
    {
        // ��������� ������� �������
        if (prefab == null)
        {
            Debug.LogError("������ �� ��������!");
            return;
        }

        // ������� ������� � ���� ������
        for (int z = 0; z < gridSize; z++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    // ������� ��������� �������
                    GameObject instance = Instantiate(prefab) as GameObject;

                    // ������������ �������
                    Vector3 position = new Vector3(
                        x * spacing,    // �� ��� X
                        y * spacing,    // �� ��� Y
                        z * spacing     // �� ��� Z
                    );

                    // ������������� �������
                    instance.transform.position = position;

                    // �����������: ����� ���������� ������������ ������
                    instance.transform.SetParent(transform);
                }
            }
        }
    }
}