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
        Debug.Log("skfurehvuifhvuivuhirbhfkfndkjbfdjhv fdu vfjvbvvh b g gv vfj bfhj vfh  f");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider == sphereCollider)
            {
                // Ищем все объекты с полем Hod
                ProcessObjectsWithHod();
                SetControllerValue();
            }
        }
    }

    private void ProcessObjectsWithHod()
    {
        // Получаем все компоненты на сцене
        Component[] allComponents = FindObjectsOfType<Component>();

        foreach (Component component in allComponents)
        {
            // Проверяем наличие поля Hod
            FieldInfo hodField = component.GetType().GetField("Hod");

            if (hodField != null && hodField.FieldType == typeof(int))
            {
                // Устанавливаем значение Hod в 1
                hodField.SetValue(component, 1);
            }
        }
    }
    private void SetControllerValue()
    {
        // Находим объект по имени
        GameObject controllerObject = GameObject.Find("controllersigma");

        if (controllerObject != null)
        {
            // Получаем компонент ControllerController
            ControllerController controller = controllerObject.GetComponent<ControllerController>();

            if (controller != null)
            {
                // Устанавливаем значение true в переменную start
                controller.IsPlayer1Turn = true;
            }
            else
            {
                Debug.LogError("Компонент ControllerController не найден на объекте controllersigma");
            }
        }
        else
        {
            Debug.LogError("Объект controllersigma не найден на сцене");
        }
    }
}