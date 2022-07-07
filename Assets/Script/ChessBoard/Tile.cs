using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject highLight;
    private void Start()
    {
        highLight.SetActive(false);
    }

    private void OnMouseEnter()
    {
        Debug.Log(transform.name);
        highLight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highLight.SetActive(false);
    }
}
