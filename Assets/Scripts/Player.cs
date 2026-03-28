using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(InputReader))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    
    private CharacterController _characterController;
    private InputReader _inputReader;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputReader = GetComponent<InputReader>();
    }

    private void Update()
    {
        if (_characterController != null)
        {
            Vector3 playerInput = new Vector3(_inputReader.GetHorizontal(), 0, _inputReader.GetVertical());
            playerInput *= Time.deltaTime * _speed;

            if (_characterController.isGrounded)
            {
                _characterController.Move(playerInput + Vector3.down);
            }
            else
            {
                _characterController.Move(_characterController.velocity + Physics.gravity * Time.deltaTime);
            }
        }
    }
}
