using UnityEngine;

public class CanvasContentsController : MonoBehaviour
{
    public GameObject LoadingContainer;
    public GameObject SaveSelectContainer;
    public GameObject MainMenuContainer;

    void Start()
    {
        LoadingContainer.SetActive(false);
        SaveSelectContainer.SetActive(false);
    }

    /// <summary>
    /// Handles the swapping to different displays.
    /// Will automatically disable the current display and enable the desired display.
    /// </summary>
    /// <param name="container"> the ID for the containers. 0 for Main, 1 for Save, 2 for Loading. </param>
    public void SwapTo(byte container) 
    {
        switch (container) 
        {
            case 0: MainMenuContainer.SetActive(true); SaveSelectContainer.SetActive(false); LoadingContainer.SetActive(false); break;
            case 1: MainMenuContainer.SetActive(false); SaveSelectContainer.SetActive(true); LoadingContainer.SetActive(false); break;
            case 2: MainMenuContainer.SetActive(false); SaveSelectContainer.SetActive(false); LoadingContainer.SetActive(true); break;
        }
    }
}
