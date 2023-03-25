/*  Filename:           PlayerData.cs
 *  Author:             Juan Munoz Rivera
 *  Last Update:        November 26, 2022
 *  Description:        Player data for recording the power up, position, enemy data, box data and scene name
 *  Revision History:   November 26, 2022 (Juan Munoz Rivera): Initial script.
 */

/// <summary>
/// class <c>PlayerData</c> saves the power up, position, enemy data, box data and scene name
/// </summary>

[System.Serializable]
public class PlayerData 
{
    public TempPlayerController.Powerup powerup;
    public float[] position = new float[2];
    public EnemyData[] enemiesData;
    public BoxData[] boxData;
    public string sceneName;

    public PlayerData(TempPlayerController playerController, EnemyData[] enemiesData, BoxData[] boxData ,  string sceneName)
    {
        this.powerup = playerController.currentPowerUp;
        position[0] = playerController.transform.position.x;
        position[1] = playerController.transform.position.y;
        this.enemiesData = enemiesData;
        this.boxData = boxData;
        this.sceneName = sceneName;
    }
}
