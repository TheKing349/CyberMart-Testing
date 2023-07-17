using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BuildingMenuSearch : MonoBehaviour
{
    public RaycastBuilding raycastBuildingScript;
    public BuildingButtons buildingButtonsScript;

    public TMP_InputField searchBar;

    [HideInInspector] public bool isTyping;

    public void OnSelectSearchBar()
    {
        isTyping = true;
    }

    public void SearchTerm()
    {
        buildingButtonsScript.DestroyPreviousBuildingButtons();
        if (searchBar.text.Length > 0)
        {
            searchBar.text = UppercaseFirst(searchBar.text);
            List<GameObject> sortedPrefabsList = raycastBuildingScript.prefabBlueprints.OrderBy(o => o.name).ToList();

            for (int i = 0; i < sortedPrefabsList.Count; i++)
            {
                GameObject prefab = sortedPrefabsList[i];

                int closureIndex = i;
                prefab.name = prefab.name.Replace("Blueprint", "");
                if (prefab.name.Contains(searchBar.text))
                {
                    buildingButtonsScript.InstantiatePrefabButton(prefab.name, closureIndex);
                }
            }
        }
        else
        {
            buildingButtonsScript.SortBuildingButtons(buildingButtonsScript.previousBuildingType);
        }
    }

    public void OnDeselectSearchBar()
    {
        isTyping = false;
    }

    string UppercaseFirst(string s)
    {
        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }
}