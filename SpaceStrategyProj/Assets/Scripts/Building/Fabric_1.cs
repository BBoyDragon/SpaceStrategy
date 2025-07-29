using UnityEngine;

public class Fabric_1 : Fabric
{
    // �������������� ����, ���� �����
    private float spawnInterval;

    // ����������� �� ���������
    public Fabric_1() : base()
    {
        spawnInterval = 5.0f;
    }

    // ����������������� �����������
    public Fabric_1(int initialShield, int maxShield, int regenRate, int buildingId, int owner,
                   int spawnTime, GameObject bomberPrefab, float interval)
        : base(initialShield, maxShield, regenRate, buildingId, owner, spawnTime, bomberPrefab)
    {
        spawnInterval = interval;
    }

     void Start()
    {        
      Spawn();
    }
}