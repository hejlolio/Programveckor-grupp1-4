using UnityEngine;


public class dropping : MonoBehaviour
{
    float timer = 0;
    Rigidbody2D rb;
    public GameObject square2;

    [SerializeField] GameObject bottlePrefab;
    [SerializeField] float velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1 && timer < 1.5)
        {
            //velocity på rb

        }


        //när y-värdet ör samma som suqares y värde. Sätt this .SetActive(false) och square SetActive(true)


        //om timer > 5 byt igen setActive på både. och ändr aposition för flaskan. nollställ velocity. 
    }

    public void Throw(GameObject target, Vector3 spawnPos)
    {
        bool Possible = CalculateTrajectory(Vector3.Distance(target.transform.position, transform.position), 2f, out float angle);

        if (Possible)
        {
            var spawned = Instantiate(bottlePrefab);

            spawned.transform.eulerAngles = new Vector3(angle, 0, 0);

            Rigidbody2D rb2 = spawned.GetComponent<Rigidbody2D>();
            rb2.linearVelocityX = velocity;
        }
    }

    public static bool CalculateTrajectory(float TargetDistance, float ProjectileVelocity, out float CalculatedAngle)
    {
        CalculatedAngle = 0.5f * (Mathf.Asin((-Physics.gravity.y * TargetDistance) / (ProjectileVelocity * ProjectileVelocity)) * Mathf.Rad2Deg);
        if (float.IsNaN(CalculatedAngle))
        {
            CalculatedAngle = 0;
            return false;
        }
        return true;
    }
}