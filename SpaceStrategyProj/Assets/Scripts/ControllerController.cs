using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


public class ControllerController : MonoBehaviour
{
    public List<ShipController> player1shipControllers;
    public List<ShipController> player2shipControllers;
    public List<ShipController> shipControllers;
    public InputFieldReader input;
    public Button switchshipButton;
    public Button startbutton;
    ShipController currentcontroller;
    ShipModel currentmodel;
    Queue<ShipController> player1shipQueue = new Queue<ShipController>();
    Queue<ShipController> player2shipQueue = new Queue<ShipController>();
    public bool CanMoveShips = false;
    public bool IsPlayer1Turn = true;
    public void QueueMover()
    {
       if (player1shipQueue.Count!=0)
       {
            currentcontroller = player1shipQueue.Dequeue();
            currentmodel = currentcontroller.model;
            if (Mathf.Abs(currentcontroller.targetship.x - currentcontroller.currentposint.x) + Mathf.Abs(currentcontroller.targetship.y - currentcontroller.currentposint.y) +
                Mathf.Abs(currentcontroller.targetship.z - currentcontroller.currentposint.z) <= currentmodel.energy)
            {
                currentcontroller.GetTargetLocation(input.ReadInput());
                currentmodel.energy = currentmodel.energy - Mathf.Abs(currentcontroller.targetship.x - currentcontroller.currentposint.x) + Mathf.Abs(currentcontroller.targetship.y - currentcontroller.currentposint.y) +
                Mathf.Abs(currentcontroller.targetship.z - currentcontroller.currentposint.z);
            }
            else
            {
                Debug.Log("Не удалось передвинуть корабль, недостаточно энергии!");
            }
       }
       else if(player2shipQueue.Count!=0) 
       {
            currentcontroller = player2shipQueue.Dequeue();
            currentmodel = currentcontroller.model;
            if (Mathf.Abs(currentcontroller.targetship.x - currentcontroller.currentposint.x) + Mathf.Abs(currentcontroller.targetship.y - currentcontroller.currentposint.y) +
                Mathf.Abs(currentcontroller.targetship.z - currentcontroller.currentposint.z) <= currentmodel.energy)
            {
                currentcontroller.GetTargetLocation(input.ReadInput());
                currentmodel.energy = currentmodel.energy - Mathf.Abs(currentcontroller.targetship.x - currentcontroller.currentposint.x) + Mathf.Abs(currentcontroller.targetship.y - currentcontroller.currentposint.y) +
                Mathf.Abs(currentcontroller.targetship.z - currentcontroller.currentposint.z);
            }
            else
            {
                Debug.Log("Не удалось передвинуть корабль, недостаточно энергии!");
            }
       }
    }

// Start is called before the first frame update
    void Start()
    {
        switchshipButton.onClick.AddListener(QueueMover);
        startbutton.onClick.AddListener(Watasigma);
        if (player1shipQueue.Count == 0)
        {
            foreach (ShipController controller in player1shipControllers)
            {
                player1shipQueue.Enqueue(controller);
            }
        }
    }

    public void Watasigma()
    {
        if (player1shipQueue.Count == 0 && IsPlayer1Turn == true && CanMoveShips == false)
        {
            foreach (ShipController controller in player2shipControllers)
            {
                player2shipQueue.Enqueue(controller);
            }
            IsPlayer1Turn = false;
        }
        else if(player2shipQueue.Count == 0 && IsPlayer1Turn == false && CanMoveShips == false)
        {
            if (CanMoveShips)
            {
                CanMoveShips = false;
            }
            else
            {
                CanMoveShips = true;
            }
            foreach (ShipController controller in player1shipControllers)
            {
                player1shipQueue.Enqueue(controller);
            }
            IsPlayer1Turn = true;
        }
        else if(CanMoveShips == true) {
            CanMoveShips = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMoveShips)
        {
            //foreach (ShipController controller in shipControllers)
            //{
            //    if (currentcontroller.targetship == controller.targetship)
            //    {
            //        currentcontroller.targetship.x -= 1;
            //    }
            //}
            foreach(ShipController controller in shipControllers)
            {
                controller.GetStep();
            }
        }
    }

    public void OnDestroy() 
    { 
        switchshipButton.onClick.RemoveAllListeners();
    }
}
