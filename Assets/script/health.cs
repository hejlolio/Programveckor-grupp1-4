using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class health : MonoBehaviour
{
    public HealthBar healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float liv, maxhealth = 1f;
    void Start()
    {
        liv = maxhealth;
        healthBar.SetMaxHealth(maxhealth);
    }
    public void TakeDamage(float damageAmount)
    {
        liv -= damageAmount;
        healthBar.SetHealth(liv);
        if (liv <= 0)
        {
            StartCoroutine(DelayDeathSceneLoad());
        }
    }
    private IEnumerator DelayDeathSceneLoad()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
        SceneManager.LoadScene("death scene");
    }

}
