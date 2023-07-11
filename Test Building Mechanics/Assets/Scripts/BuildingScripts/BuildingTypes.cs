using UnityEngine;

public class BuildingTypes : MonoBehaviour
{
    public BuildingTypeDropdownEnum buildingTypeDropdown = new BuildingTypeDropdownEnum();
}

public enum BuildingTypeDropdownEnum
{
    Shelf,
    Misc
};