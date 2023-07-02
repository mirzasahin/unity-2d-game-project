using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   [SerializeField] private Transform target;


    // Update is called once per frame
   private void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y + 2, transform.position.z );
    }
}
