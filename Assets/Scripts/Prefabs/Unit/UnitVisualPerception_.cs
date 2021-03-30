using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitVisualPerception_ : Component_
{
    public float visualAngle;
    public float visualDistance;
    public float visualRefreshTime;
    public List<Base_> baseObjectsInVisualField;

    private void Awake()
    {
        owner = GetComponentInParent<Unit_>();
    }

    private void Start()
    {
        StartCoroutine(LookEvery(visualRefreshTime));
    }

    [Button]
    public void Look()
    {
        baseObjectsInVisualField.Clear();

        List<Collider2D> colliders = Physics2D.OverlapCircleAll(transform.position, visualDistance).ToList();
        foreach (Collider2D collider in colliders)
        {
            if(IsBaseObject(collider.gameObject, out Base_ baseObjectInVisualField))
            {
                if (IsInVisualField(baseObjectInVisualField))
                {
                    baseObjectsInVisualField.Add(baseObjectInVisualField);
                }
            }
        }
    }

    public IEnumerator LookEvery(float seconds)
    {
        while (true)
        {
            Look();
            yield return new WaitForSeconds(seconds);
        }
    }

    public bool IsInVisualField(Base_ baseObject)
    {
        bool isInVisualField = false;
        if (IsInVisualAngle(baseObject))
        {
            if (IsInVisualDistance(baseObject))
            {
                isInVisualField = true;
            }
        }
        return isInVisualField;
    }

    public bool IsInVisualAngle(Base_ baseObject)
    {
        bool isInVisualAngle = false;
        float baseObjectAngle = Vector3.Angle(transform.right, baseObject.transform.position - owner.transform.position);
        if (baseObjectAngle <= visualAngle/2)
        {
            isInVisualAngle = true;
        }
        return isInVisualAngle;
    }

    public bool IsInVisualDistance(Base_ baseObject)
    {
        bool isInVisualDistance = false;
        float baseObjectDistance = Vector3.Distance(owner.transform.position, baseObject.transform.position);
        if(baseObjectDistance <= visualDistance)
        {
            isInVisualDistance = true;
        }
        return isInVisualDistance;
    }

    public bool IsBaseObject(GameObject gameObject_, out Base_ baseObject)
    {
        bool isBaseObject = false;
        baseObject = null;
        if(gameObject_.GetComponentInParent<Base_>() != null)
        {
            baseObject = gameObject_.GetComponentInParent<Base_>();
            isBaseObject = true;
        }
        return isBaseObject;
    }
}
