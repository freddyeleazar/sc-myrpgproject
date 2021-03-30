using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController_ : MonoBehaviour
{
    public WorldSettings_ settings;
    public WorldGraphics_ graphics;
    public WorldMechanics_ mechanics;

    public static float DistanceBetween(Vector2 p1, Vector2 p2)
    {
        return (p1 - p2).magnitude;
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.z;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        return mousePosition;
    }
}
