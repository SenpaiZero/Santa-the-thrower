using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    bool isMove = false;
    [SerializeField] private Transform target;

    private void Update()
    {
        if (isMove == true)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    public void setMove(bool isMove)
    {
        this.isMove = isMove;
    }
}
