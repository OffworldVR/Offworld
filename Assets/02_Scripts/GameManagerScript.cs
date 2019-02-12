using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ShipPrefab;             // Reference to the prefab the AI will control.
    public ShipManager[] Ships;               // A collection of managers for enabling and disabling different aspects of the tanks.

    private int WaypointNumber;                  // Which waypoint the ship is currently on.

    private void Start()
    {
        SpawnAllShips();
        SetCameraTargets();
    }

    private void SpawnAllShips()
    {
        // For all the tanks...
        for (int i = 0; i < Ships.Length; i++)
        {
            // ... create them, set their player number and references needed for control.
            Ships[i].m_Instance =
                Instantiate(ShipPrefab, Ships[i].m_SpawnPoint.position, Ships[i].m_SpawnPoint.rotation) as GameObject;
            Ships[i].m_PlayerNumber = i + 1;
            Ships[i].Setup();
        }
    }

    private void SetCameraTargets()
    {
        // Create a collection of transforms the same size as the number of tanks.
        Transform[] targets = new Transform[Ships.Length];

        // For each of these transforms...
        for (int i = 0; i < targets.Length; i++)
        {
            // ... set it to the appropriate tank transform.
            targets[i] = Ships[i].m_Instance.transform;
        }
    }

    // This function is used to turn all the ships back on and reset their positions and properties.
    private void ResetAllShips()
    {
        for (int i = 0; i < Ships.Length; i++)
        {
            Ships[i].Reset();
        }
    }

    private void EnableShipControl()
    {
        for (int i = 0; i < Ships.Length; i++)
        {
            Ships[i].EnableControl();
        }
    }

    private void DisableShipControl()
    {
        for (int i = 0; i < Ships.Length; i++)
        {
            Ships[i].DisableControl();
        }
    }
}
