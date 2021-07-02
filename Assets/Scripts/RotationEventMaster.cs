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

    [SerializeField] PolygonCollider2D CompoundFieldOrange;
    [SerializeField] BoxCollider2D FieldAOrange;
    [SerializeField] BoxCollider2D FieldBOrange;
    [SerializeField] PolygonCollider2D CompoundFieldLightBlue;
    [SerializeField] BoxCollider2D FieldALightBlue;
    [SerializeField] BoxCollider2D FieldBLightBlue;

    PlayerController[] Players;


    private void Start()
    {
        orientationMaster = OrientationMaster.Instance;

        orientationMaster.OnTurn += UpdateTeleporters;
        orientationMaster.OnTurn += UpdateColliders;
        orientationMaster.OnTurn += UpdateFields;

        TeleporterA.SetActive(false);
        TeleporterB.SetActive(false);

        Players = FindObjectsOfType<PlayerController>();

    }

    void UpdateTeleporters()
    {
        if (orientationMaster.LevelOrientation == OrientationMaster.LevelOrientations.half)
        {
            TeleporterA.SetActive(false);
            TeleporterB.SetActive(false);
        }
        else if(orientationMaster.LevelOrientation == OrientationMaster.LevelOrientations.normal)
        {
            TeleporterA.SetActive(true);
            TeleporterB.SetActive(true);
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

    void UpdateFields()
    {
        foreach (PlayerController player in Players)
            player.Interacting = true;

        if (orientationMaster.LevelOrientation == OrientationMaster.LevelOrientations.half)
        {
            CompoundFieldLightBlue.enabled = false;
            CompoundFieldOrange.enabled = false;

            FieldAOrange.enabled = true;
            FieldBOrange.enabled = true;
            FieldALightBlue.enabled = true;
            FieldBLightBlue.enabled = true;
        }
        else if (orientationMaster.LevelOrientation == OrientationMaster.LevelOrientations.normal)
        {
            CompoundFieldLightBlue.enabled = true;
            CompoundFieldOrange.enabled = true;

            FieldAOrange.enabled = false;
            FieldBOrange.enabled = false;
            FieldALightBlue.enabled = false;
            FieldBLightBlue.enabled = false;
        }

        foreach (PlayerController player in Players)
            player.Interacting = false;
    }

}
