using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SwitchScript : MonoBehaviour
{
    public List<MazeEvent> mazeEvents;

    public void MazeAction()
    {
        for (int i = 0; i < mazeEvents.Count; i++)
        {
            mazeEvents[i].ringToAffect.SwitchAction(mazeEvents[i].clockwiseTurns, mazeEvents[i].corner);
            //If reversible, the next time it goes back to original position, else it doesn't move again
            if (mazeEvents[i].isReversible) { mazeEvents[i].clockwiseTurns = 0 - mazeEvents[i].clockwiseTurns; }
            else { mazeEvents[i].clockwiseTurns = 0; }
        }
    }
}

[System.Serializable]
public class MazeEvent
{
    public MazeRingScript ringToAffect;
    public int corner;
    public bool isReversible;
    public int clockwiseTurns;
}
