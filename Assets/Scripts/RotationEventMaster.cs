using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationEventMaster : MonoBehaviour
{
    OrientationMaster orientationMaster;

    [SerializeField] Teleportation TeleporterA;
    [SerializeField]Teleportation TeleporterB;

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
            //TODO
        }
        else if (orientationMaster.LevelOrientation == OrientationMaster.LevelOrientations.normal)
        {
            //TODO
        }
    }
}
