using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObject : MonoBehaviour
{
    private Rigidbody2D bounceRigidbody;
    private float power = 3;
    void Start()
    {
        bounceRigidbody = GetComponent<Rigidbody2D>();

        if (bounceRigidbody.transform.position.x > 0)
        {
            bounceRigidbody.AddForce(Vector2.left * power, ForceMode2D.Impulse);
        }
        else
        {
            bounceRigidbody.AddForce(Vector2.right * power, ForceMode2D.Impulse);
        }

        Destroy(gameObject, 5f);
    }
}
