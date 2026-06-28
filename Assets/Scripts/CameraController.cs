using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(0f, 4f, -6f);
    [SerializeField] private float rotationSpeed = 100f;

    private float mouseInput;
    
    void Start()
    {

    }

       void Update()
    {
        if (player == null)
        {
            return;
        }

        mouseInput = Input.GetAxis("Mouse X");

        player.Rotate(Vector3.up * mouseInput * rotationSpeed * Time.deltaTime);
        transform.position = player.position + player.TransformDirection(offset);
        transform.LookAt(player.position);
    }
}
