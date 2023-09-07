using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTransform : MonoBehaviour
{
    public float playerSpeed = 1;
    public float playerSpeedDia = 1;
    bool diagonal = false;
    public static bool lockMovement = false;

    void FixedUpdate()
    {
        #region BasicMovement
        // Basic Player Movement
        if (Input.GetKey("w") && !diagonal && !lockMovement)
        {
            transform.position += Vector3.up * playerSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKey("a") && !diagonal && !lockMovement)
        {
            transform.position += Vector3.left * playerSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.GetKey("s") && !diagonal && !lockMovement)
        {
            transform.position += Vector3.down * playerSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        if (Input.GetKey("d") && !diagonal && !lockMovement)
        {
            transform.position += Vector3.right * playerSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, 0, 270);
        }
        #endregion
        #region Diagonal
        // Diagonal Player Movement
        if (Input.GetKey("w") && Input.GetKey("a") && !lockMovement)
        {
            transform.position += Vector3.up * playerSpeedDia * Time.deltaTime;
            transform.position += Vector3.left * playerSpeedDia * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, 0, 45);
            diagonal = true;
        }
        else if (Input.GetKey("a") && Input.GetKey("s") && !lockMovement)
        {
            transform.position += Vector3.left * playerSpeedDia * Time.deltaTime;
            transform.position += Vector3.down * playerSpeedDia * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, 0, -225);
            diagonal = true;
        }
        else if (Input.GetKey("s") && Input.GetKey("d") && !lockMovement)
        {
            transform.position += Vector3.down * playerSpeedDia * Time.deltaTime;
            transform.position += Vector3.right * playerSpeedDia * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, 0, 225);
            diagonal = true;
        }
        else if (Input.GetKey("d") && Input.GetKey("w") && !lockMovement)
        {
            transform.position += Vector3.right * playerSpeedDia * Time.deltaTime;
            transform.position += Vector3.up * playerSpeedDia * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, 0, -45);
            diagonal = true;
        }
        else
        {
            diagonal = false;
        }
        #endregion
    }
}
