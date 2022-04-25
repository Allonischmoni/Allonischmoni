using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    private bool doMovement = true;

    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;
    public float maxX = 10;
    public float minX = 10;
    public float maxZ = 10;
    public float minZ = 10;
    public float minDirX = 0.2f;
    public float maxDirX = 0.67f;
    public Quaternion r;

    // Update is called once per frame
    void Update()
    {
        if (Manager.gameIsOver)
        {
            this.enabled = false;
            return;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            doMovement = !doMovement;
        }
        if (!doMovement)
        {
            return;
        }
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        Quaternion    rotationX = transform .rotation ;

        pos.y -= scroll* 200 * scrollSpeed * Time.deltaTime;
        rotationX.x   -= scroll * 10 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        rotationX.x = Mathf.Clamp(rotationX.x, minDirX , maxDirX );

        
        transform.position = pos;
        transform.rotation = rotationX;
        r = transform.rotation;
    }
}
