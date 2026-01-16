using System.Collections;
using UnityEngine;

public class AttackWhenSeeing : MonoBehaviour
{
    public GameObject thrownObject;
    public GameObject throwTarget;
    [SerializeField] float coolDown;
    public GameObject eyes;
    public LineOfSight LOS;
    public GameObject lead;
    public Rigidbody2D rb;

    bool gateFire = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LOS = eyes.GetComponent<LineOfSight>();
        rb = lead.GetComponent<Rigidbody2D>();
        gateFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (LOS.CanSeePlayer)
        {
            if (!gateFire)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
                print("PLAYER SEEN!");
                StartCoroutine(delayThrow());
            }
            gateFire = true;
        }
        if (!LOS.CanSeePlayer)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            StopCoroutine(delayThrow());
            gateFire = false;
        }
    }
    private IEnumerator delayThrow()
    {
        while (true && LOS.CanSeePlayer)
        {
            yield return new WaitForSeconds(0.4f);
            ThrowObject();
            yield return new WaitForSeconds(coolDown);
        }
    }

    void ThrowObject()
    {
        GameObject thrownObjectClone = Instantiate(thrownObject, transform.position, Quaternion.identity);
        FollowTarget followTarget = thrownObjectClone.GetComponent<FollowTarget>();
        followTarget.target = throwTarget.transform;
    }
}
