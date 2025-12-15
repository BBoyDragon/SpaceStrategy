using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using TMPro;
using JetBrains.Annotations;


public class ControllerController : MonoBehaviour
{

    public List<ShipController> player1shipControllers;
    public List<ShipController> player2shipControllers;
    public List<ShipController> shipControllers;
    public List<ShipController> ShipControllers;
    public InputFieldReader input;
    public Button switchshipButton;
    public Button startbutton;
    public TextMeshProUGUI myText;
    public TextMeshProUGUI shipstext;
    public TextMeshProUGUI energytext;
    public TextMeshProUGUI shieldtext;
    public TextMeshProUGUI firepowertext;
    ShipController currentcontroller;
    ShipModel currentmodel;
    Queue<ShipController> player1shipQueue = new Queue<ShipController>();
    Queue<ShipController> player2shipQueue = new Queue<ShipController>();
    public bool CanMoveShips = false;
    public bool IsPlayer1Turn = true;
    public int ships_went;

    public int count_active_ships()
    {
        int count_of_ships = 0;
        for (int i = 0; i < ShipControllers.Count(); i++)
        {
            if (ShipControllers[i].gameObject.activeSelf)
            {
                count_of_ships++;
            }
        }
        return count_of_ships;
    }

    public bool full_disactive(Queue<ShipController> Sobludayu_kod_staylik)
    {
        bool ans = true;
        foreach (ShipController a in Sobludayu_kod_staylik) {
            if (a.gameObject.activeSelf) { ans = false; break; }
        }
        return ans;
    }
    public void QueueMover()
    {
       if (player1shipQueue.Count != 0 && !full_disactive(player1shipQueue))
       {
            while (!player1shipQueue.Peek().gameObject.activeSelf)
            {
                currentcontroller = player1shipQueue.Dequeue();
            }
            ShipController shiptemp = player1shipQueue.Peek();
            
            shiptemp.GetTargetLocation(input.ReadInput());
            if (!(Mathf.Abs(shiptemp.targetship.x - shiptemp.currentposint.x) + Mathf.Abs(shiptemp.targetship.y - shiptemp.currentposint.y) +
                Mathf.Abs(shiptemp.targetship.z - shiptemp.currentposint.z) <= shiptemp.model.energy))
            {
                shipstext.text = "Не удалось передвинуть корабль, недостаточно энергии!";
                return;
            }
            shipstext.text = "";
            currentcontroller = player1shipQueue.Dequeue();
            
            currentmodel = currentcontroller.model;
            currentmodel.SwapToUnactive();

            if (player1shipQueue.Count != 0 && !full_disactive(player1shipQueue)) 
            {
                while (!player1shipQueue.Peek().gameObject.activeSelf)
                {
                    player1shipQueue.Dequeue();
                }
                player1shipQueue.Peek().model.SwapToActive();
                energytext.text = player1shipQueue.Peek().model.energy.ToString() + " энергии";
                shieldtext.text = player1shipQueue.Peek().model.shield.ToString() + " щита";
                firepowertext.text = player1shipQueue.Peek().model.firepowermax.ToString() + " огневой мощи";
            }
            currentcontroller.GetTargetLocation(input.ReadInput());
            currentmodel.energy = currentmodel.energy - Mathf.Abs(currentcontroller.targetship.x - currentcontroller.currentposint.x) - Mathf.Abs(currentcontroller.targetship.y - currentcontroller.currentposint.y) -
            Mathf.Abs(currentcontroller.targetship.z - currentcontroller.currentposint.z);
       }
       else if(player2shipQueue.Count != 0 && !full_disactive(player2shipQueue)) 
       {
            while (!player2shipQueue.Peek().gameObject.activeSelf)
            {
                currentcontroller = player2shipQueue.Dequeue();
            }
            ShipController shiptemp = player2shipQueue.Peek();
            shiptemp.GetTargetLocation(input.ReadInput());
            if (!(Mathf.Abs(shiptemp.targetship.x - shiptemp.currentposint.x) + Mathf.Abs(shiptemp.targetship.y - shiptemp.currentposint.y) +
                Mathf.Abs(shiptemp.targetship.z - shiptemp.currentposint.z) <= shiptemp.model.energy))
            {
                shipstext.text = "Не удалось передвинуть корабль, недостаточно энергии!";
                return;
            }
            shipstext.text = "";
            currentcontroller = player2shipQueue.Dequeue();
            currentmodel = currentcontroller.model;
            currentmodel.SwapToUnactive();
            if (player2shipQueue.Count != 0 && !full_disactive(player2shipQueue))
            {
                while (!player2shipQueue.Peek().gameObject.activeSelf)
                {
                    player2shipQueue.Dequeue();
                }
                player2shipQueue.Peek().model.SwapToActive();
                energytext.text = player2shipQueue.Peek().model.energy.ToString() + " энергии";
                shieldtext.text = player2shipQueue.Peek().model.shield.ToString() + " щита";
                firepowertext.text = player2shipQueue.Peek().model.firepowermax.ToString() + " огневой мощи";
            }
            currentcontroller.GetTargetLocation(input.ReadInput());
            currentmodel.energy = currentmodel.energy - Mathf.Abs(currentcontroller.targetship.x - currentcontroller.currentposint.x) - Mathf.Abs(currentcontroller.targetship.y - currentcontroller.currentposint.y) -
            Mathf.Abs(currentcontroller.targetship.z - currentcontroller.currentposint.z);
       }
        if ((player1shipQueue.Count == 0 || full_disactive(player1shipQueue)) && IsPlayer1Turn == true)
        {
            switchshipButton.interactable = false;
            startbutton.interactable = true;
        }
        else if ((player2shipQueue.Count == 0 || full_disactive(player2shipQueue)) && IsPlayer1Turn == false) 
        {
            switchshipButton.interactable = false;
            startbutton.interactable = true;
        }
    }

