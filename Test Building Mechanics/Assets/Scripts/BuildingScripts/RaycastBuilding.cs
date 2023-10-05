using System.Collections.Generic;
using UnityEngine;

public class RaycastBuilding : MonoBehaviour
{
    public CurrentKeybinds currentKeybindsScript;
    public GameCanvasHandler gameCanvasHandlerScript;
    public BuildingDataHandler buildingDataHandlerScript;
    private CanBuild canBuildScript;

    private GameObject blueprint;

    public List<GameObject> prefabBlueprints;

    private MeshRenderer blueprintMeshRenderer;

    [HideInInspector] public bool isGridSnap = false;
    [HideInInspector] public bool prevIsGridSnap = false;
    [HideInInspector] public bool isBlueprintFollowingCursor = false;

    public int gridRotationDegreeAmount = 45;
    [HideInInspector] public int currentPrefabInt = -1;
    private int shelfPrefabInt = 3;

    public float rayDistance = 3.0f;
    public float gridSize = 1f;
    public float blueprintRotationSpeed = 100f;
    [HideInInspector] public float epsilon = 0.005f;

    private void Update()
    {
        if (blueprint != null)
        {
            canBuildScript = blueprint.GetComponent<CanBuild>();
            blueprintMeshRenderer = blueprint.GetComponent<MeshRenderer>();

            if (isBlueprintFollowingCursor)
            {
                BlueprintToCursor();
                if ((!isGridSnap) && (Input.GetKey(currentKeybindsScript.blueprintLeftKey)))
                {
                    RotateBlueprint(0);
                }
                else if ((!isGridSnap) && (Input.GetKey(currentKeybindsScript.blueprintRightKey)))
                {
                    RotateBlueprint(1);
                }
                else if ((isGridSnap) && (Input.GetKeyDown(currentKeybindsScript.blueprintLeftKey)))
                {
                    RotateBlueprint(0);
                }
                else if ((isGridSnap) && (Input.GetKeyDown(currentKeybindsScript.blueprintRightKey)))
                {
                    RotateBlueprint(1);
                }
            }
        }
    }

    public void SelectBlueprint(int prefabNumber)
    {
        currentPrefabInt = prefabNumber;
        ShelfGridSnap();
        if (!isBlueprintFollowingCursor)
        {
            isBlueprintFollowingCursor = true;
            blueprint = Instantiate(prefabBlueprints[currentPrefabInt]);
            blueprint.GetComponent<BuildingTypes>().prefabInt = currentPrefabInt;
            BlueprintToCursor();
            Vector3 scale = blueprint.transform.localScale;
            scale -= new Vector3(epsilon, epsilon, epsilon);
            blueprint.transform.localScale = scale;
        }
    }

    public void BlueprintToCursor()
    {
        bool hitSuccessful = RaycastHitTest(false, "", out RaycastHit hit);

        if (hitSuccessful)
        {
            Bounds b = prefabBlueprints[currentPrefabInt].GetComponent<MeshFilter>().sharedMesh.bounds;
            Vector3 lowerCenter = new Vector3(b.center.x * blueprint.transform.localScale.x, (-b.extents.y * blueprint.transform.localScale.y) - epsilon, b.center.z * blueprint.transform.localScale.z);

            Vector3 newPos = hit.point - lowerCenter;

            if (currentPrefabInt == shelfPrefabInt)
            {
                float rotationY = blueprint.transform.eulerAngles.y;
                float offset = 0.5f;

                Vector3 wallForward = Quaternion.Euler(0, rotationY, 0) * Vector3.forward;
                Vector3 wallRight = Quaternion.Euler(0, rotationY, 0) * Vector3.right;

                newPos += wallForward * offset;

                if ((Mathf.Abs(Vector3.Dot(wallForward, Vector3.back)) > 0.9f) && (Mathf.Abs(Vector3.Dot(wallRight, Vector3.right)) < 0.1f))
                {
                    newPos -= wallForward * offset;
                }
            }

            if (isGridSnap)
            {
                newPos = new Vector3(Mathf.Round(newPos.x / gridSize) * gridSize, Mathf.Round(newPos.y / gridSize) * gridSize, Mathf.Round(newPos.z / gridSize) * gridSize);
            }

            blueprint.transform.position = newPos;
        }
    }

    public void DeselectBlueprint()
    {
        if (isBlueprintFollowingCursor)
        {
            isBlueprintFollowingCursor = false;
            Destroy(blueprint);
        }
    }

