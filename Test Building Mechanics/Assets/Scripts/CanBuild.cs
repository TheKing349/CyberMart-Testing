using UnityEngine;

public class CanBuild : MonoBehaviour
{
    public Material solidMaterial;
    public Material blueprintMaterial;
    public Material blueprintErrorMaterial;

    [HideInInspector] public bool canBuildBlueprint = true;
    [HideInInspector] public bool isSolidObject = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isSolidObject)
        {
            canBuildBlueprint = false;
            transform.GetComponent<MeshRenderer>().material = blueprintErrorMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isSolidObject)
        {
            canBuildBlueprint = true;
            transform.GetComponent<MeshRenderer>().material = blueprintMaterial;
        }
    }
}