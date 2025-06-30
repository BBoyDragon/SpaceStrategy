using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Routs;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    void Start()
    {
        BasicRout rout = new Nebula("Nebula", 300);
        BasicRout rout2 = new AsteroidField("Nebula", 300);
        
        
        SpaceShip ship = new Scout("Scout");
        SpaceShip ship2 = new CargoShip("Scout");
        rout.CalculateTime(ship);
        rout.CalculateTime(ship2);
        
        rout2.CalculateTime(ship);
        rout2.CalculateTime(ship2);
    }


    void Update()
    {
        
    }
}
