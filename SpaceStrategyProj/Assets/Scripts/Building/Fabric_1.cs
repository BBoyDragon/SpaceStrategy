using UnityEngine;

public class Fabric_1 : Fabric
{
    // Дополнительные поля, если нужны
    private float spawnInterval;

    // Конструктор по умолчанию
    public Fabric_1() : base()
    {
        spawnInterval = 5.0f;
    }

    // Параметризованный конструктор
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