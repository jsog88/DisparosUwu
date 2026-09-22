using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    public float gravity = -9.81f;

    public float movementSpeed;
    public float rotationSpeed;

    private float currentRotationPos;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotation();
    }

    public void Movement()
    {
        Vector3 inputVector = new Vector3(0,0,InputController.Instance.movementVector.y);

        inputVector = transform.TransformDirection(inputVector);

        Vector3 finalMovement = (inputVector * movementSpeed) + (Vector3.up * gravity);

        characterController.Move(finalMovement * Time.deltaTime);
    }

    public void Rotation()
    {
        float rotationInput = InputController.Instance.movementVector.x * rotationSpeed * Time.deltaTime;

        currentRotationPos += rotationInput;

        transform.localRotation = Quaternion.AngleAxis(currentRotationPos, transform.up);
    }

}
