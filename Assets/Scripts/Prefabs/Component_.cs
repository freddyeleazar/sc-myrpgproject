using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Component_ : MonoBehaviour
{
    [HideInInspector]
    public Owner_ owner;

    public virtual void Build(Owner_ owner)
    {
        this.owner = owner;
        List<FieldInfo> fields = GetType().GetFields().ToList().FindAll(t => t.FieldType.InheritsFrom(typeof(Component_)));
        foreach (FieldInfo field in fields)
        {
            Component_ component = (Component_)GetComponentInChildren(field.FieldType);
            if(component != null)
            {
                component.Build(owner);
                field.SetValue(this, component);
            }
        }
    }

    public virtual void Empty()
    {
        owner = null;
        List<FieldInfo> fields = GetType().GetFields().ToList().FindAll(t => t.FieldType.InheritsFrom(typeof(Component_)));
        foreach (FieldInfo field in fields)
        {
            field.SetValue(this, null);
        }
    }
}
