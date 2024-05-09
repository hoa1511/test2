using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using IndieMarc.EnemyVision;
using CanvasHaHa;

public class EnemySawPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Player")
        {
            //statePattern enemy
            this.GetComponent<NavMeshAgent>().enabled = false;
            this.GetComponent<Enemy>().run_speed = 0;
        
        }
    }
}
