using UnityEngine;
using UnityEngine.UI;

public class SlagButton : MonoBehaviour
{
    public SlagTypes SlagToSell;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate(){
            SlagCount.SlagSell(SlagToSell);
        });
    }

}
