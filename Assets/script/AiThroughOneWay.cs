using Pathfinding;
using UnityEngine;

public class AiThroughOneWay : MonoBehaviour
{
    public GameObject pathfinderLead;
    public AiPlatformerPath platformerPath;
    Path path;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platformerPath = pathfinderLead.GetComponent<AiPlatformerPath>();
        path = platformerPath.path;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < path.vectorPath.Count; i++)
        {
            Vector3 from = path.vectorPath[i];
            Vector3 to = path.vectorPath[i + 1];

            if (Physics.Linecast(from, to, LayerMask.NameToLayer("OnewayPlatform")))
            {

            }
        }

    }
}