    public void ToggleGridSnap()
    {
        if (currentPrefabInt != shelfPrefabInt)
        {
            isGridSnap = !isGridSnap;
            prevIsGridSnap = isGridSnap;
            if (blueprint != null)
            {
                blueprint.transform.eulerAngles = Vector3.zero;
            }
        }
    }

    public void ShelfGridSnap()
    {
        if (currentPrefabInt == shelfPrefabInt)
        {
            prevIsGridSnap = isGridSnap;
            isGridSnap = true;
        }
        else
        {
            isGridSnap = prevIsGridSnap;
        }
    }

    public void RotateBlueprint(int direction)
    {
        //left
        if (direction == 0)
        {
            if ((isGridSnap) || (currentPrefabInt == shelfPrefabInt))
            {
                blueprint.transform.eulerAngles += new Vector3(0, gridRotationDegreeAmount, 0);
            }
            else
            {
                blueprint.transform.Rotate(blueprintRotationSpeed * Time.deltaTime * Vector3.up);
            }
        }
        //right
        else if (direction == 1)
        {
            if ((isGridSnap) || (currentPrefabInt == shelfPrefabInt))
            {
                blueprint.transform.eulerAngles += new Vector3(0, -gridRotationDegreeAmount, 0);
            }
            else
            {
                blueprint.transform.Rotate(blueprintRotationSpeed * Time.deltaTime * Vector3.down);
            }
        }
    }

    public void BuildBlueprint()
    {
        if (isBlueprintFollowingCursor && canBuildScript.canBuildBlueprint)
        {
            canBuildScript = blueprint.GetComponent<CanBuild>();
            BuildingTypes buildingTypesScript = blueprint.GetComponent<BuildingTypes>();

            if (canBuildScript.canBuildBlueprint)
            {
                blueprintMeshRenderer = blueprint.GetComponent<MeshRenderer>();

                blueprintMeshRenderer.material = canBuildScript.solidMaterial;
                Vector3 scale = blueprint.transform.localScale;
                scale += new Vector3(epsilon, epsilon, epsilon);

                blueprint.layer = 0;
                isBlueprintFollowingCursor = false;
                blueprint.transform.localScale = scale;
                canBuildScript.isSolidObject = true;
                blueprint.GetComponent<MeshCollider>().isTrigger = false;
                blueprint.tag = "SolidObject";

                buildingDataHandlerScript.AddBuilding(buildingTypesScript.prefabGuid, buildingTypesScript.prefabInt, blueprint.transform.position, blueprint.transform.rotation);

                SelectBlueprint(buildingTypesScript.prefabInt);
            }
        }
    }

    public void MoveObject()
    {
        bool hitSuccessful = RaycastHitTest(true, "SolidObject", out RaycastHit hit);
        if (hitSuccessful)
        {
            blueprint = hit.transform.gameObject;
            currentPrefabInt = blueprint.GetComponent<BuildingTypes>().prefabInt;
            blueprintMeshRenderer = blueprint.GetComponent<MeshRenderer>();
            canBuildScript = blueprint.GetComponent<CanBuild>();

            Vector3 scale = blueprint.transform.localScale;
            scale -= new Vector3(epsilon, epsilon, epsilon);
            blueprint.transform.localScale = scale;

            blueprint.layer = 2;
            isBlueprintFollowingCursor = true;
            blueprintMeshRenderer.material = canBuildScript.blueprintMaterial;
            canBuildScript.isSolidObject = false;
            blueprint.GetComponent<MeshCollider>().isTrigger = true;
            blueprint.tag = "Blueprint";
        }
    }

    public void DestroyObject()
    {
        bool hitSuccessful = RaycastHitTest(true, "SolidObject", out RaycastHit hit);
        if (hitSuccessful)
        {
            buildingDataHandlerScript.RemoveBuilding(hit.transform.GetComponent<BuildingTypes>().prefabGuid);

            Destroy(hit.transform.gameObject);
        }
    }

    public bool RaycastHitTest(bool isTagNeeded, string tag, out RaycastHit hit)
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        bool hitSuccessful;
        if (isTagNeeded)
        {
            hitSuccessful = Physics.Raycast(ray, out hit, rayDistance) && hit.collider != null && hit.transform.CompareTag(tag);
        }
        else
        {
            hitSuccessful = Physics.Raycast(ray, out hit, rayDistance) && hit.collider != null;
        }
        return hitSuccessful;
    }
}
