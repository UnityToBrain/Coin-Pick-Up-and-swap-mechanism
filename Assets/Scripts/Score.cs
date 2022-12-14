using System;
using UnityEngine;

public class Score : MonoBehaviour
{

    private Camera MainCam;
    void Start()
    {
        MainCam = Camera.main;
    }

    private void OnEnable()
    {
        Invoke("InActive",0.5f);
    }

    private void InActive()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        transform.LookAt(transform.position + MainCam.transform.forward);
    }
}
