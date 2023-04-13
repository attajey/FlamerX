using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyController
{
    public GameObject[] activateOnDestroy;
    public GameObject[] deactivateOnDestroy;
    public GameObject[] grounds;
    public GameObject[] spawns;
    public GameObject[] originals;
    public Vector3[] positions;
    private int cursor = 0;
    private int every5 = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BossMechanics());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy() {
        GlobalVariables.totalScore += 10000;
        if ( player.transform.position.x >= -3 ) { player.transform.position = new Vector3( 1f , -5.8f , 0f ); }
        else player.transform.position = new Vector3( -7f , -5.8f , 0f );
        foreach ( GameObject go in spawns ) go.SetActive(false);
        foreach ( GameObject go in deactivateOnDestroy ) go.SetActive(false);
        foreach ( GameObject go in activateOnDestroy ) go.SetActive(true);
    }

    IEnumerator BossMechanics() {
        while ( true ) {
            yield return new WaitForSeconds( 1 );
            for ( int i = 0 ; i < grounds.Length ; i++ ) {
                if ( i == cursor ) grounds[i].SetActive(false);
                else grounds[i].SetActive(true);
            }
            cursor = (cursor + 1) % grounds.Length;
            every5 = (every5 + 1) % 5;
            if ( every5 == 4 ) {
                for ( int i = 0 ; i < spawns.Length ; i++ ) {
                    if ( spawns[i] == null ) {
                        GameObject go = Instantiate( originals[i] , positions[i] , Quaternion.identity );
                        spawns[i] = go;
                    }
                }
            }
        }
    }
}
