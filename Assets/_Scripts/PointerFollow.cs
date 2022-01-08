using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerFollow : MonoBehaviour
{
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition); //Convierte las coordenadas del ratón en coordenadas del mundo
        mousePos = new Vector3(mousePos.x, mousePos.y); // x, y, 0
        transform.position = mousePos;
    }
}
