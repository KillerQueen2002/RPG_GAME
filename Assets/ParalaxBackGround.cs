using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(300)]
public class ParalaxBackGround : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private float paralaxEffect;

    private float xPosition;
    private float lenght;
        

    private void Start()
    {
        cam = GameObject.Find("Main Camera");

        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
    }

    private void FixedUpdate()
    {
        float distanceMoved = cam.transform.position.x * (1 - paralaxEffect);
        float distanceToMove = cam.transform.position.x * paralaxEffect;

        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);


        if (distanceMoved > xPosition + lenght)
            xPosition = xPosition + lenght;
        else if (distanceMoved < xPosition - lenght)
            xPosition = xPosition - lenght;
    }
}
