using FPS.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int value = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        other.gameObject.GetComponent<InventoryItems>().IncreaseMoney(value);

        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

        GetComponent<AudioSource>().Play();

        Transform[] transforms = GetComponentsInParent<Transform>();

        foreach (Transform t in transforms)
        {
            Destroy(t.gameObject, GetComponent<AudioSource>().clip.length);
        }
    }
}
