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

    public string playername;

    new Transform transform;

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
                        shipModel.shield = Mathf.Max(0, shipModel.shield - firepowermax);
                        if (shipModel.shield == 0)
                        {
                            foundObjects[i].SetActive(false);
                        }
                        break;

                    }
                }
                else if (foundObjects[i].TryGetComponent(out StandardBuilding standardBuilding))
                {
                    if (standardBuilding.capturer != capturer)
                    {
                        Debug.Log("Y");
                        standardBuilding.shield = Mathf.Max(0, standardBuilding.shield - firepowermax);
                        if (standardBuilding.shield == 0)
                        {
                            foundObjects[i].SetActive(false);
                        }
                        break;
                    }
                }
            }
        }
    }


    public bool CMS()
    {
        
        GameObject controllerObject = GameObject.Find("controllersigma");
        bool A = false;

        if (controllerObject != null)
        {

            ControllerController controller = controllerObject.GetComponent<ControllerController>();


            if (controller != null)
            {

                 A = controller.CanMoveShips;

                

            }
        }
        return A;
    }
    IEnumerator Fly()
    {
        AF = true;
        Debug.Log("B");
        while (CMS()) {

            yield return new WaitForSeconds(0.5f);
        }
        
        Debug.Log(CMS());
        Debug.Log("D");
        AF = false;
        Fire();
        ProcessObjectsWithHod();
        SetControllerValue();
        Regenerate();
    }
    private void Start()
    {
        transform = GetComponent<Transform>();
        Vector3 positionn = transform.position;
        position = positionn;
    }

    private void Update()
    {
        
        if (CMS() && !AF)
        {
            Debug.Log("A");
            flyCoroutine = StartCoroutine(Fly());
            
        }
        
    }

    public void Regenerate()
    {
        shield += shieldregen;
    }
    
    public void ApplyDamage(int damage)
    {
    }

}

     

    

     



