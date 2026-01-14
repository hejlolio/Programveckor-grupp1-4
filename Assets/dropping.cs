using UnityEngine;


public class dropping : MonoBehaviour
{
    float timer = 0;
    Rigidbody2D rb;
    public GameObject square2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //hämta rigidbody

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1 && timer < 1.5)
        {
            //velocity på rb
            rb.linearVelocity


        }


        //när y-värdet ör samma som suqares y värde. Sätt this .SetActive(false) och square SetActive(true)


        //om timer > 5 byt igen setActive på både. och ändr aposition för flaskan. nollställ velocity. 
    }

}
}
