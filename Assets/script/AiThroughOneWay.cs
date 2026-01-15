using UnityEngine;

public class AiThroughOneWay : MonoBehaviour
{
    public GameObject pathfinderLead;
    public AiPlatformerPath platformerPath;
    public Collider2D myCollider2D;
    LayerMask mask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platformerPath = pathfinderLead.GetComponent<AiPlatformerPath>();
        myCollider2D = GetComponent<Collider2D>();
        mask = LayerMask.GetMask("OnewayPlatform");
    }

    // Update is called once per frame
    void Update()
    {
        var path = platformerPath.path;

        if (path == null)
        {
            return;
        }

        for (int i = 0; i < path.vectorPath.Count - 1; i++)
        {
            Vector3 from = path.vectorPath[i];
            from.z = 0;
            Vector3 to = path.vectorPath[i + 1];
            to.z = 0;


            RaycastHit2D hit = Physics2D.Linecast(from, to, mask);
            {
                if (hit.collider != null)
                {
                    Physics2D.IgnoreCollision(myCollider2D, hit.collider, true);
                }
            }
        }

    }
}
