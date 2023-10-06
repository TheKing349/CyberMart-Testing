using System;
using UnityEngine;

[Serializable]
public class BuildingData
{
    public Guid buildingGuid;

    public int prefabInt;
    public Vector3 buildingPosition;
    public Quaternion buildingRotation;

    //public BuildingMetaData buildingMetaData;
}

public class BuildingMetaData
{
    public int prefabInt;
    public int positionInGameObject;
}