using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using TMPro;
using static UnityEditor.Progress;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    public ShipModel ship = new ShipModel(1,1,1,1,1,1,1,1,1,1,"a");

    Vector3Int targetship;
    Vector3Int currentposint;
    public ShipModel model;
    public ShipModel modelp1;
    public ShipModel modelp2;
    public InputFieldReader input;
    public bool IsShip1Active;
    public Button switchButton;

    private void Start()
    {
        model = modelp1;
        IsShip1Active = true;
        switchButton.onClick.AddListener(SwitchShips);
    }

    private void Update()
    {
        GetTargetLocation();
        if (currentposint != targetship)
        {
            Step();
            Debug.Log(currentposint + " " + targetship);
        }
        model.transform.position = Vector3.MoveTowards(model.transform.position, ToWorldCoordinates(currentposint),1f*Time.deltaTime);
    }

    void SwitchShips()
    {
        if (IsShip1Active)
        {
            model = modelp2;
            IsShip1Active=false;
        }
        else
        {
            model = modelp1;
            IsShip1Active=true;
        }
    }
    public void GetTargetLocation()
    {
        targetship = input.ReadInput();
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
}
