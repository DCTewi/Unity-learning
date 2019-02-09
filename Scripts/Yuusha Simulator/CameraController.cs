using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smooth = 1.5f;

    private Vector3 offset;
    private Transform player;
    private float leftBoundary;
    private float rightBoundary;
    private float widthCamera;

    private void Awake()
    {
        widthCamera = gameObject.GetComponent<Camera>().orthographicSize / 9f * 16f;
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Transform>();
        offset = transform.position - player.position;

        leftBoundary = GameObject.FindGameObjectWithTag(Tags.Boundary).transform.Find("LeftDown").position.x;
        rightBoundary = GameObject.FindGameObjectWithTag(Tags.Boundary).transform.Find("RightUp").position.x;
    }

    private void LateUpdate()
    {
        Vector3 trueOff = new Vector3(offset.x * (player.GetComponent<PlayerController>().faceRight ? 1f : -1f), offset.y, offset.z);
        Vector3 targetPos = player.position + trueOff;

        if (targetPos.x - widthCamera < leftBoundary)
        {
            targetPos.x = leftBoundary + widthCamera;
        }
        else if (targetPos.x + widthCamera > rightBoundary)
        {
            targetPos.x = rightBoundary - widthCamera;
        }

        if (transform.position - player.position != trueOff)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
        }
    }
}
