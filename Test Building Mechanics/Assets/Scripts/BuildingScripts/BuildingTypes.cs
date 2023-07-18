using System;
using UnityEngine;

public class BuildingTypes : MonoBehaviour
{
    [HideInInspector] public int prefabInt = -1;
    [HideInInspector] public Guid prefabGuid = Guid.NewGuid();
    public BuildingTypeDropdownEnum buildingTypeDropdown = new BuildingTypeDropdownEnum();
}

public enum BuildingTypeDropdownEnum
{
    Shelf,
    ShelfItem,
    Misc
};