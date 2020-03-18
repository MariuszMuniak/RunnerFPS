using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLifetime : MonoBehaviour
{
    [SerializeField] float distance = 1f;

    void Start()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.startLifetime = distance / main.startSpeed.constant;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
