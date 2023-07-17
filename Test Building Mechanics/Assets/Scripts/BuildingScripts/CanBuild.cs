using UnityEngine;

public class CanBuild : MonoBehaviour
{
    public Material solidMaterial;
    public Material blueprintMaterial;
    public Material blueprintErrorMaterial;

    private int triggerCount = 0;

    [HideInInspector] public bool canBuildBlueprint = true;
    [HideInInspector] public bool isSolidObject = false;

    private void OnTriggerEnter()
    {
        triggerCount++;
        BuildBlueprint(false);
    }
    private void OnTriggerExit()
    {
        triggerCount--;
        BuildBlueprint(true);
    }

    private void OnCollisionEnter()
    {
        triggerCount++;
        BuildBlueprint(false);
    }
    private void OnCollisionExit()
    {
        triggerCount--;
        BuildBlueprint(true);
    }

    private void BuildBlueprint(bool canBuild)
    {
        if (!isSolidObject)
        {
            if ((canBuild) && (triggerCount == 0))
            {
                canBuildBlueprint = true;
                transform.GetComponent<MeshRenderer>().material = blueprintMaterial;
            }
            else if (!canBuild)
            {
                canBuildBlueprint = false;
                transform.GetComponent<MeshRenderer>().material = blueprintErrorMaterial;
            }
        }
    }
}