// Производные классы
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fabric : StandardBuilding
{

    public int timeToSpawn;
    public int currentTime;
    public ShipController bomber;
    public ControllerController controller; 

    // Конструктор по умолчанию
    public Fabric() : base()
    {
        timeToSpawn = 60;
        currentTime = 0;
        bomber = null;
    }

    // Параметризованный конструктор
    public Fabric(int initialShield, int maxShield, int regenRate, int buildingId, int owner,
                 int spawnTime, ShipController bomberPrefab)
        : base(initialShield, maxShield, regenRate, buildingId, owner)
    {
        timeToSpawn = spawnTime;
        currentTime = 0;
        bomber = bomberPrefab;
    }

   

    public void Spawn()
    {


        controller.shipControllers.Add(Instantiate(bomber, transform.position+ new Vector3(13, 13, 13), Quaternion.identity));
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

