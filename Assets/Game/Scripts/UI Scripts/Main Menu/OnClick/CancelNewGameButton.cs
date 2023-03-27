using UnityEngine;
using UnityEngine.UI;

public class CancelNewGameButton : MonoBehaviour
{
    public CanvasContentsController CanvasContentsController;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => {
            CanvasContentsController.SwapTo(0);
        });
    }
}
