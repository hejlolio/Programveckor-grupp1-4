using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Animation : MonoBehaviour
{
    
    [SerializeField] private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("IsRunning", true);
            transform.localScale = new Vector3(1, 1, 1);
        }
       else if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("IsRunning", true);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }







    }
}
