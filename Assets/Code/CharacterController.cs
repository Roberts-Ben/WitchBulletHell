using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += new Vector3(0f, Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * speed * Time.deltaTime;
    }
}
