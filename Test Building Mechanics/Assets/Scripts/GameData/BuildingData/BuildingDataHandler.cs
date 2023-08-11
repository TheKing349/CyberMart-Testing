using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingDataHandler : MonoBehaviour
{
    public GameDataManager gameDataManagerScript;
    public RaycastBuilding raycastBuildingScript;

    [HideInInspector] public List<BuildingData> buildingDataList = new List<BuildingData>();
    public Dictionary<Guid, BuildingData> buildingDataDictionary = new Dictionary<Guid, BuildingData>();

    public void AddBuilding(Guid buildingGuid, int prefabInt, Vector3 buildingPosition, Quaternion buildingRotation)
    {
        foreach(BuildingData previousBuildingData in buildingDataList.ToList())
        {
            if (buildingGuid == previousBuildingData.buildingGuid)
            {
                buildingDataList.Remove(previousBuildingData);
                buildingDataDictionary[buildingGuid] = null;
                break;
            }
        }

        BuildingData buildingData = new BuildingData
        {
            buildingGuid = buildingGuid,

            prefabInt = prefabInt,
            buildingPosition = buildingPosition,
            buildingRotation = buildingRotation,
        };

        buildingDataList.Add(buildingData);
        buildingDataDictionary[buildingGuid] = buildingData;
    }

    public void RemoveBuilding(Guid buildingGuid)
    {
        foreach (BuildingData previousBuildingData in buildingDataList.ToList())
        {
            if (buildingGuid == previousBuildingData.buildingGuid)
            {
                buildingDataList.Remove(previousBuildingData);
            }
        }
    }

    public void LoadBuildings()
    {
        gameDataManagerScript.ReadData();

        foreach (BuildingData buildingData in buildingDataList.ToList())
        {
            GameObject blueprint = Instantiate(raycastBuildingScript.prefabBlueprints[buildingData.prefabInt]);

            CanBuild canBuildScript = blueprint.GetComponent<CanBuild>();
            BuildingTypes buildingTypesScript = blueprint.GetComponent<BuildingTypes>();

            buildingTypesScript.prefabInt = buildingData.prefabInt;
            buildingTypesScript.prefabGuid = buildingData.buildingGuid;

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
