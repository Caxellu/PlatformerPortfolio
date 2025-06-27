using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlatformerModel
{
    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    public PlayerController player;

    public Transform spawnPoint;



    public float jumpDeceleration = 0.5f;

}