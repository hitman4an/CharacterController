using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Follower : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private Player _target;
    [SerializeField] private float _minDistance = 2f;
    [SerializeField] private float _gravity = 50f;
    [SerializeField] private float _jumpForce = 0.5f;
    [SerializeField] LayerMask _stepMask;

    private Rigidbody _rigidBody;
    private float _stepHeight = 0.5f;
    private float _forwardOffset = 0.5f;
    private float _checkStepDistance = 0.5f;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, _target.transform.position);

        _rigidBody.AddForce(Vector3.down * _gravity, ForceMode.Acceleration);

        if (distance > _minDistance)
        {
            Vector3 direction = (_target.transform.position - transform.position).normalized;

            OvercomeSteps();
            
            _rigidBody.velocity = direction * _speed;
        }
        else
        {
            _rigidBody.velocity = Vector3.zero;
        }
    }

    private void OvercomeSteps()
    {
        Vector3 forwardPos = transform.position + transform.forward * _forwardOffset;

        if (Physics.Raycast(forwardPos + Vector3.up * _stepHeight, Vector3.down, out RaycastHit hit, _checkStepDistance, _stepMask))
        {
            _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}
