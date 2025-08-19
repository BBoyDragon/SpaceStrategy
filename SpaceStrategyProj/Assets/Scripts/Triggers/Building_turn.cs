using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class Building_turn : MonoBehaviour
{
    private Collider sphereCollider;

    private void Start()
    {
        sphereCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider == sphereCollider)
            {
                // ���� ��� ������� � ����� Hod
                ProcessObjectsWithHod();
                SetControllerValue();
            }
        }
    }

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
                controller.start = true;
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
}