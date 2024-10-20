using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float _verticalSpeed;
    [SerializeField] private float _horizontalSpeed;

    [SerializeField] private Boundary _verticalSpeedRange;
    [SerializeField] private Boundary _horizontalSpeedRange;


    [SerializeField] private Boundary _verticalBoundary;
    [SerializeField] private Boundary _horizontalBoundary;

    SpriteRenderer _spriteRenderer;

    private Color[] _colors =
        { Color.green, Color.blue, Color.white, Color.magenta, Color.gray };

// Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        // Move enemy vertically or horizontally
        transform.position = new Vector2(Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalBoundary.max - _horizontalBoundary.min) + _horizontalBoundary.min,
            /*transform.position.x + _horizontalSpeed * Time.deltaTime*/
             transform.position.y + _verticalSpeed * Time.deltaTime);


        // Checks if player is off the screen from bottom
        // if yes reset enemy
        if (transform.position.y < _verticalBoundary.min)
        {
            Reset();
        }

        //// checks if player is off the screen from the side
        //// if yes, change horizontal speed to other direction
        //if (transform.position.x > _horizontalBoundary.max || transform.position.x < _horizontalBoundary.min)
        //{
        //    _horizontalSpeed = -_horizontalSpeed;
        //}
    }

    public IEnumerator DyingRoutine()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    public void DyingSequence()
    {
        _spriteRenderer.enabled = false;
        GetComponent<Collider2D>().enabled = false;

    }

    // reset  the enemys position and speeds
    private void Reset()
    {
        _spriteRenderer.color = _colors[Random.Range(0, _colors.Length)];
        _spriteRenderer.enabled = true;
        GetComponent<Collider2D>().enabled = true;
        transform.position = new Vector2(Random.Range(_horizontalBoundary.min, _horizontalBoundary.max), _verticalBoundary.max);
        transform.localScale = new Vector3(1.0f + Random.Range(-0.3f, 0.3f),
            1.0f + Random.Range(-0.3f, 0.3f), 1.0f + Random.Range(-0.3f, 0.3f));
        _verticalSpeed = Random.Range(_verticalSpeedRange.min, _verticalSpeedRange.max);
        _horizontalSpeed = Random.Range(_horizontalSpeedRange.min, _horizontalSpeedRange.max);
    }

}
