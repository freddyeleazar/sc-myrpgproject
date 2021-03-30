using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitStats_ : Stats_
{
    public UnitType unitType;
    public float walkSpeed;
    public float runSpeed;
    public float strenght;

    public string unitName;
    public int maxHp;
    public int hp;
    public int courage;
    public int level;
    public Diplomacy diplomacy;

    public Relationship playerRelationShip;

    [ShowInInspector]
    public List<Relationship> relationships = new List<Relationship>();

    public List<Unit_> allUnits = new List<Unit_>();

    public UnitInventory_ inventory;

    public void Awake()
    {
        allUnits = FindObjectsOfType<Unit_>().ToList();

        allUnits.ForEach(unit => relationships.Add(new Relationship(unit, GetRelationShipLevel(unit))));
        /*
        Player_ player = FindObjectOfType<Player_>();
        relationships.Add(new Relationship((Unit_)owner, 1));
        relationships.Add(new Relationship(player, -1));
        */
        inventory = GetComponentInParent<Unit_>().gameObject.GetComponentInChildren<UnitInventory_>();
    }

    public int GetRelationShipLevel(Unit_ unit)
    {
        if (this.diplomacy == ((UnitStats_)unit.mechanics_.stats_).diplomacy)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    public float GetSpeed(MovementType movementType)
    {
        switch (movementType)
        {
            case MovementType.Walk:
                return walkSpeed / 3.6f;
            case MovementType.Run:
                return runSpeed / 3.6f;
            default:
                return 0;
        }
    }

    public int GetHitDamage()
    {
        int attackDamage = 10;
        Item_ equippedWeapon = inventory.GetEquippedItems().Find(t => t.itemType == ItemType.Weapon);
        if(equippedWeapon != null)
        {
            int weaponDamage = inventory.GetEquippedItems().Find(i => i.itemType == ItemType.Weapon).damage;
            attackDamage += weaponDamage;
        }
        return attackDamage;
    }
}

public enum UnitType
{
    Player,
    NPC,
    Animal
}

public class Relationship
{
    public Unit_ unit;
    public int level;

    public Relationship(Unit_ unit, int level)
    {
        this.unit = unit;
        this.level = level;
    }
}

public enum Diplomacy
{
    Enemy,
    Allied
}