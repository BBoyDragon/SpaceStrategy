// Производные классы
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fabric : StandardBuilding
{
    public int timeToSpawn;
    public int currentTime;
    public GameObject bomber;

    // Конструктор по умолчанию
    public Fabric() : base()
    {
        timeToSpawn = 60;
        currentTime = 0;
        bomber = null;
    }

    // Параметризованный конструктор
    public Fabric(int initialShield, int maxShield, int regenRate, int buildingId, int owner,
                 int spawnTime, GameObject bomberPrefab)
        : base(initialShield, maxShield, regenRate, buildingId, owner)
    {
        timeToSpawn = spawnTime;
        currentTime = 0;
        bomber = bomberPrefab;
    }

    public void Spawn()
    {
        Instantiate(bomber);
    }

    public void Timer()
    {
        currentTime++;
        if (currentTime >= timeToSpawn)
        {
            currentTime = 0;
        }
    }
}

