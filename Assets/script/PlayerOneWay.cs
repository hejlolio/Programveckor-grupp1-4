using System.Collections;
using UnityEngine;

public class PlayerOneWay : MonoBehaviour
{

    private GameObject CurrentOneWayPlatform;

    [SerializeField] private CircleCollider2D PlayerCollider;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (CurrentOneWayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            CurrentOneWayPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            CurrentOneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = CurrentOneWayPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(PlayerCollider, platformCollider);
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreCollision(PlayerCollider, platformCollider, false);
    }

}
