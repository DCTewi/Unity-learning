using UnityEngine;
using DG.Tweening;

public class DOTCamera : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float strength;

    private Camera maincamera;

    private void Start()
    {
        maincamera = Camera.main;

        if (duration == 0) duration = 0.5f;
        if (strength == 0) strength = 1;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            maincamera.transform.DOShakePosition(duration : duration, strength : strength);
        }
    }
}
