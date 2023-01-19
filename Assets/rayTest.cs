using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class rayTest : MonoBehaviour
{
    GraphicRaycaster gr;
    PointerEventData ped;
    EventSystem es;

    // Start is called before the first frame update
    void Start()
    {
        gr = gameObject.GetComponent<GraphicRaycaster>();
        es = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        ped = new PointerEventData(es);
        ped.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        gr.Raycast(ped, results);

        foreach(RaycastResult result in results)
        {
            Debug.Log("Hit " + result.gameObject.name);
        }
    }
}
