using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHeadMovement : MonoBehaviour
{
    public float Rotation;
    Transform transform;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = new Vector3(
            Mathf.Clamp(Mathf.Sin(Rotation) * (transform.parent.localScale.x), -transform.parent.localScale.x/2, transform.parent.localScale.x/2 ),
            Mathf.Clamp(Mathf.Cos(Rotation) * (transform.parent.localScale.y), -transform.parent.localScale.y/2, transform.parent.localScale.y/2)
        );
        float crot = Mathf.Atan2(-transform.position.y, -transform.position.x );
        transform.rotation = Quaternion.Euler(0, 0, crot * Mathf.Rad2Deg + 90);
    }
}
