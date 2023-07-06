using UnityEngine;

public class RaycastBuilding : MonoBehaviour
{
    public Keybinds keybinds;
    public CanvasHandler canvasHandler;

    private GameObject blueprint;
    public GameObject[] prefabBlueprints;
    private CanBuild canBuild;
    private MeshRenderer blueprintMeshRenderer;

    public Material solidMaterial;
    public Material blueprintMaterial;
    public Material blueprintErrorMaterial;

    [HideInInspector] public bool isBlueprintFollowingCursor = false;
    [HideInInspector] public bool isGridSnap = false;

    private int currentPrefabInt = -1;
    private const float epsilon = 0.00001f;

    public int gridRotationDegreeAmount = 45;
    public float rayDistance = 3.0f;
    public float gridSize = 1f;
    public float blueprintRotationSpeed = 100f;

    private void Update()
    {
        if (blueprint != null)
        {
            canBuild = blueprint.GetComponent<CanBuild>();
            blueprintMeshRenderer = blueprint.GetComponent<MeshRenderer>();

            if (isBlueprintFollowingCursor)
            {
                BlueprintToCursor();
                if (!isGridSnap && (Input.GetKey(keybinds.rotateLeftKey) || Input.GetKey(keybinds.rotateRightKey)))
                {
                    RotateBlueprint(Input.GetKey(keybinds.rotateLeftKey) ? 0 : 1);
                }
                else if (isGridSnap && (Input.GetKeyDown(keybinds.rotateLeftKey) || Input.GetKeyDown(keybinds.rotateRightKey)))
                {
                    RotateBlueprint(Input.GetKeyDown(keybinds.rotateLeftKey) ? 0 : 1);
                }
            }
        }
    }

    public void SelectBlueprint(int prefabNumber)
    {
        canvasHandler.ToggleBuildingCanvas();

        currentPrefabInt = prefabNumber;
        if (!isBlueprintFollowingCursor)
        {
            isBlueprintFollowingCursor = true;
            blueprint = Instantiate(prefabBlueprints[currentPrefabInt]);
            blueprint.transform.position = transform.position;
            Vector3 scale = blueprint.transform.localScale;
            scale.x -= epsilon;
            scale.z -= epsilon;
            blueprint.transform.localScale = scale;
        }
    }

    public void BlueprintToCursor()
    {
        bool hitSuccessful = RaycastHitTest(false, "", out RaycastHit hit);

        if (hitSuccessful)
        {
            Bounds b = prefabBlueprints[currentPrefabInt].GetComponent<MeshFilter>().sharedMesh.bounds;
            Vector3 lowerCenter = new Vector3(b.center.x * blueprint.transform.localScale.x, -b.extents.y * blueprint.transform.localScale.y - epsilon, b.center.z * blueprint.transform.localScale.z);
            Vector3 newPos = hit.point - lowerCenter;

            if (isGridSnap)
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
            }
            blueprint.transform.position = newPos;
        }
    }

    public void DeselectBlueprint()
    {
        if (isBlueprintFollowingCursor && canBuild.canBuildBlueprint)
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
                blueprint.transform.Rotate(blueprintRotationSpeed * Time.deltaTime * Vector3.up);
            else
                blueprint.transform.eulerAngles += new Vector3(0, gridRotationDegreeAmount, 0);
        }
        else if (direction == 1)
        {
            if (!isGridSnap)
                blueprint.transform.Rotate(blueprintRotationSpeed * Time.deltaTime * Vector3.down);
            else
                blueprint.transform.eulerAngles += new Vector3(0, -gridRotationDegreeAmount, 0);
        }
    }

    public void BuildBlueprint()
    {
        if (isBlueprintFollowingCursor && canBuild.canBuildBlueprint)
        {
            canBuild = blueprint.GetComponent<CanBuild>();
            blueprintMeshRenderer = blueprint.GetComponent<MeshRenderer>();
            if (canBuild.canBuildBlueprint)
            {
                blueprint.layer = 0;
                isBlueprintFollowingCursor = false;
                blueprintMeshRenderer.material = solidMaterial;
                canBuild.isSolidObject = true;
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

            blueprint.layer = 2;
            isBlueprintFollowingCursor = true;
            blueprintMeshRenderer.material = blueprintMaterial;
            canBuild.isSolidObject = false;
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
