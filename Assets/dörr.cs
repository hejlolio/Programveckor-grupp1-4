using UnityEngine;

public class dörr : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    bool isPlayerNearby = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayerNearby = true)
        {
            print("You´re not the right shape!");
        }
        if (gameObject.tag == "Enemy2")
        {

        }

    }

}
