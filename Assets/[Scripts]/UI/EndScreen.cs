using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{

    public GameObject win;
    public GameObject loss;

    public GameObject textToEdit;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
