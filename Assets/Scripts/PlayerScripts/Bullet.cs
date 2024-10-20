using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public float Speed;
    ScoreScript _scoreScript;
    public AudioClip collisionSound;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _scoreScript = FindObjectOfType<ScoreScript>();
        GetComponent<AudioSource>().clip = collisionSound;
    }

    void Update()
    {
        _rigidbody.velocity = Vector2.up * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyTag"))
        {
            GetComponent<AudioSource>().Play();
            _scoreScript.ChangeScore(100);
            //Destroy(collision.gameObject);
            // collision.gameObject.SetActive(false);
            collision.GetComponent<EnemyBehaviour>().DyingSequence();
            StartCoroutine(collision.GetComponent<EnemyBehaviour>().DyingRoutine());

        }
    }

}
