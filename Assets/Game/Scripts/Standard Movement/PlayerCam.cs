using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{
    // GPT Test Vars
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;



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

        pointerEventData = new PointerEventData(null);
        eventSystem = EventSystem.current;
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

        // Only do the UI stuff if the game is running.
        if (UIController.UIState != UIStates.Paused)
        {
            // Testing code from ChatGPT, will edit with any corrections after seeing it in action.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) // Had to learn about the out keyword to realize this was fine.
            {
                Canvas canvas = hit.transform.GetComponent<Canvas>();
                if (canvas != null)
                {
                    pointerEventData.position = Input.mousePosition;

                    // GPT was way off here.  Had to make a new list before the raycast event so that I could load the results in.
                    List<RaycastResult> hits = new();

                    eventSystem.RaycastAll(pointerEventData, hits);
                    // It was trying to do this:
                    // RaycastAll(ped, null);
                    // RaycastResult[] res = RaycastResult[RaycastAll(ped).Count]
                    // RaycastAll(ped, res);

                    // Obviously doesn't work.

                    // Completely restructured to fit the new List format.
                    hits.ForEach(hit =>
                    {
                        if (hit.gameObject.GetComponent<Button>() != null)
                        {
                            if (Input.GetMouseButtonDown(0))
                            {
                                hit.gameObject.GetComponent<Button>().onClick.Invoke();
                            }
                            else
                            {
                                eventSystem.SetSelectedGameObject(hit.gameObject);
                            }
                        }
                        // If I want to select other input fields or things, I can add them here instead of GetComponent<Button>.
                    });

                    // Had to add this to cancel the selection if there were no targets.
                    // I don't like having my button glow as if it is selected when nothing can be done with it.
                    if (hits.Count == 0)
                    {
                        if (eventSystem.currentSelectedGameObject != null)
                        {
                            eventSystem.SetSelectedGameObject(null);
                        }
                    }

                }
            }
        }
    }
}
