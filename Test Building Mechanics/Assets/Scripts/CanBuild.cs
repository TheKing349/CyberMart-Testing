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
        Debug.Log("ENTERED: CANNOT");
        if (!isSolidObject)
        {
            canBuildBlueprint = false;
            transform.GetComponent<MeshRenderer>().material = blueprintErrorMaterial;
        }
    }

    private void CanBuildBlueprint()
    {
        Debug.Log("EXITED: CAN");
        if (!isSolidObject)
        {
            canBuildBlueprint = true;
            transform.GetComponent<MeshRenderer>().material = blueprintMaterial;
        }
    }
}