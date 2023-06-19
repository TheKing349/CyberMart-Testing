using UnityEngine;

public class CanBuild : MonoBehaviour
{
    [HideInInspector] public bool canBuildBlueprint = true;
    public Material solidMaterial;
    public Material blueprintMaterial;
    public Material blueprintErrorMaterial;
    private void OnTriggerEnter(Collider other)
    {
        canBuildBlueprint = false;
        transform.GetComponent<MeshRenderer>().material = blueprintErrorMaterial;
    }

    private void OnTriggerExit(Collider other)
    {
        canBuildBlueprint = true;
        transform.GetComponent<MeshRenderer>().material = blueprintMaterial;
    }
}