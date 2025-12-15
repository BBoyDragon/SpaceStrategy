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
    // Start is called before the first frame update
    void Start()
    {
        building.capturer = UnityEngine.Random.Range(1,2);
        building1.capturer = 2 - building.capturer + 1;
        Instantiate(building, new Vector3(20, 50, 50), Quaternion.identity);
        Instantiate(building1, new Vector3(80, 50, 50), Quaternion.identity);
        Instantiate(building2, new Vector3(UnityEngine.Random.Range(0, 9)*10,UnityEngine.Random.Range(0, 9) * 10, 50), Quaternion.identity);  
    }
}
