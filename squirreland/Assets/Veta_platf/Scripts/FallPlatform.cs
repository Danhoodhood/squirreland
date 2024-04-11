using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{
    private Rigidbody2D _myRigidbody;
    private float _collapseTime = 0.5f; // Установите желаемое время для коллапса платформы

    private void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        _myRigidbody.isKinematic = true;
        _myRigidbody.gravityScale = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Collapse());
        }
    }

    private IEnumerator Collapse()
    {
        yield return new WaitForSeconds(_collapseTime);
        FallDown();
    }

    private void FallDown()
    {
        _myRigidbody.isKinematic = false;
        _myRigidbody.gravityScale = 1f;
    }
}
