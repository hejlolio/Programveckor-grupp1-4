using UnityEngine;
using UnityEngine.SceneManagement;

public class health : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float liv, maxhealth = 1f;
    void Start()
    {
        liv = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void TakeDamage(float damageAmount)
    {
        liv -= damageAmount;

        if (liv <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("death scene");
        }
    }
}
