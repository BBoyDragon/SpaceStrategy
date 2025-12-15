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
    public int potentialdamage = 0;
    Collider[] ships_capturing;

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
        potentialdamage -= shieldRegen;
    }

    public void FinaliseBuild()
    {
        ShipModel foundmodel = null;
        shield -= potentialdamage;
        shield = Mathf.Min(shield, shieldMax);
        potentialdamage = 0;
        if (shield == 0)
        {
            gameObject.SetActive(false);
        }
        ships_capturing = Physics.OverlapSphere(this.transform.position, 15f);
        foreach (Collider collider in ships_capturing)
        {
            if (collider.TryGetComponent<ShipModel>(out foundmodel))
            {
                break;
            }
        }
        if (foundmodel != null)
        {
            capturer = foundmodel.GetComponent<ShipModel>().capturer;
        }
    }
}
