using UnityEngine;
public class CanBuild : MonoBehaviour
{
    public Material solidMaterial;
    public Material blueprintMaterial;
    public Material blueprintErrorMaterial;

    [HideInInspector] public bool canBuildBlueprint = true;
    [HideInInspector] public bool isSolidObject = false;

    private void OnTriggerEnter()
    {
        CannotBuildBlueprint();
    }
    private void OnTriggerExit()
    {
        CanBuildBlueprint();
    }

    private void OnCollisionEnter()
    {
        CannotBuildBlueprint();
    }
    private void OnCollisionExit()
    {
        CanBuildBlueprint();
    }

    private void CannotBuildBlueprint()
    {
        if (!isSolidObject)
        {
            canBuildBlueprint = false;
            transform.GetComponent<MeshRenderer>().material = blueprintErrorMaterial;
        }
    }

    private void CanBuildBlueprint()
    {
        if (!isSolidObject)
        {
            canBuildBlueprint = true;
            transform.GetComponent<MeshRenderer>().material = blueprintMaterial;
        }
    }
}