using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationEventMaster : MonoBehaviour
{
    OrientationMaster orientationMaster;

    [SerializeField] Teleportation TeleporterA;
    [SerializeField] Teleportation TeleporterB;

    [SerializeField] EdgeCollider2D ColliderA;
    [SerializeField] EdgeCollider2D ColliderB;
    [SerializeField] EdgeCollider2D ColliderC;
    [SerializeField] EdgeCollider2D ColliderD;

    private void Start()
    {
        orientationMaster = OrientationMaster.Instance;

        orientationMaster.OnTurn += UpdateTeleporters;
        orientationMaster.OnTurn += UpdateColliders;

        TeleporterA.Active = false;
        TeleporterB.Active = false;
    }

    void UpdateTeleporters()
    {
        if (orientationMaster.LevelOrientation == OrientationMaster.LevelOrientations.half)
        {
            TeleporterA.Active = false;
            TeleporterB.Active = false;
        }
        else if(orientationMaster.LevelOrientation == OrientationMaster.LevelOrientations.normal)
        {
            TeleporterA.Active = true;
            TeleporterB.Active = true;
        }
    }

    void UpdateColliders()
    {
        if (orientationMaster.LevelOrientation == OrientationMaster.LevelOrientations.half)
        {
            ColliderA.enabled = true;
            ColliderB.enabled = true;
            ColliderC.enabled = true;
            ColliderD.enabled = true;
        }
        else if (orientationMaster.LevelOrientation == OrientationMaster.LevelOrientations.normal)
        {
            ColliderA.enabled = false;
            ColliderB.enabled = false;
            ColliderC.enabled = false;
            ColliderD.enabled = false;
        }
    }
}
