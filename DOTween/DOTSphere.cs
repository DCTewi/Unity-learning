using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DOTSphere : MonoBehaviour
{
    private Tweener moving;
    private Material material;

    private void Start()
    {
        // transform.DOMove(new Vector3(3, 4, 0), 3);
        // transform.DOMove(new Vector3(2, 2, 0), 2).From(); // Global Position
        // transform.DOMove(new Vector3(2, 2, 0), 2).From(true); // Local 
        moving = transform.DOMove(new Vector3(2, 2, 0), 1);
        moving.Pause();
        moving.SetAutoKill(false);

        material = GetComponent<MeshRenderer>().material;
        Debug.Log("Get Material" + material);
        material.DOColor(Color.red, 3)
            .OnComplete(DOToBlue);
    }

    private void DOToBlue()
    {
        material.DOColor(Color.blue, 2);
    }

    public void PlayForward()
    {
        moving.PlayForward();
    }

    public void PlayBackward()
    {
        moving.PlayBackwards();
    }
}
