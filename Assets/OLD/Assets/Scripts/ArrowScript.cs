using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public Vector3 WorldToScreen, screenToViewportPoint, offset;
    public Transform LookAt;
    public Camera cam;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        WorldToScreen = cam.WorldToScreenPoint(LookAt.position + offset);
        screenToViewportPoint = cam.ScreenToViewportPoint(LookAt.position);

        transform.position = WorldToScreen;
    }
}
