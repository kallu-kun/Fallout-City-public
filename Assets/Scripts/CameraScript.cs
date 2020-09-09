using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private int screenWidth;
    private int screenHeight;

    private int cameraSpeed = 2;
    //private int borderMovementTresholdDistance = 100;

    private GameController gameController;

    private Camera cameraComponent;

    private Vector3 cursorPos;

    private Vector3 cameraOrigin;

    private Vector3 ScreenPos;

    private Vector3 move;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        cameraComponent = GetComponent<Camera>();
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
        Mathf.Clamp(transform.position.x, -32, 32),
        Mathf.Clamp(transform.position.y, 0, 32),
        Mathf.Clamp(transform.position.z, -10, -10));

        //keyboard commands
        if(Input.GetKey(KeyCode.D)) {
            transform.Translate(new Vector3(1,0,0));
        }
        if(Input.GetKey(KeyCode.A)) {
            transform.Translate(new Vector3(-1,0,0));
        }
        if(Input.GetKey(KeyCode.W)) {
            transform.Translate(new Vector3(0,1,0));
        }
        if(Input.GetKey(KeyCode.S)) {
            transform.Translate(new Vector3(0,-1,0));
        }

        //Dragging

        if(Input.GetMouseButtonDown(1)) {
            cursorPos = Input.mousePosition;
        }
        if(Input.GetMouseButton(1)) {
            Vector3 pos = Camera.main.ScreenToViewportPoint(cursorPos - Input.mousePosition);
            move = new Vector3(pos.x * cameraSpeed, pos.y * cameraSpeed, 0);
            transform.Translate(move, Space.World);
        }
        if(Input.GetMouseButtonUp(1)) {
            //transform.Translate(move, Space.World);
        }
        

        //cursor
        //checks that cursor is inside Game Screen

        /*
        if(Input.mousePosition.x < screenWidth && Input.mousePosition.x > 0
        && Input.mousePosition.y < screenHeight && Input.mousePosition.y > 0) {

            if(Input.mousePosition.x > screenWidth - borderMovementTresholdDistance 
            && transform.position.x < gameController.size/2) {
                transform.Translate(new Vector3(1,0,0) * Time.deltaTime * (cameraSpeed * cameraComponent.orthographicSize));
            }
            if(Input.mousePosition.x < borderMovementTresholdDistance
            && transform.position.x > -gameController.size/2) {
                transform.Translate(new Vector3(-1,0,0) * Time.deltaTime * (cameraSpeed * cameraComponent.orthographicSize));
            }
            if(Input.mousePosition.y > screenHeight - borderMovementTresholdDistance
            && transform.position.y < gameController.size/2) {
                transform.Translate(new Vector3(0,1,0) * Time.deltaTime * (cameraSpeed * cameraComponent.orthographicSize));
            }
            if(Input.mousePosition.y < borderMovementTresholdDistance
            && transform.position.y > 0) {
                transform.Translate(new Vector3(0,-1,0) * Time.deltaTime * (cameraSpeed * cameraComponent.orthographicSize));
            }
        }
        */
        

        // Scroll Zoom
        if(Input.mouseScrollDelta.y < 0 && cameraComponent.orthographicSize < 15) {
            cameraComponent.orthographicSize += 1;
        }
        if(Input.mouseScrollDelta.y > 0 && cameraComponent.orthographicSize > 1) {
            cameraComponent.orthographicSize -= 1;
        }
    }
}
