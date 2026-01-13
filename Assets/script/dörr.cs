using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dörr : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer;
    [SerializeField] string neededTagName;

    [SerializeField] float openPositionY;

    [SerializeField] float textRevealDelay;

    [SerializeField] bool isLevelTransition;
    [SerializeField] string nextLevel;

    [SerializeField] TextMeshProUGUI TextField;

    bool isPlayerNearby = false;

    float startPosY;

    void Start()
    {
        isPlayerNearby = false;

        startPosY = transform.position.y;
    }

    void Update()
    {
        if (isPlayerNearby == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, startPosY + openPositionY, transform.position.z), 0.02f);
        }
        else if (isPlayerNearby == false && transform.position.y > startPosY)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, startPosY, transform.position.z), 0.02f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(neededTagName) && ((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            isPlayerNearby = true;

            if (nextLevel != null)
            {
                StartCoroutine(SwitchScene(nextLevel));
            }
        }
        else
        {
            StartCoroutine(RevealText("You're not the right shape"));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isPlayerNearby && collision.gameObject.CompareTag(neededTagName) && ((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            isPlayerNearby = false;
        }
    }

    IEnumerator SwitchScene(string scene)
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(scene);
    }

    IEnumerator RevealText(string finalText)
    {
        TextField.text = "";

        foreach (char c in finalText)
        {
            TextField.text += c;

            yield return new WaitForSeconds(textRevealDelay);
        }

        yield return new WaitForSeconds(2);

        TextField.text = "";
    }
}