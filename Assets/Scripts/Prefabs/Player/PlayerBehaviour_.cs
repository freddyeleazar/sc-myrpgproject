using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerBehaviour_ : UnitBehaviour_
{
    private void Update()
    {
        movementType = (Input.GetKey(KeyCode.LeftShift)) ? MovementType.Run : MovementType.Walk;
    }

    public void DefaultAction(PointerEventData pointerEventData, GameObject interactingObject, string interactingObjectType)
    {
        switch (interactingObjectType)
        {
            case "Ground":
                attackTarget = null;
                target = WorldController_.GetMouseWorldPosition();
                break;
            case "Item":
                attackTarget = null;
                StoreItem(interactingObject.GetComponent<Item_>());
                break;
            case "Tree":
                attackTarget = null;
                break;
            case "NPC":
                attackTarget = interactingObject;
                Attack(attackTarget.GetComponent<Unit_>());
                break;
            default:
                break;
        }
    }

    public override void Hit(Unit_ unit)
    {
        base.Hit(unit);
        UnitBehaviour_ unitBehaviour_ = (UnitBehaviour_)unit.mechanics_.behaviour_;
        unitBehaviour_.attackTarget = owner.gameObject;
    }

    public override IEnumerator RefreshTargetEvery()
    {
        while (true)
        {
            if (attackTarget != null && stats_.hp > 0)
            {
                target = attackTarget.transform.position;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}