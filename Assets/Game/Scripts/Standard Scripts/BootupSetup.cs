using UnityEngine;

public class BootupSetup : MonoBehaviour
{

    private void Awake()
    {
        // This is to make it only run once
        DontDestroyOnLoad(this);

        // Setup the PlayerStats script so that it can accept the load data.
        PlayerStats.Setup();
    }
}