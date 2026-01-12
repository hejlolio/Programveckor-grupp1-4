using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class cutsceneTest : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject target; //positionen som kameran ska fokusera på

    [SerializeField] 
    TextMeshProUGUI Text;

    [SerializeField]
    float seconds;

    [SerializeField]
    string finalText;

    tempMove playerScript;

    bool testControl = false; //debug
    bool test1 = true;

    void Start()
    {
        playerScript = player.GetComponent<tempMove>(); //gör så att spelaren inte kontrolleras
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            testControl = true;

            playerScript.isControlled = false;
        }

        if (testControl)
        {
            //gör så att kameran rör på sig långsamt
            Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, new Vector3(transform.position.x, transform.position.y, -100), 0.001f);
            if (test1)
            {
                StartCoroutine(RevealText());
                test1 = false;
            }
        }
    }

    IEnumerator RevealText()
    {
        foreach (char c in finalText)
        {
            Text.text += c;

            yield return new WaitForSeconds(seconds);
        }
    }
}