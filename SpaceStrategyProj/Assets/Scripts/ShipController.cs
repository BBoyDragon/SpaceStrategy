using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UI;
using System;

public class ShipController : MonoBehaviour
{
    public Vector3Int targetship;
    Vector3Int currentposint;
    public ShipModel model;

    private void Start()
    {
        currentposint = ToGameCoordinates(model.transform.position);
        targetship = ToGameCoordinates(model.transform.position);
    }

    private void Update()
    {
        model.transform.position = Vector3.MoveTowards(model.transform.position, ToWorldCoordinates(currentposint),5f*Time.deltaTime);
    }
    public void GetTargetLocation(Vector3Int inputik)
    {
        targetship = inputik;
    }

    public void GetStep()
    {
        if (currentposint != targetship)
        {
            Step();
            Debug.Log(currentposint + " " + targetship);
        }

    }

    public void Fire(ShipModel targetship)
    {

    }
    public void EndTurn()
    {

    }
    
    public void Step()
    {
        if (targetship == null || currentposint == targetship) 
        {
            return;
        }
        Vector3Int vector = GetStepTarget(currentposint, targetship);
        currentposint = currentposint + vector;
    }

    private Vector3Int GetStepTarget(Vector3Int current, Vector3Int target)
    {
        int xpos;
        int ypos;
        int zpos;
        xpos = Mathf.Clamp(target.x - current.x, -1, 1);
        ypos = Mathf.Clamp(target.y - current.y, -1, 1);
        zpos = Mathf.Clamp(target.z - current.z, -1, 1);
        return new Vector3Int(xpos, ypos, zpos);
    }

    private Vector3 ToWorldCoordinates(Vector3Int coordinates)
    {
        return new (coordinates.x *10, coordinates.y * 10, coordinates.z * 10);
    }

    private Vector3Int ToGameCoordinates(Vector3 coordinates)
    {
        return new(Convert.ToInt32(coordinates.x / 10), Convert.ToInt32(coordinates.y / 10), Convert.ToInt32(coordinates.z / 10));
    }

}
