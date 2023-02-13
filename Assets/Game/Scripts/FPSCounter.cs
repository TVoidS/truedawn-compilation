using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private bool enable = false;
    public TextMeshProUGUI fpsDisplay;

    void Start()
    {
        fpsDisplay.gameObject.SetActive(enable);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.B)) 
        {
            enable = !enable;
            fpsDisplay.gameObject.SetActive(enable);
        }
        if (enable)
        {
            fpsDisplay.SetText("FPS:" + string.Format("{0,12:N3}", (1f/Time.deltaTime)));
        }
    }
}
