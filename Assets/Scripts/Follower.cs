using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Follower : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private Player _target;
    [SerializeField] private float _minDistance = 2f;
    [SerializeField] private float _gravity = 50f;
    [SerializeField] private float _jumpForce = 0.5f;
    [SerializeField] private LayerMask _stepMask;

    private Rigidbody _rigidbody;
    private float _stepHeight = 0.5f;
    private float _forwardOffset = 0.5f;
    private float _checkStepDistance = 0.5f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, _target.transform.position);

        _rigidbody.AddForce(Vector3.down * _gravity, ForceMode.Acceleration);

        if (IsTargetReached())
        {
            _rigidbody.velocity = Vector3.zero;
        }
        else
        {
            Vector3 direction = (_target.transform.position - transform.position).normalized;

            OvercomeSteps();
            
            _rigidbody.velocity = direction * _speed;
        }
    }

    private void OvercomeSteps()
    {
        Vector3 forwardPos = transform.position + transform.forward * _forwardOffset;

        if (Physics.Raycast(forwardPos + Vector3.up * _stepHeight, Vector3.down, out RaycastHit hit, _checkStepDistance, _stepMask))
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsTargetReached()
    {
        return transform.position.IsEnoughClose(_target.transform.position, _minDistance);
    }
}
