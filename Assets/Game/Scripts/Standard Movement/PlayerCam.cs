using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCam : MonoBehaviour
{
    public float senseX;
    public float senseY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senseX;
        float mouseY = -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senseY;

        xRotation += mouseX;
        yRotation += mouseY;

        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(yRotation, xRotation, 0);
        orientation.rotation = Quaternion.Euler(0, xRotation, 0);
    }
}
