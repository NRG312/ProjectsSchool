using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {

    public NavMeshAgent agent;
    public GameObject target;

    void Start() {

        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {

        agent.SetDestination(target.transform.position);
    }
}
