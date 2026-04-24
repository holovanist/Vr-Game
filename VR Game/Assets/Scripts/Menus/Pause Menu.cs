using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;

public class PauseMenu : MonoBehaviour
{
    public SnapTurnProvider SnapTurn;
    public ContinuousTurnProvider ContinuousTurn;
    public void Pause()
    {

    }
    public void Resume()
    {

    }
    public void SetTypeFromIndex(int index)
    {
        if(index == 0)
        {
            SnapTurn.enabled = true;
            ContinuousTurn.enabled = false;
        }
        else if(index == 1)
        {
            SnapTurn.enabled = false;
            ContinuousTurn.enabled = true;
        }
    }
}
