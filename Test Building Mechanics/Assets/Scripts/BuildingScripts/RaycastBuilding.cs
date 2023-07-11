using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RaycastBuilding : MonoBehaviour
{
    public Keybinds keybindsScript;
    public CanvasHandler canvasHandlerScript;
    private CanBuild canBuildScript;

    private GameObject blueprint;

    [HideInInspector] public List<GameObject> prefabBlueprints;

    private MeshRenderer blueprintMeshRenderer;

    public Material solidMaterial;
    public Material blueprintMaterial;
    public Material blueprintErrorMaterial;

    [HideInInspector] public bool isGridSnap = false;
    [HideInInspector] public bool isBlueprintFollowingCursor = false;

    public int gridRotationDegreeAmount = 45;
    private int currentPrefabInt = -1;

    public float rayDistance = 3.0f;
    public float gridSize = 1f;
    public float blueprintRotationSpeed = 100f;
    private const float epsilon = 0.005f;

    private void Awake()
    {
        Object[] prefabs;
        prefabs = Resources.LoadAll("Prefabs/Buildings", typeof(GameObject)).Cast<GameObject>().ToArray();

        int index = 0;
        foreach (var prefab in prefabs)
        {
            prefabBlueprints.Add(prefab.GameObject());
            index++;
        }
    }

    private void Update()
    {
        if (blueprint != null)
        {
            canBuildScript = blueprint.GetComponent<CanBuild>();
            blueprintMeshRenderer = blueprint.GetComponent<MeshRenderer>();

            if (isBlueprintFollowingCursor)
            {
                BlueprintToCursor();
                if ((!isGridSnap) && (Input.GetKey(keybindsScript.rotateLeftKey)))
                {
                    RotateBlueprint(0);
                }
                else if ((!isGridSnap) && (Input.GetKey(keybindsScript.rotateRightKey)))
                {
                    RotateBlueprint(1);
                }
                else if ((isGridSnap) && (Input.GetKeyDown(keybindsScript.rotateLeftKey)))
                {
                    RotateBlueprint(0);
                }
                else if ((isGridSnap) && (Input.GetKeyDown(keybindsScript.rotateRightKey)))
                {
                    RotateBlueprint(1);
                }
            }
        }
    }

    public void SelectBlueprint(int prefabNumber)
    {
        canvasHandlerScript.ToggleBuildingCanvas();

        currentPrefabInt = prefabNumber;
        if (!isBlueprintFollowingCursor)
        {
            isBlueprintFollowingCursor = true;
            blueprint = Instantiate(prefabBlueprints[currentPrefabInt]);
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

            if ((int)blueprint.GetComponent<BuildingTypes>().buildingTypeDropdown == 0)
            {
                if ((int)blueprint.GetComponent<BuildingTypes>().buildingTypeDropdown == 0)
                {
                    float rotationY = blueprint.transform.eulerAngles.y;
                    float offset = 0.5f;

                    Vector3 wallForward = Quaternion.Euler(0, rotationY, 0) * Vector3.forward;
                    Vector3 wallRight = Quaternion.Euler(0, rotationY, 0) * Vector3.right;

                    newPos += wallForward * offset;

                    if (Mathf.Abs(Vector3.Dot(wallForward, Vector3.back)) > 0.9f && Mathf.Abs(Vector3.Dot(wallRight, Vector3.right)) < 0.1f)
                    {
                        newPos -= wallForward * offset;
                    }
                }
            }

            if (isGridSnap)
            {
                newPos = new Vector3(Mathf.Round(newPos.x / gridSize) * gridSize , Mathf.Round(newPos.y / gridSize) * gridSize, Mathf.Round(newPos.z / gridSize) * gridSize);
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
        isGridSnap = !isGridSnap;
        if (blueprint != null)
        {
            blueprint.transform.eulerAngles = Vector3.zero;
        }
    }

    public void RotateBlueprint(int direction)
    {
        if (direction == 0)
        {
            if (!isGridSnap)
            {
                blueprint.transform.Rotate(blueprintRotationSpeed * Time.deltaTime * Vector3.up);
            }
            else
            {
                blueprint.transform.eulerAngles += new Vector3(0, gridRotationDegreeAmount, 0);
            }
        }
        else if (direction == 1)
        {
            if (!isGridSnap)
            {
                blueprint.transform.Rotate(blueprintRotationSpeed * Time.deltaTime * Vector3.down);
            }
            else
            {
                blueprint.transform.eulerAngles += new Vector3(0, -gridRotationDegreeAmount, 0);
            }
        }
    }

    public void BuildBlueprint()
    {
        if (isBlueprintFollowingCursor && canBuildScript.canBuildBlueprint)
        {
            canBuildScript = blueprint.GetComponent<CanBuild>();
            if (canBuildScript.canBuildBlueprint)
            {
                blueprintMeshRenderer = blueprint.GetComponent<MeshRenderer>();

                blueprintMeshRenderer.material = solidMaterial;
                Vector3 scale = blueprint.transform.localScale;
                scale += new Vector3(epsilon, epsilon, epsilon);

                blueprint.layer = 0;
                isBlueprintFollowingCursor = false;
                blueprint.transform.localScale = scale;
                canBuildScript.isSolidObject = true;
                blueprint.GetComponent<MeshCollider>().isTrigger = false;
                blueprint.tag = "SolidObject";
            }
        }
    }

    public void MoveObject()
    {
        bool hitSuccessful = RaycastHitTest(true, "SolidObject", out RaycastHit hit);
        if (hitSuccessful)
        {
            blueprint = hit.transform.gameObject;
            blueprintMeshRenderer = hit.transform.gameObject.GetComponent<MeshRenderer>();

            Vector3 scale = blueprint.transform.localScale;
            scale -= new Vector3(epsilon, epsilon, epsilon);
            blueprint.transform.localScale = scale;

            blueprint.layer = 2;
            isBlueprintFollowingCursor = true;
            blueprintMeshRenderer.material = blueprintMaterial;
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
