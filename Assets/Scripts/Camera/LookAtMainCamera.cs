using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMainCamera : MonoBehaviour
{
    private void Start()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();

        foreach(Transform t in transforms)
        {
            if (t.gameObject == gameObject) { continue; }

            t.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
