using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float smooth = 1.5f;

    private Transform player;
    private Vector3 relCameraPos;
    private float relCameraPosMag;
    private Vector3 newPos;

    private bool CheckViewPos(Vector3 checkPos)
    {
        RaycastHit hit;
        if (Physics.Raycast(checkPos, player.position - checkPos, out hit, relCameraPosMag))
        {
            if (hit.transform != player) return false;
        }
        newPos = checkPos;
        return true;
    }

    private void SmoothLookAt()
    {
        Vector3 relPlayerPos = player.position - transform.position;
        Quaternion lookAtRot = Quaternion.LookRotation(relPlayerPos, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRot, smooth * Time.deltaTime);
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Transform>();
        relCameraPos = this.transform.position - player.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f; //magnitude <=> size.
    }

    private void FixedUpdate()
    {
        Vector3 stdPos = player.position + relCameraPos;
        Vector3 abvPos = player.position + Vector3.up * relCameraPosMag;
        Vector3[] checkPoints = new Vector3[5];

        //Check points between camera and player.
        checkPoints[0] = stdPos;
        checkPoints[1] = Vector3.Lerp(stdPos, abvPos, 0.25f);
        checkPoints[2] = Vector3.Lerp(stdPos, abvPos, 0.50f);
        checkPoints[3] = Vector3.Lerp(stdPos, abvPos, 0.75f);
        checkPoints[4] = abvPos;

        //Lerp moving.
        for (int i = 0; i < checkPoints.Length; i++)
        {
            if (CheckViewPos(checkPoints[i])) break;
        }
        this.transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);
        //Rotate to look at player.
        SmoothLookAt();
    }
}
