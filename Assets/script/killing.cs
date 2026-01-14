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
    }

}
