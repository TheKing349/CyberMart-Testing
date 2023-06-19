using Unity.Loading;
using UnityEditor.Rendering;
using UnityEngine;

public class RaycastBuilding : MonoBehaviour
{
    public Keybinds keybinds;
    private CanBuild canBuild;

    private GameObject blueprint;
    public GameObject[] prefabBlueprints;

    private MeshRenderer blueprintMeshRenderer;

    public Material solidMaterial;
    public Material blueprintMaterial;
    public Material blueprintErrorMaterial;

    [HideInInspector] public bool isBlueprintFollowingCursor = false;
    [HideInInspector] public bool isGridSnap = false;
    private bool done = false;

    private int currentPrefabInt = -1;
    public int gridRotationDegreeAmount = 45;
    public float rayDistance = 3.0f;
    public float gridSize = 1f;
    public float blueprintRotationSpeed = 100f;

    void Update()
    {
        if (blueprint != null)
        {
            canBuild = blueprint.GetComponent<CanBuild>();
            blueprintMeshRenderer = blueprint.GetComponent<MeshRenderer>();
            if (isBlueprintFollowingCursor)
            {
                BlueprintToCursor();

                if (!isGridSnap)
                {
                    if (Input.GetKey(keybinds.rotateLeftKey))
                    {
                        RotateBlueprint(0);
                    }
                    else if (Input.GetKey(keybinds.rotateRightKey))
                    {
                        RotateBlueprint(1);
                    }
                }
                else
                {
                    if (Input.GetKeyDown(keybinds.rotateLeftKey))
                    {
                        RotateBlueprint(0);
                    }
                    else if (Input.GetKeyDown(keybinds.rotateRightKey))
                    {
                        RotateBlueprint(1);
                    }
                }
            }
        }
    }

    public void RotateBlueprint(int direction)
    {
        //left
        if ((direction == 0) && (!isGridSnap))
        {
            blueprint.transform.Rotate(blueprintRotationSpeed * Time.deltaTime * Vector3.up);
        }
        else if (direction == 0)
        {
            blueprint.transform.eulerAngles += new Vector3(0, gridRotationDegreeAmount, 0);
        }
        //right
        else if ((direction == 1) && (!isGridSnap))
        {
            blueprint.transform.Rotate(blueprintRotationSpeed * Time.deltaTime * Vector3.down);
        }
        else if (direction == 1)
        {
            blueprint.transform.eulerAngles += new Vector3(0, -gridRotationDegreeAmount, 0);
        }
    }

    public void SelectBlueprint(int prefabNumber)
    {
        currentPrefabInt = prefabNumber;
        if (!isBlueprintFollowingCursor)
        {
            done = false;
            isBlueprintFollowingCursor = true;
            blueprint = Instantiate(prefabBlueprints[currentPrefabInt]);
        }
    }

    public void DeselectBlueprint()
    {
        if ((isBlueprintFollowingCursor) && (canBuild.canBuildBlueprint))
        {
            isBlueprintFollowingCursor = false;
            Destroy(blueprint);
        }
    }

    public void BuildBlueprint()
    {
        if ((isBlueprintFollowingCursor) && (canBuild.canBuildBlueprint))
        {
            blueprint.layer = 0;
            isBlueprintFollowingCursor = false;
            canBuild = blueprint.GetComponent<CanBuild>();
            blueprintMeshRenderer = blueprint.GetComponent<MeshRenderer>();
            if (canBuild.canBuildBlueprint)
            {
                blueprintMeshRenderer.material = solidMaterial;
                blueprint.GetComponent<MeshCollider>().isTrigger = false;
                blueprint.tag = "SolidObject";
                canBuild.isSolidObject = true;
            }
        }
    }

    public void DestroyObject()
    {
        RaycastHitTest("SolidObject", out bool hitSuccessful, out RaycastHit hit);
        if (hitSuccessful) {
            Destroy(hit.transform.gameObject);
        }
    }

    public void MoveObject()
    {
        RaycastHitTest("SolidObject", out bool hitSuccessful, out RaycastHit hit);
        if (hitSuccessful)
        {
            blueprint.layer = 2;
            BlueprintToCursor();
            isBlueprintFollowingCursor = true;
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

    public void BlueprintToCursor()
    {
        RaycastHitTest("Ground", out bool hitSuccessful, out RaycastHit hit);

        if (hitSuccessful)
        {
            blueprintMeshRenderer.material = blueprintMaterial;
            blueprint.GetComponent<MeshCollider>().isTrigger = true;

            Bounds b = prefabBlueprints[currentPrefabInt].GetComponent<MeshFilter>().sharedMesh.bounds;
            Vector3 lowerCenter = new Vector3(b.center.x * blueprint.transform.localScale.x, -b.extents.y * blueprint.transform.localScale.y - 0.01f, b.center.z * blueprint.transform.localScale.z);
            Vector3 newPos = hit.point - lowerCenter;

            if (!isGridSnap)
            {
                blueprint.transform.position = newPos;
            }
            else
            {
                newPos = new Vector3(Mathf.Round(newPos.x / gridSize) * gridSize, newPos.y, Mathf.Round(newPos.z / gridSize) * gridSize);

                float rotationY = blueprint.transform.eulerAngles.y;
                if (currentPrefabInt == 2)
                {
                    float offset = blueprint.transform.localScale.x / 2;
                    if ((Mathf.Approximately(rotationY, 0f)) || (Mathf.Approximately(rotationY, 180f)))
                    {
                        newPos.x += (rotationY > 90f && rotationY < 270f) ? -offset : offset;
                    }
                    else
                    {
                        newPos.z += (rotationY > 180f) ? offset : -offset;
                    }
                }

                blueprint.transform.position = newPos;

                if (!done)
                {
                    blueprint.transform.localScale = new Vector3(blueprint.transform.localScale.x - 0.00001f, blueprint.transform.localScale.y - 0.00001f, blueprint.transform.localScale.z - 0.00001f);
                    done = true;
                }
            }
        }
    }

    public void RaycastHitTest(string tag, out bool hitSuccessful, out RaycastHit hit)
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        hitSuccessful = ((Physics.Raycast(ray, out hit, rayDistance)) && (hit.collider != null) && (hit.transform.CompareTag(tag)));
    }
}