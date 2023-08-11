using System;
using UnityEngine;

public class BuildingTypes : MonoBehaviour
{
    [HideInInspector] public int prefabInt = -1;
    [HideInInspector] public Guid prefabGuid = Guid.NewGuid();
}