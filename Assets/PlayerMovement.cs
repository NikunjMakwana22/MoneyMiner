using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Progress;

public class PlayerMovement : MonoBehaviour
{
    public DynamicJoystick dynamicJoystick;
    private Rigidbody rigidbody;
    private Vector3 dir;
    [SerializeField]private float speed;


    private void Start()
    {
        rigidbody= GetComponent<Rigidbody>();   
    }

    private void Update()
    {
     
     
            Debug.Log(dynamicJoystick.Direction);
            dir.x = dynamicJoystick.Direction.x;
            dir.z = dynamicJoystick.Direction.y;
            rigidbody.velocity = dir * speed * Time.deltaTime;

            transform.GetChild(0).rotation = Quaternion.Lerp(transform.GetChild(0).rotation, Quaternion.LookRotation(dir), Time.deltaTime * 20);
    }
}
