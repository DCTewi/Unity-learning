using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smooth = 1.5f;
    public float shake = 0.05f;

    private Vector3 offset;
    private Transform player;
    private float leftBoundary;
    private float rightBoundary;
    private float widthCamera;
    private Camera self;

    public IEnumerator IEnumShake()
    {
        self.rect = new Rect(new Vector2(Mathf.Lerp(0f, 0.05f, 0.1f), Mathf.Lerp(0f, -0.05f, 0.1f)), new Vector2(1f, 1f));
        yield return new WaitForSeconds(0.1f);
        self.rect = new Rect(new Vector2(0f, 0f), new Vector2(1f, 1f));
    }

    public void Shake()
    {
        StartCoroutine(IEnumShake());
    }

    private void Start()
    {
        self = GetComponent<Camera>();
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
