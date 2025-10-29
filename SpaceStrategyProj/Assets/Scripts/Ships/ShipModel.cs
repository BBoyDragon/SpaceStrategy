using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class ShipModel : MonoBehaviour
{
    private Coroutine flyCoroutine;
    public bool AF;
    public int energy;
    public int energyregen;
    public int shield;
    public int shieldmax;
    public int shieldregen;
    public int capturer;
    public ControllerController controller;
    public int speed;
    public int warprange;

    public int firepowermin;
    public int firepowermax;
    public int firerange;
    public int potentialdamage = 0;

    public string playername;

    new Transform transform;

    public Material active_material;
    public Material unactive_material;
    public MeshRenderer mesh;

    public bool readytofinalize = false;

    public Vector3 position;
    public int searchRadius = 100;
    public List<string> targetComponents = new List<string>
    {
        "ShipModel",
        "Fabric_1",
        "Healing"
    };
    private void ProcessObjectsWithHod()
    {
        // �������� ��� ���������� �� �����
        Component[] allComponents = FindObjectsOfType<Component>();

        foreach (Component component in allComponents)
        {
            // ��������� ������� ���� Hod
            FieldInfo hodField = component.GetType().GetField("Hod");

            if (hodField != null && hodField.FieldType == typeof(int))
            {
                // ������������� �������� Hod � 1
                hodField.SetValue(component, 1);
            }
        }
    }
    private void SetControllerValue()
    {
        // ������� ������ �� �����
        GameObject controllerObject = GameObject.Find("controllersigma");

        if (controllerObject != null)
        {
            // �������� ��������� ControllerController
            ControllerController controller = controllerObject.GetComponent<ControllerController>();

            if (controller != null)
            {
                // ������������� �������� true � ���������� start
                controller.IsPlayer1Turn = true;
            }
            else
            {
                Debug.LogError("��������� ControllerController �� ������ �� ������� controllersigma");
            }
        }
        else
        {
            Debug.LogError("������ controllersigma �� ������ �� �����");
        }
    }
    public int CalculateMoveCost(int distance, bool warp)
    {
        return 0;
    }
    public int CalculateFireCost(int firemin, int firemax)
    {
        return firemax - firemin;
    }
    public void Fire()
    {
        Debug.Log("C");
        GameObject controllerObject = GameObject.Find("controllersigma");
        // �������� ��� ������� � �������
        Collider[] hits = Physics.OverlapSphere(transform.position, searchRadius);

        // ������ ��������� ��������
        List<GameObject> foundObjects = new List<GameObject>();

        // ��������� ������ ������
        foreach (Collider hit in hits)
        {
            GameObject obj = hit.gameObject;

            // ��������� ������� ������ �� ������� ����������� �� �����
            foreach (string componentName in targetComponents)
            {
                // �������� ��������� �� �����
                Component component = obj.GetComponent(componentName);

                if (component != null)
                {
                    foundObjects.Add(obj);
                    break; // ���������� ��������, ���� ��������� ������
                }
            }
        }

        // ���� ������� �������
        if (foundObjects.Count > 0)
        {
            // ������� ��������� ������
            GameObject closestObject = foundObjects
                .OrderBy(obj => Vector3.SqrMagnitude(transform.position - obj.transform.position))
                .First();

            for (int i  = 0;  i < foundObjects.Count; i += 1)
            {
                if (foundObjects[i].TryGetComponent(out ShipModel shipModel))
                {
                    Debug.Log(shipModel.capturer);
                    Debug.Log(capturer);
                    if (shipModel.capturer != capturer)
                    {
                        Debug.Log("X");
                        shipModel.potentialdamage += firepowermax;
                        break;

                    }
                }
                else if (foundObjects[i].TryGetComponent(out StandardBuilding standardBuilding))
                {
                    if (standardBuilding.capturer != capturer)
                    {
                        Debug.Log("Y");
                        standardBuilding.potentialdamage += firepowermax;
                        break;
                    }
                }
            }
        }
    }


    public IEnumerator Fly()
    {
        Fire();
        ProcessObjectsWithHod();
        SetControllerValue();
        Regenerate();
        readytofinalize = true;
        StopCoroutine(Fly());
        yield return null;
    }
    private void Start()
    {
        transform = GetComponent<Transform>();
        Vector3 positionn = transform.position;
        position = positionn;
        mesh.material = unactive_material;
    }

    private void Update()
    {
    }

    public void Regenerate()
    {
        potentialdamage -= shieldregen;
        energy += energyregen;
    }
    
    public void ApplyDamage(int damage)
    {
    }

    public void SwapToActive()
    {
        mesh.material = active_material;
    }

    public void SwapToUnactive()
    {
        mesh.material = unactive_material;
    }

    public void Finalise()
    {
        shield -= potentialdamage;
        shield = Mathf.Min(shield, shieldmax);
        potentialdamage = 0;
        if (shield <= 0)
        {
            gameObject.SetActive(false);
        }
        readytofinalize = false;
    }
}