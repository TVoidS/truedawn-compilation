using UnityEngine;
using UnityEngine.UI;

public class Back : MonoBehaviour
{
    private Button BackBTN;
    public PauseUISwapper UISwapper;

    private void Start()
    {
        BackBTN = gameObject.GetComponent<Button>();
        BackBTN.onClick.AddListener(() =>
        {
            UISwapper.SwapToPauseUI();
        });
    }
}
