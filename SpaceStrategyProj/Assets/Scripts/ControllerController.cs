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
    public List<StandardBuilding> buildings;
    public List<GameObject> predicts;
    public InputFieldReader input;
    public Button switchshipButton;
    public GameObject cubePrefab;
    public Button startbutton;
    public Button center_camera;
    public TextMeshProUGUI myText;
    public TextMeshProUGUI shipstext;
    public TextMeshProUGUI energytext;
    public TextMeshProUGUI shieldtext;
    public TextMeshProUGUI firepowertext;
    public GameObject cur_camera;
    ShipController currentcontroller;
    ShipModel currentmodel;
    Queue<ShipController> player1shipQueue = new Queue<ShipController>();
    Queue<ShipController> player2shipQueue = new Queue<ShipController>();
    public bool CanMoveShips = false;
    public bool IsPlayer1Turn = true;
    public int ships_went;
    public AudioClip click_sound;
//    public AudioClip test1_sound;
//    public AudioClip test2_sound;
    public List<AudioSource> buttons_sources;
    public AudioSource background_music;
    public GameObject ship_predict;
    public GameObject line_predict;
    public Material predict_material;

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

    public static void IncrementHodForAllBuildings()
    {
        StandardBuilding[] buildings = Object.FindObjectsOfType<StandardBuilding>();

        foreach (StandardBuilding building in buildings)
        {
            building.Hod += 1;
        }
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
       buttons_sources[0].Play();
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
                shipstext.text = "Not enough energy, try to reduce the range!";
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
                energytext.text = player1shipQueue.Peek().model.energy.ToString() + " Energy";
                shieldtext.text = player1shipQueue.Peek().model.shield.ToString() + " Shield";
                firepowertext.text = player1shipQueue.Peek().model.firepowermax.ToString() + " Damage";
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
                shipstext.text = "Not enough energy, try to reduce the range!";
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
                energytext.text = player2shipQueue.Peek().model.energy.ToString() + " Energy";
                shieldtext.text = player2shipQueue.Peek().model.shield.ToString() + " Shield";
                firepowertext.text = player2shipQueue.Peek().model.firepowermax.ToString() + " Damage";
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
        myText.text = "Player 1 Turn";
        shipControllers = ShipControllers;
        while (!player1shipQueue.Peek().gameObject.activeSelf)
        {
            player1shipQueue.Dequeue();
        }
        player1shipQueue.Peek().model.SwapToActive();
        energytext.text = player1shipQueue.Peek().model.energy.ToString() + " Energy";
        shieldtext.text = player1shipQueue.Peek().model.shield.ToString() + " Shield";
        firepowertext.text = player1shipQueue.Peek().model.firepowermax.ToString() + " Damage";
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
        background_music.Play();
        buttons_sources[0].clip = click_sound;
        buttons_sources[2].clip = click_sound;
        buttons_sources[1].clip = click_sound;
        switchshipButton.onClick.AddListener(QueueMover);
        startbutton.onClick.AddListener(Watasigma);
        center_camera.onClick.AddListener(Senter_Game_Camera);
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
        myText.text = "Player 1 Turn";
        shipstext.text = energytext.text = shieldtext.text = firepowertext.text = "";
        while (!player1shipQueue.Peek().gameObject.activeSelf)
        {
            player1shipQueue.Dequeue();
        }
        player1shipQueue.Peek().model.SwapToActive();
        energytext.text = player1shipQueue.Peek().model.energy.ToString() + " Energy";
        shieldtext.text = player1shipQueue.Peek().model.shield.ToString() + " Shield";
        firepowertext.text = player1shipQueue.Peek().model.firepowermax.ToString() + " Damage";
        for (int i = 0; i <= 100; i += 10) 
            for (int j = 0; j <= 100; j += 10)
                for (int k = 0; k <= 100; k += 10)
                    Instantiate(cubePrefab, new Vector3(i,j,k), Quaternion.identity);
        ship_predict.transform.position = new Vector3(1234, 1234, 1234);
        float lineWidth = 0.1f;
        line_predict.transform.parent = this.transform; // Делаем текущий объект родителем
        line_predict.transform.localPosition = Vector3.zero;

        LineRenderer lr = line_predict.AddComponent<LineRenderer>();

        // Настраиваем Line Renderer
        lr.material = predict_material;
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.positionCount = 2;
        lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        lr.receiveShadows = false;
        lr.useWorldSpace = true;
        line_predict.isStatic = true;
    }

    public void Watasigma()
    {
        buttons_sources[1].Play();
        if (IsPlayer1Turn == true && CanMoveShips == false)
        {
            foreach (ShipController controller in player2shipControllers)
            {
                player2shipQueue.Enqueue(controller);
            }
            IsPlayer1Turn = false;
            switchshipButton.interactable = true;
            startbutton.interactable = false;
            myText.text = "Player 2 Turn";
            while (!player2shipQueue.Peek().gameObject.activeSelf)
            {
                player2shipQueue.Dequeue();
            }
            player2shipQueue.Peek().model.SwapToActive();
            energytext.text = player2shipQueue.Peek().model.energy.ToString() + " Energy";
            shieldtext.text = player2shipQueue.Peek().model.shield.ToString() + " Shield";
            firepowertext.text = player2shipQueue.Peek().model.firepowermax.ToString() + " Damage";
        }
        else if(IsPlayer1Turn == false && CanMoveShips == false)
        {
            foreach (ShipController controller in player1shipControllers)
            {
                player1shipQueue.Enqueue(controller);
            }
            IsPlayer1Turn = true;
            myText.text = "Player 1 Turn";
            IncrementHodForAllBuildings();
            StartCoroutine(WaitForShips());
        }
    }

    public void Senter_Game_Camera()
    {
        buttons_sources[2].Play();
        cur_camera.transform.position = currentmodel.transform.position;
        cur_camera.transform.position = new Vector3(cur_camera.transform.position.x, cur_camera.transform.position.y + 2, cur_camera.transform.position.z);
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
            foreach (ShipController controller in shipControllers)
            {
                controller.GetStep();
                if (Vector3.Distance(controller.ToWorldCoordinates(controller.targetship), controller.transform.position) < 1)
                {
                    ships_went += 1;
                    shipControllers = shipControllers.Where(x => Vector3.Distance(x.transform.position, x.ToWorldCoordinates(x.targetship)) > 1).ToList();
                }
            }
        }
        else
        {
            Draw_Prediction();
        }
    }

    public void  Draw_Prediction()
    {
    ShipController shiptemp;
    if (IsPlayer1Turn && player1shipQueue.Count() > 0) { shiptemp = player1shipQueue.Peek(); }
    else if (!IsPlayer1Turn && player2shipQueue.Count() > 0) { shiptemp = player2shipQueue.Peek(); }
    else { return; }
    LineRenderer lr = line_predict.GetComponent<LineRenderer>();

    if (shiptemp.currentposint != input.ReadInput())
    {
        lr.SetPosition(0, shiptemp.gameObject.transform.position);
        lr.SetPosition(1, shiptemp.ToWorldCoordinates(input.ReadInput()));
        ship_predict.transform.position = shiptemp.ToWorldCoordinates(input.ReadInput());
    }
    }

    public void OnDestroy() 
    { 
        switchshipButton.onClick.RemoveAllListeners();
    }
}
