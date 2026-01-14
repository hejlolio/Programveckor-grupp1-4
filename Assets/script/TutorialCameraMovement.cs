using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialCameraMovement : MonoBehaviour
{
    [SerializeField] List<GameObject> cameraPoints; //lista över positioner som kameran ska röra sig till
    [SerializeField] List<TextMeshProUGUI> textBoxes; //text lådor som ska ändras
    [SerializeField] List<String> textBoxStrings; //text som ska skrivas i lådor

    [SerializeField] float textDelay; //tid mellan varje bokstav

    [SerializeField] GameObject player;
    tempMove playerScript;

    int activeCamera = 0;
    
    void Start()
    {
        playerScript = player.GetComponent<tempMove>();

        playerScript.isControlled = false;
        StartCoroutine(Logic());
    }

    void FixedUpdate() //här hanteras rörelsen av kameran
    {
        Camera.main.transform.position = UnityEngine.Vector3.Lerp(Camera.main.transform.position, new UnityEngine.Vector3(cameraPoints[activeCamera].transform.position.x, cameraPoints[activeCamera].transform.position.y, Camera.main.transform.position.z), 0.001f);
    }

    IEnumerator Logic()
    {
        foreach (GameObject cp in cameraPoints)
        {
            activeCamera += 1;

            yield return new WaitForSeconds(4);

            StartCoroutine(RevealText(textBoxStrings[activeCamera], textBoxes[activeCamera]));

            yield return new WaitForSeconds(4);
        }

        playerScript.isControlled = true;

        this.enabled = false;
    }

    IEnumerator RevealText(string toWrite, TextMeshProUGUI Text)
    {
        foreach (char c in toWrite)
        {
            Text.text += c;

            yield return new WaitForSeconds(textDelay);
        }
    }
}
