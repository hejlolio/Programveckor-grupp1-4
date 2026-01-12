using UnityEngine;
using UnityEngine.SceneManagement;

public class dörr : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public LayerMask targetLayer;
    public LayerMask obstructionlayer;
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


        if (collision.tag == "Enemy2" && collision.gameObject.layer == 6)
        {
            SceneManager.LoadScene("level2");
        }
        else
        {
            print("You´re not the right shape!");
        }

    }

}
