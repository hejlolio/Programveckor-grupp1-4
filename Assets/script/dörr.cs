using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
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

    [SerializeField] List<Light2D> lights;

    bool isPlayerNearby = false;

    float startPosY;

    void Start()
    {
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
        Debug.Log($"Something entered trigger with {collision.gameObject.tag}, {collision.gameObject.layer}");

        if ((targetLayer.value & (1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            isPlayerNearby = true;

            if (nextLevel != "" && isLevelTransition == true)
            {
                StartCoroutine(SwitchScene(nextLevel));
            }
        }
        else
        {
            //StartCoroutine(RevealText("You're not the right shape"));
           // StartCoroutine(BlinkenLight());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isPlayerNearby && ((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            isPlayerNearby = false;
        }
    }

    IEnumerator SwitchScene(string scene)
    {
        yield return new WaitForSeconds(1.5f);

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

    IEnumerator BlinkenLight() 
    {
        for(int i = 0; i < 2; i++)
        {
            foreach(Light2D l in lights)
            {
                l.enabled = true;
            }

            yield return new WaitForSeconds(1);

            foreach(Light2D l in lights)
            {
                l.enabled = false;
            }

            yield return new WaitForSeconds(2);
        }
    }
}