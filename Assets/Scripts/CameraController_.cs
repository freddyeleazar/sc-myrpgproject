using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_ : MonoBehaviour
{
    public Transform player;
    public float tracingDelay;

    private void Update()
    {
        Vector3 playerPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.DOMove(playerPosition, tracingDelay);
    }
}
