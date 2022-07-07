using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDisActivator : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject);
    }
}
