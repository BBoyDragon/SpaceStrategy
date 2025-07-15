// Базовый класс
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StandardBuilding : MonoBehaviour
{
    // Поля класса
    public int shield;
    public int shieldMax;
    public int shieldRegen;
    public int id;
    public int capturer = 0; // 0 - никто, 1 - первый игрок, 2 - второй игрок

    // Конструктор по умолчанию
    public StandardBuilding()
    {
        shield = 0;
        shieldMax = 100;
        shieldRegen = 1;
        id = -1;
        capturer = 0;
    }

    // Параметризованный конструктор
    public StandardBuilding(int initialShield, int maxShield, int regenRate, int buildingId, int owner)
    {
        shield = initialShield;
        shieldMax = maxShield;
        shieldRegen = regenRate;
        id = buildingId;
        capturer = owner;
    }

    public bool IsShieldDestroyed()
    {
        return shield <= 0;
    }

    public int GetDamage(int damage, int currentShield)
    {
        return Mathf.Max(0, currentShield - damage);
    }

    public int SearchShieldDestroyer()
    {
        return 0; // Логика поиска разрушителя
    }

    public void Regenerate()
    {
        shield = Mathf.Min(shieldMax, shield + shieldRegen);
    }
}
