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
    public AudioClip spawn_sound;
    public AudioSource source;

    private void Start()
    {
        source.clip = spawn_sound;
    }

    // Конструктор по умолчанию
    public Fabric() : base()
    {
        timeToSpawn = 1;
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
        Debug.Log("Сработоло");
        if (Hod == timeToSpawn)
        {
            Debug.Log("Сработоло");
            if (capturer == 1)
            {
                Debug.Log("blue");
                var a = (Instantiate(bomber, transform.position + new Vector3(13, 13, 13), Quaternion.identity));
                controller.player1shipControllers.Add(a);
                controller.ShipControllers.Add(a);
                controller.shipControllers.Add(a);
                a.currentposint = a.ToGameCoordinates(a.model.transform.position);
                a.targetship = a.ToGameCoordinates(a.model.transform.position);
                source.Play();
            }
            if (capturer == 2)
            {
                Debug.Log("red");
                var a = (Instantiate(bomber, transform.position + new Vector3(13, 13, 13), Quaternion.identity));
                controller.player2shipControllers.Add(a);
                controller.ShipControllers.Add(a);
                controller.shipControllers.Add(a);
                a.currentposint = a.ToGameCoordinates(a.model.transform.position);
                a.targetship = a.ToGameCoordinates(a.model.transform.position);
                source.Play();
            }

        }
        

    }

    
}