    IEnumerator WaitForShips()
    {
        ships_went = 0;
        currentmodel.SwapToUnactive();
        CanMoveShips = true;
        ShipControllers = shipControllers;
        
        switchshipButton.interactable = false;
        startbutton.interactable = false;
        while (ships_went != count_active_ships())
        {
            yield return null;
        }
        foreach (ShipController shipcon in ShipControllers)
        {
            if (shipcon.gameObject.activeSelf)
            {
                shipcon.model.StartCoroutine(shipcon.model.Fly());
            }
        }
        switchshipButton.interactable = true;
        startbutton.interactable = false;
        CanMoveShips = false;
        myText.text = "Очередь 1 игрока";
        shipControllers = ShipControllers;
        while (!player1shipQueue.Peek().gameObject.activeSelf)
        {
            player1shipQueue.Dequeue();
        }
        player1shipQueue.Peek().model.SwapToActive();
        energytext.text = player1shipQueue.Peek().model.energy.ToString() + " энергии";
        shieldtext.text = player1shipQueue.Peek().model.shield.ToString() + " щита";
        firepowertext.text = player1shipQueue.Peek().model.firepowermax.ToString() + " огневой мощи";
        foreach (ShipController shipcon in ShipControllers)
        {
            shipcon.model.Finalise();
        }
        foreach (StandardBuilding building in GameObject.FindObjectsOfType<StandardBuilding>())
        {
            building.FinaliseBuild();
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
        ships_went = 0;
        ShipControllers = shipControllers;
        switchshipButton.interactable = true;
        startbutton.interactable = false;
        CanMoveShips = false;
        myText.text = "Очередь 1 игрока";
        shipstext.text = energytext.text = shieldtext.text = firepowertext.text = "";
        while (!player1shipQueue.Peek().gameObject.activeSelf)
        {
            player1shipQueue.Dequeue();
        }
        player1shipQueue.Peek().model.SwapToActive();
        energytext.text = player1shipQueue.Peek().model.energy.ToString() + " энергии";
        shieldtext.text = player1shipQueue.Peek().model.shield.ToString() + " щита";
        firepowertext.text = player1shipQueue.Peek().model.firepowermax.ToString() + " огневой мощи";
    }

    public void Watasigma()
    {
        if (IsPlayer1Turn == true && CanMoveShips == false)
        {
            foreach (ShipController controller in player2shipControllers)
            {
                player2shipQueue.Enqueue(controller);
            }
            IsPlayer1Turn = false;
            switchshipButton.interactable = true;
            startbutton.interactable = false;
            myText.text = "Очередь 2 игрока";
            while (!player2shipQueue.Peek().gameObject.activeSelf)
            {
                player2shipQueue.Dequeue();
            }
            player2shipQueue.Peek().model.SwapToActive();
            energytext.text = player2shipQueue.Peek().model.energy.ToString() + " энергии";
            shieldtext.text = player2shipQueue.Peek().model.shield.ToString() + " щита";
            firepowertext.text = player2shipQueue.Peek().model.firepowermax.ToString() + " огневой мощи";
        }
        else if(IsPlayer1Turn == false && CanMoveShips == false)
        {
            foreach (ShipController controller in player1shipControllers)
            {
                player1shipQueue.Enqueue(controller);
            }
            IsPlayer1Turn = true;
            myText.text = "Корабли летят";
            StartCoroutine(WaitForShips());
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
                if (Vector3.Distance(controller.ToWorldCoordinates(controller.targetship),controller.transform.position) < 1)
                {
                    ships_went += 1;
                    shipControllers = shipControllers.Where(x => Vector3.Distance(x.transform.position,x.ToWorldCoordinates(x.targetship)) > 1).ToList(); 
                }
            }
        }
    }

    public void OnDestroy() 
    { 
        switchshipButton.onClick.RemoveAllListeners();
    }
}
