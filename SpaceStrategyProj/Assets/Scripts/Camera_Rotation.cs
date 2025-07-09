using UnityEngine;

public class Camera_Rotation : MonoBehaviour
{
    public float rotationSpeed = 100f;    // �������� �������� ������
    public float sensitivity = 0.01f;     // ���������������� ����
    public float moveSpeed = 5f;          // �������� �����������
    private bool isRotating = false;      // ���� ��� ������������ ������� ������ ����
    private bool isMoving = false;        // ���� ��� ������������ ��������
    private Vector3 targetPosition;       // ������� ������� �������
    private Quaternion startRotation;     // ��������� ������� ������

    void Update()
    {
        // ��������� ��������
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
            startRotation = transform.rotation;
            targetPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            Vector3 currentMousePos = Input.mousePosition;
            float xDiff = (currentMousePos.x - targetPosition.x) * sensitivity;
            float yDiff = (currentMousePos.y - targetPosition.y) * sensitivity;
            transform.Rotate(new Vector3(-yDiff * rotationSpeed, xDiff * rotationSpeed, 0));
            targetPosition = currentMousePos;
        }

        // ��������� ��������
        if (Input.GetKeyDown(KeyCode.W)) isMoving = true;
        if (Input.GetKeyUp(KeyCode.W)) isMoving = false;

        if (Input.GetKeyDown(KeyCode.S)) isMoving = true;
        if (Input.GetKeyUp(KeyCode.S)) isMoving = false;

        if (Input.GetKeyDown(KeyCode.A)) isMoving = true;
        if (Input.GetKeyUp(KeyCode.A)) isMoving = false;

        if (Input.GetKeyDown(KeyCode.D)) isMoving = true;
        if (Input.GetKeyUp(KeyCode.D)) isMoving = false;

        if (isMoving)
        {
            float horizontal = Input.GetAxis("Horizontal");    // A/D
            float vertical = Input.GetAxis("Vertical");        // W/S

            // ��������� ����������� �������� � ������ �������� ������
            Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
            moveDirection = transform.TransformDirection(moveDirection);

            // ��������� ��������
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }
}