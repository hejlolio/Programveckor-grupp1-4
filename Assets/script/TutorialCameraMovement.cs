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

    [SerializeField] float cameraMoveSpeed; //hur snabbt kameran rör sig

    [SerializeField] float textDelay; //tid mellan varje bokstav

    [SerializeField] GameObject player;
    tempMove playerScript;

    GameObject activeCamera;
    
    void Start()
    {
        playerScript = player.GetComponent<tempMove>();

        playerScript.isControlled = false;
        StartCoroutine(Logic());
    }

    void Update() //här hanteras rörelsen av kameran
    {
        UnityEngine.Vector3 targetPos = new UnityEngine.Vector3(
            activeCamera.transform.position.x,
            activeCamera.transform.position.y,
            Camera.main.transform.position.y
        );

        Camera.main.transform.position = UnityEngine.Vector3.Lerp(Camera.main.transform.position, targetPos, Time.deltaTime * cameraMoveSpeed);
    }

    IEnumerator Logic() //här är själva logiken som bestämmer vilken position kameran ska röra sig till
    {
        foreach (GameObject cp in cameraPoints)
        {
            activeCamera = cp;

            yield return new WaitForSeconds(4);

            int index = cameraPoints.IndexOf(cp);
            yield return StartCoroutine(RevealText(textBoxStrings[index], textBoxes[index]));

            yield return new WaitForSeconds(2);
        }

        playerScript.isControlled = true;

        this.enabled = false;
    }

    IEnumerator RevealText(string toWrite, TextMeshProUGUI Text)
    {
        Text.text = "";

        foreach (char c in toWrite)
        {
            Text.text += c;

            yield return new WaitForSeconds(textDelay);
        }
    }
}