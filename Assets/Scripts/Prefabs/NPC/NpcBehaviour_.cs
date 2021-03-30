using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class NpcBehaviour_ : UnitBehaviour_
{
    public float decideRefreshTime;
    public float attackRefreshTime;

    public Item_ pants;
    public Item_ polera;

    public void Start()
    {
        if(pants != null) EquipItem(pants);
        if (polera != null) EquipItem(polera);
        StartCoroutine(DecideEvery(decideRefreshTime));
        StartCoroutine(AttackEvery(attackRefreshTime));
    }

    public IEnumerator DecideEvery(float seconds)
    {
        while (true)
        {
            Decide();
            yield return new WaitForSeconds(seconds);
        }
    }

    public IEnumerator AttackEvery(float seconds)
    {
        while (true)
        {
            if (attackTarget != null && ((UnitStats_)attackTarget.GetComponent<Unit_>().mechanics_.stats_).hp >= 0)
            {
                Attack(attackTarget.GetComponent<Unit_>());
            }
            yield return new WaitForSeconds(seconds);
        }
    }

    public override IEnumerator RefreshTargetEvery()
    {
        while (true)
        {
            if (attackTarget != null)
            {
                targetAttackDistance = WorldController_.DistanceBetween(owner.transform.position, attackTarget.transform.position);
                if (targetAttackDistance <= visualPerception_.visualDistance && stats_.hp > 0)
                {
                    target = attackTarget.transform.position;
                }
                else
                {
                    attackTarget = null;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Decide()
    {
        foreach (Base_ baseObject in visualPerception_.baseObjectsInVisualField)
        {
            switch (baseObject.baseType_)
            {
                case BaseType_.Item:
                    break;
                case BaseType_.Unit:
                    Unit_ unit = ((Unit_)baseObject);
                    if (IsEnemy(unit) && stats_.hp >= 0)//Si es mi enemigo, si aún está vivo y si aún yo estoy vivo...
                    {
                        if(((UnitStats_)unit.mechanics_.stats_).hp >= 0)
                        {
                            attackTarget = unit.gameObject;
                        }
                        else
                        {
                            attackTarget = null;
                        }
                    }
                    break;
                case BaseType_.Tree:
                    break;
                default:
                    break;
            }
        }
    }

    public bool IsEnemy(Unit_ unit)
    {
        bool isEnemy = false;
        Relationship rs = stats_.relationships.Find(t => t.unit == unit);

        if(rs.level < 0)
        {
            isEnemy = true;
        }
        return isEnemy;
    }
}