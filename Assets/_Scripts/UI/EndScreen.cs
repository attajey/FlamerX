using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{

    public GameObject win;
    public GameObject loss;

    public TextMeshProUGUI textToEdit;

    public int[] grades;

    // Start is called before the first frame update
    void Start()
    {
        if ( GlobalVariables.loss ) {
            loss.SetActive( true );
            win.SetActive( false );
        }
        else {
            win.SetActive( true );
            loss.SetActive( false );
        }

        string finalGrade = "SABCDF";

        int totalScore = GlobalVariables.totalScore;

        if ( totalScore < grades[0]) finalGrade = "F";
        else if ( totalScore < grades[1] ) finalGrade = "D";
        else if ( totalScore < grades[2] ) finalGrade = "C";
        else if ( totalScore < grades[3] ) finalGrade = "B";
        else if ( totalScore < grades[4] ) finalGrade = "A";
        else finalGrade = "S";


        textToEdit.SetText(
            GlobalVariables.enemiesKilled.ToString() + '\n' +
            GlobalVariables.itemsCollected.ToString() + '\n' +
            '\n' +
            GlobalVariables.totalScore.ToString() + '\n' +
            finalGrade
            );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
