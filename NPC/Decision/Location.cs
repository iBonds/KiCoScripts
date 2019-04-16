using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "D_Location_", menuName = "KiCo/Decisions/Location")]
public class Location : Decision
{
    public Vector3 location;
    public bool use_tag = true;
    public string tag;
    public float epsilon = 4f;

    [SerializeField]
    private Vector3 current_location;

    public override bool Decide(Controller controller)
    {
        current_location = controller.transform.position;
        if (use_tag)
        {
            if (controller.range.InRange(tag))
            {
                return Vector3.SqrMagnitude(controller.range.Position(tag) - current_location) < epsilon;
            }

        } else
        {
            return Vector3.SqrMagnitude(location - current_location) < epsilon;
        }
        
        return false;
    }
}
