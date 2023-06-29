using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpinningRat : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    
    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private bool _isStop = false;
    
    public SpinningType spinningType;
    private static readonly int Color1 = Shader.PropertyToID("_Color");
    private Camera _camera;

    public enum SpinningType
    {
        Transform,
        Physic
    }

    private void Start()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            TouchRat();
        }
        RotateRat();
    }

    private void RotateRat()
    {
        if(_isStop) return;
        switch (spinningType)
        {
            case SpinningType.Transform:
                transform.Rotate(new Vector3(0, speed, 0) * Time.deltaTime);
                break;
            case SpinningType.Physic:
                _isStop = true;
                _rigidbody.AddTorque(new Vector3(0, speed, 0), ForceMode.Force);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void TouchRat()
    {
        var ray = _camera.ScreenPointToRay (Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit, 100)) return;
        
        if(hit.transform == transform)
            _renderer.material.SetColor(Color1, GetRandomColor());
    }

    private Color GetRandomColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
