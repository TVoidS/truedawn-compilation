using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    public PauseUISwapper uISwapper;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate() {
            uISwapper.SwapToSaveUI(); 
        });
    }
}
