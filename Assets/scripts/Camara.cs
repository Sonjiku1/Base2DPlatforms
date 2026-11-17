using UnityEngine;
using System.Collections;

public class Camara : MonoBehaviour

    
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector3 offset;
    [SerializeField] float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = playerTransform.position - offset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position - offset, speed );
    }
}
