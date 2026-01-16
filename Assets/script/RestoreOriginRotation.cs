using UnityEngine;

public class RestoreOriginRotation : MonoBehaviour
{
    Quaternion lastParentRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastParentRotation = transform.parent.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Inverse(transform.parent.localRotation) * lastParentRotation * transform.localRotation;

        lastParentRotation = transform.parent.localRotation;
    }
}
