using FPS.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBullet : MonoBehaviour
{
    [SerializeField] [Range(5, 100)] float destroyAfter;
    [SerializeField] float speed = 1f;

    GameObject instigator;
    Transform target;
    float damage = 0f;

    private void Start()
    {
        transform.LookAt(GetAimLocation());

        Destroy(gameObject, destroyAfter);
    }

    public void Initialization(GameObject instigator, Transform target, float damage)
    {
        this.instigator = instigator;
        this.target = target;
        this.damage = damage;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Health>().TakeDamage(instigator, damage);
            Destroy(gameObject);
        }
    }

    private Vector3 GetAimLocation()
    {
        return new Vector3(target.position.x, target.position.y + 0.3f, target.position.z);
    }
}
