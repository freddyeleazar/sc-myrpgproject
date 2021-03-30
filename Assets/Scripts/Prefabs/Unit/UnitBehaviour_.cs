using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitBehaviour_ : Behaviour_
{
    public BottomUi bottomUi;

    public UnitStats_ stats_;
    public UnitInventory_ inventory_;
    public UnitVisualPerception_ visualPerception_;
    public UnitCommunication_ unitCommunication_;

    public Animator unitAnimator;
    public WorldBaseGround_ baseGround_;
    public Rigidbody2D unitRigidBody2D;
    public GameObject attackTarget;
    public Vector2 target;
    public MovementType movementType;
    public float targetAttackDistance;

    private void Awake()
    {
        unitRigidBody2D = ((Unit_)owner).rigidbody2D_;
        stats_ = (UnitStats_)((Unit_)owner).mechanics_.stats_;
        unitAnimator = GetUnitAnimator();
        inventory_ = (UnitInventory_)((Unit_)owner).inventory_;
        target = owner.transform.position;
    }

    private void FixedUpdate()
    {
        if (IsTargetReached())
        {
            StopMovement();
        }
        else
        {
            MoveTo(target, movementType);
        }

        LookAt(target);
    }

    #region Movement Behaviour
    public void StopMovement()
    {
        unitRigidBody2D.velocity = Vector2.zero;
        unitAnimator.SetFloat("speed", 0);
        GetEquippedItemAnimators().ForEach(t => t.SetFloat("speed", 0));
    }

    public void MoveTo(Vector2 target, MovementType movementType)
    {
        if(stats_.hp >= 0)
        {
            unitRigidBody2D.velocity = (target - unitRigidBody2D.position).normalized * stats_.GetSpeed(movementType);
            SetSpeedParameterToAnimators(movementType);
        }
    }

    public void LookAt(Vector2 target)
    {
        if(stats_.hp >= 0)
        {
            Transform auxiliaryTransform = transform;
            auxiliaryTransform.LookAt(target);
            auxiliaryTransform.right = target - unitRigidBody2D.position;
            float angle = auxiliaryTransform.rotation.eulerAngles.z / (360 - 45);
            unitAnimator.SetFloat("angle", angle);
            GetEquippedItemAnimators().ForEach(t => t.SetFloat("angle", angle));
        }
    }

    public bool IsTargetReached()
    {
        return (WorldController_.DistanceBetween(target, unitRigidBody2D.position) < 0.4) ? true : false;
    }

    public void MoveToSelectedObject()
    {
        if(stats_.hp >= 0)
        {
            if (bottomUi.currentSelectedObject == null)//Is Ground Tile
            {
                MoveToSelectedGroundTile();
            }
            else
            {
                target = bottomUi.currentSelectedObject.transform.position;
            }
        }
    }

    private void MoveToSelectedGroundTile()
    {
        if(stats_.hp >= 0)
        {
            target = new Vector2(baseGround_.baseGroundTilemap.CellToWorld(baseGround_.tilePosition).x, baseGround_.baseGroundTilemap.CellToWorld(baseGround_.tilePosition).y);
        }
    }

    #endregion

    #region Inventory Behaviour
    [Button]
    public void StoreItem(Item_ item)
    {
        if (inventory_.IsInInventory(item))//Si el ítem ya está en alguna ranura del inventario
        {
            if (!inventory_.IsStored(item))//Si no está en la ranura genérica
            {
                if (inventory_.IsEquipped(item))//Si está equipado
                    item.graphics_.inGame_.gameObject.SetActive(false);//No mostrarlo, ya que se "almacenará" o "guardará" (no debe quedar a la vista)
                Slot_ oldSlot = item.occupyingSlot;
                oldSlot.RemoveItem(item);//Quitarlo de la ranura en que se encontraba
                inventory_.genericSlot.AddItem(item);//Ponerlo en la ranura genérica ("almacenarlo")
            }
            else//Y si ya está en la ranura genérica ("almacenado")
            {
                Debug.Log($"Item {item.name} is already in the generic slot. It isn't necessary re-store it.");
            }
        }
        else//Y si no está en el inventario
        {
            if (Vector3.Distance(item.transform.position, owner.transform.position) <= 1)//Si el objeto esta a lo más a un metro ingame de distancia
            {
                float itemWeight = ((ItemStats_)item.mechanics_.stats_).weight;
                if ((inventory_.currentWeight + itemWeight) <= inventory_.weightCapacity)//Si el jugador es capaz de llevar el peso adicional del ítem
                {
                    inventory_.genericSlot.AddItem(item);//Simplemente ponerlo en la ranura genérica ("almacenarlo")
                    inventory_.RefreshCurrentWeight();//Adicionar el peso del ítem al peso total cargado por el jugador
                    item.graphics_.inGame_.gameObject.SetActive(false);//No mostrarlo, ya que se "almacenará" o "guardará" (no debe quedar a la vista)
                }
                else
                {
                    Debug.Log($"{owner.name} can't carry item {item.name}, because his weight capacity would be exceeded.");
                }
            }
            else
            {
                Debug.Log($"{owner.name} can't carry Item {item.name}, because it is too far ({Vector3.Distance(item.transform.position, owner.transform.position)} m).");
            }
        }
    }

    [Button]
    public void DropItem(Item_ item)
    {
        if (inventory_.IsInInventory(item))//Si el ítem está en alguna ranura del inventario
        {
            Slot_ oldSlot = item.occupyingSlot;
            oldSlot.RemoveItem(item);//Quitarlo de la ranura en que se encuentra
            inventory_.RefreshCurrentWeight();//Rebajar el peso
            if (!item.graphics_.inGame_.gameObject.activeSelf)//Si la gráfica ingame no está activa
                item.graphics_.inGame_.gameObject.SetActive(true);
            item.graphics_.inGame_.animator.SetTrigger("isDropped");//Iniciar animación "isDropped" del ítem
        }
    }

    [Button]
    public void EquipItem(Item_ item)
    {
        if (inventory_.IsInInventory(item))//Si el ítem ya está en alguna ranura del inventario
        {
            if (!inventory_.IsEquipped(item))//Si el ítem no está equipado
            {
                Slot_ oldSlot = item.occupyingSlot;
                oldSlot.RemoveItem(item);//Quitarlo de la ranura en que se encontraba
                inventory_.slots.Find(t => t.slotType == item.equippableSlot).AddItem(item);//Ponerlo en la ranura capaz de equipar el ítem
                if (!item.graphics_.inGame_.gameObject.activeSelf)//Si la gráfica ingame no estaba activa
                    item.graphics_.inGame_.gameObject.SetActive(true);//...activarla
                item.graphics_.inGame_.animator.SetTrigger("isEquipped");//Equipar visualmente el ítem en el personaje (iniciar animación "isEquipped" del ítem)
                SyncAnimators();
            }
            else//Si ya está equipado
            {
                Debug.Log($"Item {item.name} is already in the equippable slot. It is not necessary re-equip it.");
            }
        }
        else//Y si no está en el inventario
        {
            if (Vector3.Distance(item.transform.position, owner.transform.position) <= 1)
            {
                float itemWeight = ((ItemStats_)item.mechanics_.stats_).weight;
                if (inventory_.currentWeight + itemWeight <= inventory_.weightCapacity)//Si el jugador puede cargar con el peso adicional del ítem
                {
                    inventory_.slots.Find(t => t.slotType == item.equippableSlot).AddItem(item);//Simplemente equiparlo (ponerlo en la ranura del inventario capaz de equipar el ítem)
                    if (!item.graphics_.inGame_.gameObject.activeSelf)//Si la gráfica ingame no estaba activa
                        item.graphics_.inGame_.gameObject.SetActive(true);//...activarla
                    item.graphics_.inGame_.animator.SetTrigger("isEquipped");//Equipar visualmente el ítem en el personaje (iniciar animación "isEquipped" del ítem)
                    inventory_.RefreshCurrentWeight();//Adicionar el peso del personaje al peso total llevado por el jugador
                    SyncAnimators();
                }
                else
                {
                    Debug.Log($"{owner.name} can't equip item {item.name}, because his weight capacity would be exceeded.");
                }
            }
            else
            {
                Debug.Log($"{owner.name} can't equip item {item.name}, because it is too far ({Vector3.Distance(item.transform.position, owner.transform.position)} m).");
            }
        }
    }


    public void StoreSelectedItem()
    {
        Item_ selectedItem = bottomUi.currentSelectedObject.GetComponent<Item_>();
        StoreItem(selectedItem);
    }

    public void DropSelectedItem()
    {
        Item_ selectedItem = bottomUi.currentSelectedObject.GetComponent<Item_>();
        DropItem(selectedItem);
    }

    public void EquipSelectedItem()
    {
        Item_ selectedItem = bottomUi.currentSelectedObject.GetComponent<Item_>();
        EquipItem(selectedItem);
    }
    #endregion

    #region Attack Behaviour
    public void Attack(Unit_ unit)
    {
        if(stats_.hp >= 0)
        {
            attackTarget = unit.gameObject;
            if (WorldController_.DistanceBetween(owner.transform.position, unit.transform.position) < 0.5)
            {
                LookAt(unit.transform.position);
                GetUnitAnimator().SetTrigger("isAttacking");
                GetEquippedItemAnimators().ForEach(t => t.SetTrigger("isAttacking"));
                SyncAnimators();
            }
            else
            {
                StartCoroutine(RefreshTargetEvery());
            }
        }
    }

    public virtual IEnumerator RefreshTargetEvery()
    {
        yield return new WaitForSeconds(0.1f);
    }

    public void AttackSelectedUnit()
    {
        if(stats_.hp >= 0)
        {
            attackTarget = bottomUi.currentSelectedObject.gameObject;
            Attack(attackTarget.GetComponent<Unit_>());
        }
    }

    public virtual void Hit(Unit_ unit)
    {
        UnitStats_ unitStats = (UnitStats_)unit.mechanics_.stats_;
        unitStats.hp -= stats_.GetHitDamage() / 2;
        unit.graphics_.inGame_.animator.SetFloat("hp", unitStats.hp);
        ((UnitBehaviour_)unit.mechanics_.behaviour_).GetEquippedItemAnimators().ForEach(t => t.SetFloat("hp", unitStats.hp));
        if(unit.GetType() == typeof(Player_))
        {
            PlayerBottomUiMiddleContainer_ playerBottomUiMiddleContainer_ = (PlayerBottomUiMiddleContainer_)unit.graphics_.ui.bottomUI.GetComponent<PlayerBottomUi_>().playerBottomUiMiddleContainer_;
            playerBottomUiMiddleContainer_.healthBarContainer.slider.value = unitStats.hp;
        }
        else if(unit.GetType() == typeof(Npc_))
        {
            NpcBottomUiLeftContainer_ npcBottomUiLeftContainer_ = (NpcBottomUiLeftContainer_)unit.graphics_.ui.bottomUI.GetComponent<NpcBottomUi_>().leftContainer_;
            npcBottomUiLeftContainer_.barContainers.hpBar.value = unitStats.hp;
        }
    }

    public void HitAttackTarget()
    {
        if(attackTarget != null)
        {
            Hit(attackTarget.GetComponent<Unit_>());
        }
    }
    #endregion

    #region Animators Behaviour
    public List<Animator> GetEquippedItemAnimators()
    {
        return ((Unit_)owner).inventory_.GetEquippedItems().Select(t => t.graphics_.inGame_.animator).ToList();
    }

    public Animator GetUnitAnimator()
    {
        return ((UnitIngame_)((Unit_)owner).graphics_.inGame_).animator;
    }

    private void SetSpeedParameterToAnimators(MovementType movementType)
    {
        float animatorParameterSpeed;
        switch (movementType)
        {
            case MovementType.Walk:
                animatorParameterSpeed = 0.5f;
                break;
            case MovementType.Run:
                animatorParameterSpeed = 1;
                break;
            default:
                animatorParameterSpeed = 0;
                break;
        }
        unitAnimator.SetFloat("speed", animatorParameterSpeed);
        GetEquippedItemAnimators().ForEach(t => t.SetFloat("speed", animatorParameterSpeed));
    }

    public void SyncAnimators()
    {
        GetEquippedItemAnimators().ForEach(t => t.Play(0, -1, 0));
        unitAnimator.Play(0, -1, 0);
    }
    #endregion
}

public enum MovementType
{
    Walk,
    Run
}
