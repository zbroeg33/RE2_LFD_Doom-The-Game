using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        private Transform target;                                    // target to aim for
        private GameObject[] players;

        private int i;


        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
           
	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {
             players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length >= 0) {
                target = players[i].transform;
            }

            GameObject closest = null;
                    float distance = Mathf.Infinity;
                    Vector3 postiion = players[i].transform.position;

            foreach ( GameObject player in players) {
                
                Vector3 diff = player.transform.position - transform.position;
                float curDistance = diff.sqrMagnitude;
                if(curDistance < distance) {
                    closest = player;
                    distance = curDistance;
                    if ((closest.transform.position - transform.position).magnitude <= 25.0f) {
                        agent.SetDestination(closest.transform.position);
                    }  else {
                        agent.SetDestination(this.transform.position);
                        //agent.isStopped = true;
                    }
                    
                }
            // if (target != null) {
            //     if ((target.position - transform.position).magnitude <= 25.0f) {
            //         agent.SetDestination(target.position);
            //     }
            // } else {
            //     players = GameObject.FindGameObjectsWithTag("Player");
            //     if (players.Length > 0) {
            //         target = players[0].transform;
            //     }
             }
            if (agent.remainingDistance > agent.stoppingDistance)
                character.Move(agent.desiredVelocity, false, false);
            else
                character.Move(Vector3.zero, false, false);
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
