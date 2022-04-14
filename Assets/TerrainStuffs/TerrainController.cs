using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    [SerializeField] private float TerrainSpeed;

    private float TerrainBounds = 7500;

    [SerializeField] private float LowerBias;

    [SerializeField] private GameObject[] TerrainPrefab;

    [SerializeField] private GameObject[] CurrentTerrains;

    private void Update()
    {
        MoveCurrentTerrains();
        DetermineIfTerrainChangeNeeded();


    }


    void MoveCurrentTerrains()
    {
        foreach(var terrain in CurrentTerrains)
        {
            terrain.transform.Translate(Vector3.forward * TerrainSpeed * Time.deltaTime);
        }
    }

    void DetermineIfTerrainChangeNeeded()
    {
        if(CurrentTerrains[2].transform.position.z < -TerrainBounds)
        {
            MakeNewTerrain();
        }
    }

    void MakeNewTerrain()
    {
        var NewTerrain = Instantiate(TerrainPrefab[DetermineWhichTerrainToUse()], transform);
        NewTerrain.transform.position = new Vector3(transform.position.x, transform.position.y - LowerBias, transform.position.z + TerrainBounds - 10);
        DeleteOldTerrain();
        ShiftTerrains(NewTerrain);
    }

    int DetermineWhichTerrainToUse()
    {
        return 0;   //THIS WILL BE DIF IN THE FUTURE
    }

    void DeleteOldTerrain()
    {
        Destroy(CurrentTerrains[2]);
    }

    void ShiftTerrains(GameObject NewTerrain)
    {
        CurrentTerrains[2] = CurrentTerrains[1];
        CurrentTerrains[1] = CurrentTerrains[0];
        CurrentTerrains[0] = NewTerrain;
    }

}
