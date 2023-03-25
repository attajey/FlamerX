/*  Filename:           BoxData.cs
 *  Author:             Juan Munoz Rivera
 *  Last Update:        November 26, 2022
 *  Description:        Box data for recording the position and the game object name
 *  Revision History:   November 26, 2022 (Juan Munoz Rivera): Initial script.
 */

/// <summary>
/// class <c>BoxData</c> saves the position and name of the game object
/// </summary>
[System.Serializable]
public class BoxData
{
    public string name;
    public float[] position = new float[2];

    public BoxData(Box box)
    {
        name = box.gameObject.name;
        position[0] = box.transform.position.x;
        position[1] = box.transform.position.y;
    }
}