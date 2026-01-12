using UnityEngine;

public class FlyCamera : MonoBehaviour
{
    public float mouseSensitivity = 2.0f; //cat de repede se roteste camera la miscarea mouse ului
    public float flySpeed = 10.0f; //viteza normala de zbor
    public float fastFlySpeed = 50.0f; //viteza de zbor rapida (când se apasa Shift)

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Start()
    {
        //blocam cursorul in mijlocul ecranului si il ascundem
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //citim rotatia initiala a camerei
        Vector3 rot = transform.localRotation.eulerAngles;
        rotationY = rot.y;
        rotationX = rot.x;
    }

    void Update()
    {
        //daca ne aflam in meniu, nu permitem miscarea camerei
        if (Time.timeScale == 0) return;

        // Rotatia
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 90); //limitam rotatia pe verticala pentru a evita rasturnarea camerei

        // Aplicam rotatia finala obiectului
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);

        // Miscarea
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? fastFlySpeed : flySpeed; //shift pentru viteza mare

        //citim WASD sau sagetile pentru miscare pe orizontala
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        //citim E si Q pentru miscare pe verticala
        float moveY = 0;
        if (Input.GetKey(KeyCode.E)) moveY = 1;
        if (Input.GetKey(KeyCode.Q)) moveY = -1;

        //calcul vecrtor de miscare si il aplicam pozitiei obiectului
        Vector3 move = transform.right * moveX + transform.forward * moveZ + transform.up * moveY;
        transform.position += move * currentSpeed * Time.deltaTime;
    }
}