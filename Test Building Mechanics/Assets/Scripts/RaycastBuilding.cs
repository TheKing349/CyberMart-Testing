using Unity.Loading;
using UnityEditor.Rendering;
using UnityEngine;

public class RaycastBuilding : MonoBehaviour
{
    /* TO DO: 
     * Destroy build
     * move build
     * Fixed Distance???
     * Put keybinds in Inspector
     * 
     */
    private CanBuild canBuild;

    private GameObject clone;
    public GameObject[] prefabBlueprints;

    public bool isBlueprintFollowingCursor = false;
    private bool isGridSnap = false;
    private bool done = false;

    private int currentPrefabInt = -1;
    public int gridRotationDegreeAmount = 15;
    public float gridSize = 1f;
    public float blueprintRotationSpeed = 100f;

    public Material solidMaterial;
    public Material blueprintMaterial;
    public Material blueprintErrorMaterial;

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Alpha1)) && (!isBlueprintFollowingCursor))
        {
            currentPrefabInt = 0;
            done = false;
            isBlueprintFollowingCursor = true;
            clone = Instantiate(prefabBlueprints[currentPrefabInt]);
        }
        else if ((Input.GetKeyDown(KeyCode.Alpha2)) && (!isBlueprintFollowingCursor))
        {
            currentPrefabInt = 1;
            done = false;
            isBlueprintFollowingCursor = true;
            clone = Instantiate(prefabBlueprints[currentPrefabInt]);
        }
        else if ((Input.GetKeyDown(KeyCode.Alpha3)) && (!isBlueprintFollowingCursor))
        {
            currentPrefabInt = 2;
            done = false;
            isBlueprintFollowingCursor = true;
            clone = Instantiate(prefabBlueprints[currentPrefabInt]);
        }

        if ((Input.GetMouseButtonDown(0)) && (isBlueprintFollowingCursor) && (canBuild.canBuildBlueprint))
        {
            clone.layer = 0;
            done = false;
            isBlueprintFollowingCursor = false;
        }
        if ((Input.GetMouseButtonDown(1)) && (isBlueprintFollowingCursor) && (canBuild.canBuildBlueprint))
        {
            isBlueprintFollowingCursor = false;
            Destroy(clone);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            BuildBlueprint();
        }

        if (Input.GetKeyDown(KeyCode.G)) {
            isGridSnap = !isGridSnap;
            Debug.Log("Grid Snap " + (isGridSnap ? "On" : "Off"));
        }

        if (clone != null)
        {
            canBuild = clone.GetComponent<CanBuild>();
            if (isBlueprintFollowingCursor)
            {
                BlueprintToCursor();

                if (!isGridSnap)
                {
                    if (Input.GetKey(KeyCode.Q))
                    {
                        clone.transform.Rotate(Vector3.up * blueprintRotationSpeed * Time.deltaTime);
                    }
                    else if (Input.GetKey(KeyCode.E))
                    {
                        clone.transform.Rotate(Vector3.down * blueprintRotationSpeed * Time.deltaTime);
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        clone.transform.eulerAngles += new Vector3(0, gridRotationDegreeAmount, 0);
                    }
                    else if (Input.GetKeyDown(KeyCode.E))
                    {
                        clone.transform.eulerAngles += new Vector3(0, -gridRotationDegreeAmount, 0);
                    }
                }
            }
        }
    }
    void BlueprintToCursor()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if ((Physics.Raycast(ray, out RaycastHit hit)) && (hit.collider != null) && (hit.transform.CompareTag("Ground")))
        {
            Bounds b = prefabBlueprints[currentPrefabInt].GetComponent<MeshFilter>().sharedMesh.bounds;
            Vector3 lowerCenter = new Vector3(b.center.x * clone.transform.localScale.x, -b.extents.y * clone.transform.localScale.y - 0.01f, b.center.z * clone.transform.localScale.z);
            Vector3 newPos = hit.point - lowerCenter;

            if (!isGridSnap)
            {
                clone.transform.position = newPos;
            }
            else
            {
                newPos = new Vector3(Mathf.Round(newPos.x / gridSize) * gridSize, newPos.y, Mathf.Round(newPos.z / gridSize) * gridSize);

                float rotationY = clone.transform.eulerAngles.y;
                if (currentPrefabInt == 2)
                {
                    float offset = clone.transform.localScale.x / 2;
                    if ((Mathf.Approximately(rotationY, 0f)) || (Mathf.Approximately(rotationY, 180f)))
                    {
                        newPos.x += (rotationY > 90f && rotationY < 270f) ? -offset : offset;
                    }
                    else
                    {
                        newPos.z += (rotationY > 180f) ? offset : -offset;
                    }
                }

                clone.transform.position = newPos;

                if (!done)
                {
                    clone.transform.localScale = new Vector3(clone.transform.localScale.x - 0.00001f, clone.transform.localScale.y - 0.00001f, clone.transform.localScale.z - 0.00001f);
                    done = true;
                }
            }
        }
    }

    void BuildBlueprint()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if ((Physics.Raycast(ray, out RaycastHit hit)) && (hit.collider != null) && (hit.transform.CompareTag("Clone")))
        {
            canBuild = hit.transform.GetComponent<CanBuild>();
            if (canBuild.canBuildBlueprint)
            {
                hit.transform.GetComponent<MeshRenderer>().material = solidMaterial;
                hit.transform.GetComponent<MeshCollider>().isTrigger = false;
            }
        }
    }
}