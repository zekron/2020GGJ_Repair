using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"2D {collision.name} Enter");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log($"2D {collision.name} Stay");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"2D {collision.name} Exit");
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log($"3D {collision.name} Enter");
    }
    private void OnTriggerStay(Collider collision)
    {
        Debug.Log($"3D {collision.name} Stay");
    }
    private void OnTriggerExit(Collider collision)
    {
        Debug.Log($"3D {collision.name} Exit");
    }
}
