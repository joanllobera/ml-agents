using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private bool dirRight = true;
    public float len = 9.0f;
    public float speed = 2.0f;

    // Update is called once per frame
    void Update() {
        if (dirRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate(-Vector2.right * speed * Time.deltaTime);

        if (transform.position.x >= len)
        {
            dirRight = false;
        }

        if (transform.position.x <= -len)
        {
            dirRight = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        dirRight = !dirRight;
    }

}
