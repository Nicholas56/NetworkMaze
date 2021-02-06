using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MazeRingScript : MonoBehaviour
{
    public List<MazeCorner> corners;

    private void Start()
    {     
        for (int i = 0; i < corners.Count; i++)
        {
            int randNum = Random.Range(0, 4);
            RotateCorner(randNum, i);
        }
    }

    public void RotateCorner(int timesRight,int cornerNum) 
    {
        int angle = timesRight * 90;
        for (int i = 0; i < 2; i++)
        {
            //rotate each adjacent wall around the corner point
            corners[cornerNum].adjacentWalls[i].RotateAround(corners[cornerNum].corner.position, Vector3.up, angle);
        }
    }
    public void SwitchAction(int clockwiseTurns, int cornerNum) { RotateCorner(clockwiseTurns, cornerNum); }
}

[System.Serializable]
public class MazeCorner
{
    public Transform corner;
    public Transform[] adjacentWalls;
}
