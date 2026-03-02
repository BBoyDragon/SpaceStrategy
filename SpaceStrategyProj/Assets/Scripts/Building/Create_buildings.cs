using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Create_buildings : MonoBehaviour
{
    public Fabric_1 building;
    public Fabric_1 building1;
    public Healing building2;
    public ControllerController a;
    // Start is called before the first frame update
    void Start()
    {
        building.capturer = UnityEngine.Random.Range(1,2);
        building1.capturer = 2 - building.capturer + 1;
        a.buildings.Add(Instantiate(building, new Vector3(20, 50, 50), Quaternion.identity));
        a.buildings.Add(Instantiate(building1, new Vector3(80, 50, 50), Quaternion.identity));
        a.buildings.Add(Instantiate(building2, new Vector3(UnityEngine.Random.Range(0, 9)*10,UnityEngine.Random.Range(0, 9) * 10, 50), Quaternion.identity));  
    }
}
