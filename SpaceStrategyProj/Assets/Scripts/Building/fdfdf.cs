using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class HealTrigger : Heal, IPointerClickHandler
{
    // Маска слоя для кликов
    public LayerMask clickableLayer;

    // Флаг выбора объекта
    private bool isSelected;

    private void Start()
    {
        // Устанавливаем радиус по умолчанию, если не задан
        if (DistanceOfHealing == 0)
            DistanceOfHealing = 20;
    }

    // Метод, вызываемый при клике на объект
    public void OnPointerClick(PointerEventData eventData)
    {
        // Проверяем, был ли клик осуществлен левой кнопкой мыши
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Активируем лечение
            Healing();

            // Визуальная обратная связь
            Debug.Log("Лечение активировано!");
        }
    }

    // Для работы с кликами нужно добавить коллайдер
    private void OnEnable()
    {
        // Подписываемся на события кликов
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    // Опциональная функция для визуального отображения радиуса
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DistanceOfHealing);
    }
}