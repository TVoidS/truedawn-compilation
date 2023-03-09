using System;

public static class UIController
{
    // Static Functionality.

    private static UIStates uistate = UIStates.Free;
    public static UIStates UIState => uistate;

    /// <summary>
    /// This doesn't message the new UI state to activate yet. TODO.
    /// </summary>
    /// <param name="newState"> the new UIState that we are going to be entering into. </param>
    /// <returns> Wether the change was successful. </returns>
    public static bool ChangeState(UIStates newState) 
    {
        if (UIState == UIStates.Free)
        {
            switch (newState) 
            {
                case UIStates.Free: 
                    break; // We didn't do anything?
                case UIStates.Paused: 
                    PauseController.Pause(); 
                    break;
                case UIStates.Inspect: 
                    throw new NotImplementedException();
                case UIStates.Inventory: 
                    throw new NotImplementedException();
                case UIStates.Other: 
                    throw new NotImplementedException();
                default: 
                    throw new InexplicableException("How did we get here. There isn't even an option for this.", new NotImplementedException());
            }
            uistate = newState;
            return true;
        }
        else if (newState == UIStates.Free)
        {
            switch (uistate) 
            {
                case UIStates.Free: // Do nothing, we were already there?
                    break;
                case UIStates.Paused: 
                    PauseController.Unpause(); 
                    break;
                case UIStates.Inspect: 
                    throw new NotImplementedException();
                    //break;
                case UIStates.Inventory: 
                    throw new NotImplementedException();
                    //break;
                case UIStates.Other: 
                    throw new NotImplementedException();
                    //break;
                default: 
                    throw new InexplicableException("How did we get here. There isn't even an option for this.", new NotImplementedException());
            }
            uistate = newState;
            return true;
        }
        else 
        {
            return false;
        }
    }
}

public enum UIStates 
{
    Free,
    Paused,
    Inventory,
    Inspect,
    Other
}
