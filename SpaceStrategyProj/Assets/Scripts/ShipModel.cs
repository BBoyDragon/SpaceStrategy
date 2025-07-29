using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShipModel : MonoBehaviour
{
    public int energy;
    public int energyregen;
    public int shield;
    public int shieldmax;
    public int shieldregen;

    public int speed;
    public int warprange;

    public int firepowermin;
    public int firepowermax;
    public int firerange;

    public string playername;

    new Transform transform;

    public Vector3 position;

    private void Start()
    {
        transform = GetComponent<Transform>();
        Vector3 positionn = transform.position;
        position = positionn;
    }

    public ShipModel(int energy, int energyregen, int shield, int shieldmax,
        int shieldregen, int speed, int warprange, int firepowermin, int firepowermax,
        int firerange, string playername)
    {
        this.energy = energy;
        this.energyregen = energyregen;
        this.shield = shield;
        this.shieldmax = shieldmax;
        this.shieldregen = shieldregen;
        this.speed = speed;
        this.warprange = warprange;
        this.firepowermin = firepowermin;
        this.firepowermax = firepowermax;
        this.firerange = firerange;
        this.playername = playername;
    }

    public void Regenerate()
    {

    }

    public int CalculateMoveCost(int distance, bool warp)
    {
        return 0;
    }

    public int CalculateFireCost(int firemin, int firemax)
    {
        return firemax - firemin;
    }

    public void ApplyDamage(int damage)
    {

    }
}
