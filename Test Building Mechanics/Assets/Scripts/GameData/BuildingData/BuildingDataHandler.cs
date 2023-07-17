using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDataHandler : MonoBehaviour
{
    /*
     * TO DO:
     * Get buildingMetaData working: 
     * GameObjects in shelves to work
     */

    public DataManager dataManagerScript;
    public RaycastBuilding raycastBuildingScript;

    [HideInInspector] public List<BuildingData> buildingDataList = new List<BuildingData>();
    public Dictionary<Guid, BuildingData> buildingDataDictionary = new Dictionary<Guid, BuildingData>();

    public void AddBuilding(Guid buildingGuid, int prefabInt, Vector3 buildingPosition, Quaternion buildingRotation)
    {
        BuildingData buildingData = new BuildingData
        {
            buildingGuid = buildingGuid,

            prefabInt = prefabInt,
            buildingPosition = buildingPosition,
            buildingRotation = buildingRotation,

            buildingMetaData = new BuildingMetaData {
                prefabInt = -1,
                positionInGameObject = -1
            }
        };

        buildingDataList.Add(buildingData);
        buildingDataDictionary[buildingData.buildingGuid] = buildingData;
    }

    public void RemoveBuilding(Guid buildingGuid)
    {
        int removeIndex = -1;
        for (int i = 0; i < buildingDataList.Count; i++)
        {
            if (buildingDataList[i].buildingGuid == buildingGuid)
            {
                removeIndex = i;
                break;
            }
        }

        if (removeIndex != -1)
        {
            buildingDataList.RemoveAt(removeIndex);
        }
    }

    public void LoadBuildings()
    {
        dataManagerScript.ReadData();

        foreach (BuildingData buildingData in buildingDataList)
        {
            GameObject blueprint = Instantiate(raycastBuildingScript.prefabBlueprints[buildingData.prefabInt]);

            CanBuild canBuildScript = blueprint.GetComponent<CanBuild>();

            blueprint.GetComponent<MeshRenderer>().material = canBuildScript.solidMaterial;
            blueprint.layer = 0;
            canBuildScript.isSolidObject = true;
            blueprint.GetComponent<MeshCollider>().isTrigger = false;
            blueprint.tag = "SolidObject";

            Vector3 scale = blueprint.transform.localScale;
            scale -= new Vector3(raycastBuildingScript.epsilon, raycastBuildingScript.epsilon, raycastBuildingScript.epsilon);

            blueprint.transform.position = buildingData.buildingPosition;
            blueprint.transform.rotation = buildingData.buildingRotation;

            scale = blueprint.transform.localScale;
            scale += new Vector3(raycastBuildingScript.epsilon, raycastBuildingScript.epsilon, raycastBuildingScript.epsilon);
        }
    }
}
