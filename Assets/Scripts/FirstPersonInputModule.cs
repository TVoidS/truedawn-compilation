using UnityEngine;
using UnityEngine.EventSystems;


/*
 * Class from a public post by EmmaEwert:
 * https://forum.unity.com/threads/fake-mouse-position-in-4-6-ui-answered.283748/
 * THANK YOU.
*/
public class FirstPersonInputModule : StandaloneInputModule
{
    protected override MouseState GetMousePointerEventData(int id)
    {
        var lockState = Cursor.lockState;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false; // I added these lines to the original code to avoid the blinking mouse problem that occurs after the mouse is freed.
        var mouseState = base.GetMousePointerEventData(id);
        Cursor.lockState = lockState;
        return mouseState;
    }

    protected override void ProcessMove(PointerEventData pointerEvent)
    {
        var lockState = Cursor.lockState;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        base.ProcessMove(pointerEvent);
        Cursor.lockState = lockState;
    }

    protected override void ProcessDrag(PointerEventData pointerEvent)
    {
        var lockState = Cursor.lockState;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        base.ProcessDrag(pointerEvent);
        Cursor.lockState = lockState;
    }
}

