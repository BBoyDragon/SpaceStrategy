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
    int hod = 1;

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

        if (hod == timeToSpawn)
        {
            if (capturer == 1)
            {
                var a = (Instantiate(bomber, transform.position + new Vector3(13, 13, 13), Quaternion.identity));
                controller.player1shipControllers.Add(a);
                controller.ShipControllers.Add(a);
                controller.shipControllers.Add(a);
                a.currentposint = a.ToGameCoordinates(a.model.transform.position);
                a.targetship = a.ToGameCoordinates(a.model.transform.position);
                a.model.StartCoroutine(a.model.Fly());

            }
            if (capturer == 2)
            {
                var a = (Instantiate(bomber, transform.position + new Vector3(13, 13, 13), Quaternion.identity));
                controller.player2shipControllers.Add(a);
                controller.ShipControllers.Add(a);
                controller.shipControllers.Add(a);
                a.currentposint = a.ToGameCoordinates(a.model.transform.position);
                a.targetship = a.ToGameCoordinates(a.model.transform.position);
                a.model.StartCoroutine(a.model.Fly());
            }
            hod = 1;
        }
        else
        {
            hod += 1;
        }

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

