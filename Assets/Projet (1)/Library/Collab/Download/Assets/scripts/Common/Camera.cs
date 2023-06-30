using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Transform Target;

    private Vector3 moveTemp;

    [SerializeField] float speed = 5;
    private float xDifference;
    private float yDifference;
    private float yminvalue = 1;
    private float xminvalue = -7;
    
    [SerializeField] float movementthreshold = 2;
   
    void follow()
    {
        if (Target.transform.position.x > transform.position.x)
        {
            xDifference = Target.transform.position.x - transform.position.x;
        } else
        {
            xDifference = transform.position.x - Target.transform.position.x;
        }
        if (Target.transform.position.y > transform.position.y)
        {
            yDifference = Target.transform.position.y - transform.position.y;
        }
        else
        {
            yDifference = transform.position.y - Target.transform.position.y;
        }
        if (xDifference >= movementthreshold || yDifference >= movementthreshold)
        {
            moveTemp = Target.transform.position;
            moveTemp.z = -10;
            moveTemp.y = Mathf.Clamp(Target.position.y, yminvalue, Target.position.y);
            moveTemp.x = Mathf.Clamp(Target.position.x, xminvalue, Target.position.x);
            transform.position = Vector3.MoveTowards(transform.position, moveTemp, speed * Time.deltaTime);         
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        follow();
    }
}
