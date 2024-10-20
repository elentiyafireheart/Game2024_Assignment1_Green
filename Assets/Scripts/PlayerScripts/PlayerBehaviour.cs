using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float _speed;


    [SerializeField] private Boundary _horizontalBoundary;
    [SerializeField] private Boundary _verticalBoundary;
    [SerializeField] private bool _isTestMobile;
    public GameObject _bulletPrefab;
    public Transform StarPoint;

    Camera _camera;
    Vector2 _destination;

    private bool _isMobilePlatform = true;
    ScoreScript _scoreScript;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _scoreScript = FindObjectOfType<ScoreScript>();

        if (!_isTestMobile)
        {
            _isMobilePlatform = Application.platform == RuntimePlatform.Android ||
                                Application.platform == RuntimePlatform.IPhonePlayer;
        }
 
    }

    // Update is called once per frame
    void Update()
    {
   
       if (_isMobilePlatform)
       {
           GetTouchInput();
       }
       else
       {
           GetTraditionalInput();
       }

       Move();

       CheckBoundaries();
    }

    void Move()
    {
        transform.position = _destination;
    }

    void GetTraditionalInput()
    {
        // Get input and calculate movement
        float axisX = Input.GetAxisRaw("Horizontal") * Time.deltaTime * _speed;
        float axisY = Input.GetAxisRaw("Vertical") * Time.deltaTime * _speed;

        // Applies movement to transform
        _destination = new Vector3(axisX + transform.position.x, axisY+transform.position.y, 0);
    }

    void GetTouchInput()
    {
        foreach (Touch touch in Input.touches)
        {
            _destination = _camera.ScreenToWorldPoint(touch.position);
            _destination = Vector2.Lerp(transform.position, _destination, _speed * Time.deltaTime);
        }

    }

    void CheckBoundaries()
    {

        // check if player is out of bounds, spawns on other side
        if (transform.position.x > _horizontalBoundary.max)
        {
            transform.position = new Vector3(_horizontalBoundary.min, transform.position.y, 0);
        }
        else if (transform.position.x < _horizontalBoundary.min)
        {
            transform.position = new Vector3(_horizontalBoundary.max, transform.position.y, 0);
        }


        // check if player passes boundary N/S, stops movement on vertical
        if (transform.position.y > _verticalBoundary.max)
        {
            transform.position = new Vector3(transform.position.x, _verticalBoundary.max, 0);
        }
        else if (transform.position.y < _verticalBoundary.min)
        {
            transform.position = new Vector3(transform.position.x, _verticalBoundary.min, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyTag"))
        {
            _scoreScript.ChangeScore(100);
            //Destroy(collision.gameObject);
           // collision.gameObject.SetActive(false);
           collision.GetComponent<EnemyBehaviour>().DyingSequence();
           StartCoroutine(collision.GetComponent<EnemyBehaviour>().DyingRoutine());

        }
    }
}
