using UnityEngine;

public class ExitButton : ButtonBase
{    
    public override void Execute()
    {
        Application.Quit();
    }
}
