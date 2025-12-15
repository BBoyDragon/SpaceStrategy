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
    public GameObject fire_sphere;
    public GameObject projectilePrefab; 
    public float projectileSpeed = 10f;   
    
    private GameObject currentProjectile;   

    public int firepowermin;
    public int firepowermax;
    public int firerange;
    public int potentialdamage = 0;

    public string playername;

    new Transform transform;

    public MeshFilter cur_filter;
    public Mesh mesh_1;
    public Mesh mesh_2;

    public Material active_material;
    public Material unactive_material;
    public MeshRenderer mesh;

    public bool readytofinalize = false;

    public Vector3 position;
    
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
        Collider[] hits = Physics.OverlapSphere(transform.position, firerange);

        // ������ ��������� ��������
        List<GameObject> foundObjects = new List<GameObject>();

        // ��������� ������ ������
        foreach (Collider hit in hits)
        {
            GameObject obj = hit.gameObject;

            // ��������� ������� ������ �� ������� ����������� �� �����
            foreach (string componentName in targetComponents)
            {
                Component component = obj.GetComponent(componentName);
                if (component != null)
                {
                    foundObjects.Add(obj);
                    break;
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

          
            

            // ��������� ������ ��������� ����� (��� � ���������)
            for (int i = 0; i < foundObjects.Count; i++)
            {
                if (foundObjects[i].TryGetComponent(out ShipModel shipModel))
                {
                    Debug.Log(shipModel.capturer);
                    Debug.Log(capturer);
                    if (shipModel.capturer != capturer)
                    {
                        Debug.Log("X");
                        shipModel.potentialdamage += firepowermax;
                        if (projectilePrefab != null)
                        {
                            Debug.Log("���������");
                            currentProjectile = Instantiate(
                                projectilePrefab,
                                transform.position,
                                Quaternion.identity
                            );
                            currentProjectile.gameObject.transform.LookAt(shipModel.transform);
                            // ��������� �������� ������� � ����
                            StartCoroutine(MoveProjectileToTarget(currentProjectile, shipModel.gameObject));
                        }
                        break;
                    }
                }
                else if (foundObjects[i].TryGetComponent(out StandardBuilding standardBuilding))
                {
                    if (standardBuilding.capturer != capturer)
                    {
                        Debug.Log("Y");
                        standardBuilding.potentialdamage += firepowermax;
                        if (projectilePrefab != null)
                        {
                            Debug.Log("���������");
                            currentProjectile = Instantiate(
                                projectilePrefab,
                                transform.position,
                                Quaternion.identity
                            );
                            currentProjectile.gameObject.transform.LookAt(standardBuilding.transform);

                            // ��������� �������� ������� � ����
                            StartCoroutine(MoveProjectileToTarget(currentProjectile, standardBuilding.gameObject));
                        }
                        break;
                    }
                }
            }
        }
    }


    private IEnumerator MoveProjectileToTarget(GameObject projectile, GameObject target)
    {
        Debug.Log("�������� ����");
        Vector3 startPosition = projectile.transform.position;
        Vector3 targetPosition = target.transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = distance / projectileSpeed; // ����� �����
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            projectile.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        // ������ ������ ���� � ����� �������� ������ ������ ��� ����������
        OnProjectileHit(target, projectile);
    }
    private void OnProjectileHit(GameObject target, GameObject projectile)
    {
        // ����� ����� �������� ������� (�����, ���� � �.�.)
        Destroy(projectile); // ���������� ������

        // �������������: ����� ������� ���� ����� ��� ���������
        /*if (target.TryGetComponent(out ShipModel ship))
        {
            ship.potentialdamage += firepowermax;
        }
        else if (target.TryGetComponent(out StandardBuilding building))
        {
            building.potentialdamage += firepowermax;
        }*/
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
        projectileSpeed *= Time.deltaTime;
        transform = GetComponent<Transform>();
        Vector3 positionn = transform.position;
        position = positionn;
        mesh.material = unactive_material;
        fire_sphere.transform.localScale = new Vector3(firerange, firerange, firerange);

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
        cur_filter.mesh = mesh_2;
        fire_sphere.SetActive(true);
    }

    public void SwapToUnactive()
    {
        cur_filter.mesh = mesh_1;
        fire_sphere.SetActive(false);
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