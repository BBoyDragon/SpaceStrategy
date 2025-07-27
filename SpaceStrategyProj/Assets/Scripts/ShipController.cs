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
    public ShipModel shipred1;
    public ShipModel shipred2;
    public ShipModel shipred3;
    public ShipModel shipblue1;
    public ShipModel shipblue2;
    public ShipModel shipblue3;
    public InputFieldReader input;
    public Button switchButton;
    Queue<ShipModel> shipQueue = new Queue<ShipModel>();

    private void Start()
    {
        shipQueue.Enqueue(shipred1);
        shipQueue.Enqueue(shipred2);
        shipQueue.Enqueue(shipred3);
        shipQueue.Enqueue(shipblue1);
        shipQueue.Enqueue(shipblue2);
        shipQueue.Enqueue(shipblue3);
        model = shipQueue.Dequeue();
        switchButton.onClick.AddListener(QueueMover);
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

    public void QueueMover()
    {
        if (shipQueue.Count == 0)
        {
            shipQueue.Enqueue(shipred1);
            shipQueue.Enqueue(shipred2);
            shipQueue.Enqueue(shipred3);
            shipQueue.Enqueue(shipblue1);
            shipQueue.Enqueue(shipblue2);
            shipQueue.Enqueue(shipblue3);
        }
        model = shipQueue.Dequeue();
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
