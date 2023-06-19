using Unity.Loading;
using UnityEditor.Rendering;
using UnityEngine;

public class RaycastBuilding : MonoBehaviour
{
    /* TO DO: 
     * Destroy build
     * move build
     * Fixed Distance???
     */
    public Keybinds keybinds;
    private CanBuild canBuild;

    private GameObject clone;
    public GameObject[] prefabBlueprints;

    [HideInInspector] public bool isBlueprintFollowingCursor = false;
    [HideInInspector] public bool isGridSnap = false;
    private bool done = false;

    private int currentPrefabInt = -1;
    public int gridRotationDegreeAmount = 45;
    public float gridSize = 1f;
    public float blueprintRotationSpeed = 100f;

    public Material solidMaterial;
    public Material blueprintMaterial;
    public Material blueprintErrorMaterial;

    void Update()
    {
        if (clone != null)
        {
            if (isBlueprintFollowingCursor)
            {
                BlueprintToCursor();

                if (Input.GetKeyDown(keybinds.rotateLeftKey))
                {
                    RotateBlueprint("left");
                }
                else if (Input.GetKeyDown(keybinds.rotateRightKey))
                {
                    RotateBlueprint("right");
                }
            }
        }
    }

    public void RotateBlueprint(string position)
    {
        if (position == "left")
        {
            if (!isGridSnap)
            {
                clone.transform.Rotate(Vector3.up * blueprintRotationSpeed * Time.deltaTime);
            }
            else
            {
                clone.transform.eulerAngles += new Vector3(0, gridRotationDegreeAmount, 0);
            }
        }
        else if (position == "right")
        {
            if (!isGridSnap)
            {
                clone.transform.Rotate(Vector3.down * blueprintRotationSpeed * Time.deltaTime);
            }
            else
            {
                clone.transform.eulerAngles += new Vector3(0, -gridRotationDegreeAmount, 0);
            }

        }
    }

    public void RotateBlueprintRight()
    {
    }

    public void SelectBlueprint(int prefabNumber)
    {
        currentPrefabInt = prefabNumber;
        if (!isBlueprintFollowingCursor)
        {
            done = false;
            isBlueprintFollowingCursor = true;
            clone = Instantiate(prefabBlueprints[currentPrefabInt]);
        }
    }
    public void DeselectBlueprint()
    {
        if ((isBlueprintFollowingCursor) && (canBuild.canBuildBlueprint))
        {
            isBlueprintFollowingCursor = false;
            Destroy(clone);
        }
    }

    public void PlaceBlueprint()
    {
        if ((isBlueprintFollowingCursor) && (canBuild.canBuildBlueprint))
        {
            clone.layer = 0;
            done = false;
            isBlueprintFollowingCursor = false;
        }
    }

    public void DestroyBlueprint()
    {
        //Destroys already placed blueprint
    }

    public void MoveBlueprint()
    {
        //Moves already placed blueprint
    }

    public void BlueprintToCursor()
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

    public void BuildBlueprint()
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