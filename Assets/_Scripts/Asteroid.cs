using System;
using UnityEngine;
using Random = UnityEngine.Random;


[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour
{
    private const int FullCircle = 360;

    private float _size = 1f;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private AsteroidConfig _asteroidConfig;

    public AsteroidConfig AsteroidConfig => _asteroidConfig;

    public static event Action<Asteroid> OnAsteroidDestroyed;
    public static event Func<Asteroid> OnAsteroidSplit;

    public float Size
    {
        get => _size;
        set => _size = value;
    }

    public float MinSize => _asteroidConfig.MinSize;

    public float MaxSize => _asteroidConfig.MaxSize;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _spriteRenderer.sprite =
            _asteroidConfig.SpriteVariations[
                Random.Range(0, _asteroidConfig.SpriteVariations.Length)];

        transform.eulerAngles = new Vector3(0, 0, Random.value * FullCircle);

        transform.localScale = Vector3.one * _size;

        _rigidbody2D.mass = _size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody2D.AddForce(direction * _asteroidConfig.Speed);
        Invoke(nameof(SelfDestruction), _asteroidConfig.SelfDestructionTime);
    }

    private void SelfDestruction()
    {
        OnAsteroidDestroyed?.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Bullet>())
        {
            if ((_size * .5f) >= _asteroidConfig.MinSize)
            {
                CreateSplit();
                CreateSplit();
            }


            OnAsteroidDestroyed?.Invoke(this);
        }
    }

    private void CreateSplit()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * _asteroidConfig.SplitCircleOffset;


        var splitAsteroid = OnAsteroidSplit?.Invoke();
        splitAsteroid.transform.position = position;
        splitAsteroid.transform.rotation = transform.rotation;

        splitAsteroid.Size = Size * _asteroidConfig.SplitCircleOffset;

        splitAsteroid.SetTrajectory(Random.insideUnitCircle.normalized * _asteroidConfig.Speed);
    }
}