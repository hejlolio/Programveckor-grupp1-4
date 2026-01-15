using UnityEngine;

public class killing : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        if (collision.gameObject.TryGetComponent<health>(out health enemycompoment))
        {
            enemycompoment.TakeDamage(1);

        }
        Destroy(gameObject);

        //I'm killing you. I'm killing you.
        //I don't care about anything else, I don't give a shit about anything else, I- My programming is just "GET THAT FUCKING GUY RIGHT NOW".
        //It doesn't- There's no, like, "Oh, he's running? Oh, back off a little!", it's just THUMP THUMP THUMP until I get you. 
    }

}
