using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private float angle = 1;
    private void Update()
    {
        angle += 1f * rotationSpeed * Time.deltaTime;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
