using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Owner_ : MonoBehaviour
{
    private void Awake()
    {
        Build();
    }

    [Button]
    public virtual void Build()
    {
        List<FieldInfo> fields = GetType().GetFields().ToList().FindAll(t => t.FieldType.InheritsFrom(typeof(Component_)));
        foreach (FieldInfo field in fields)
        {
            Component_ component = (Component_)GetComponentInChildren(field.FieldType);
            if(component != null)
            {
                component.Build(this);
                field.SetValue(this, GetComponentInChildren(field.FieldType));
            }
            else
            {
                Debug.Log($"It doesn't exist a component for field {field.Name} ({field.FieldType.Name});");
            }
        }
    }

    [Button]
    public virtual void Empty()
    {
        List<FieldInfo> fields = GetType().GetFields().ToList().FindAll(t => t.FieldType.InheritsFrom(typeof(Component_)));
        foreach (FieldInfo field in fields)
        {
            Component_ component = (Component_)GetComponentInChildren(field.FieldType);
            component.Empty();
            field.SetValue(this, null);
        }
    }

}
