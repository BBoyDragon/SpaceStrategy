using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


public class ControllerController : MonoBehaviour
{
    public List<ShipController> shipControllers;
    public InputFieldReader input;
    public Button switchshipButton;
    public Button startbutton;
    ShipController currentcontroller;
    Queue<ShipController> shipQueue = new Queue<ShipController>();
    public bool CanMoveShips = false;
    public bool start = false;
    public void QueueMover()
    {
        if (shipQueue.Count == 0)
        {
            foreach (ShipController controller in shipControllers)
            {
                shipQueue.Enqueue(controller);
            }
        }
        currentcontroller.GetTargetLocation(input.ReadInput());
        currentcontroller = shipQueue.Dequeue();
    }

        // Start is called before the first frame update


    public void Watasigma()
    {
        if (CanMoveShips)
        {
            CanMoveShips = false;
        }
        else 
        {
            CanMoveShips = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            
            if (shipQueue.Count == 0)
            {
                foreach (ShipController controller in shipControllers)
                {
                    shipQueue.Enqueue(controller);
                }
            }
            currentcontroller = shipQueue.Dequeue();
            start = false;
            switchshipButton.onClick.AddListener(QueueMover);
            startbutton.onClick.AddListener(Watasigma);
        }
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
