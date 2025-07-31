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
    Queue<ShipController> player1shipQueue = new Queue<ShipController>();
    Queue<ShipController> player2shipQueue = new Queue<ShipController>();
    public bool CanMoveShips = false;
    public bool IsPlayer1Turn = true;
    public void QueueMover()
    {
        if (IsPlayer1Turn)
        {
            if (player1shipQueue.Count == 0)
            {
                foreach (ShipController controller in player1shipControllers)
                {
                    player1shipQueue.Enqueue(controller);
                }
            }
            currentcontroller.GetTargetLocation(input.ReadInput());
            currentcontroller = player1shipQueue.Dequeue();
        }
        else
        {
            if (player2shipQueue.Count == 0)
            {
                foreach (ShipController controller in player2shipControllers)
                {
                    player2shipQueue.Enqueue(controller);
                }
            }
            currentcontroller.GetTargetLocation(input.ReadInput());
            currentcontroller = player2shipQueue.Dequeue();
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
        currentcontroller = player1shipQueue.Dequeue();
        if (player2shipQueue.Count == 0)
        {
            foreach (ShipController controller in player2shipControllers)
            {
                player2shipQueue.Enqueue(controller);
            }
        }
    }

    public void Watasigma()
    {
        if (IsPlayer1Turn && player1shipQueue.Count == 0)
        {
            IsPlayer1Turn = false;
        }
        else
        {
            if (CanMoveShips)
            {
                CanMoveShips = false;
            }
            else
            {
                CanMoveShips = true;
            }
            IsPlayer1Turn = true;
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
