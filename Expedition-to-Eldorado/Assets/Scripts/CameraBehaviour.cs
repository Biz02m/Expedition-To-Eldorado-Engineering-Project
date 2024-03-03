using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralEnumerations;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] DeckManager deckManager;
    [SerializeField] int viewType = (int)ViewTypes.BoardCards; //default setting
    int lastViewType;
    [SerializeField] float cameraHeight = 50f;
    [SerializeField] float cameraSpeed = 5f;
    [SerializeField] float mouseCameraSpeed = 10f;
    Vector3[] camPositions = new Vector3[4];
    private Quaternion _targetRotation = Quaternion.identity;
    public float turningRate = 30f;
    private float mouseBorder = 10f;

    // Start is called before the first frame update
    void Start()
    {
        camPositions[(int)ViewTypes.CardsOnly].x = 0;
        camPositions[(int)ViewTypes.CardsOnly].y = cameraHeight;
        camPositions[(int)ViewTypes.CardsOnly].z = 0;

        camPositions[(int)ViewTypes.BoardCards].x = 0;
        camPositions[(int)ViewTypes.BoardCards].y = cameraHeight;
        camPositions[(int)ViewTypes.BoardCards].z = 0;

        camPositions[(int)ViewTypes.BoardOnly].x = 0;
        camPositions[(int)ViewTypes.BoardOnly].y = cameraHeight;
        camPositions[(int)ViewTypes.BoardOnly].z = 0;

        camPositions[(int)ViewTypes.Shop].x = -20;
        camPositions[(int)ViewTypes.Shop].y = cameraHeight;
        camPositions[(int)ViewTypes.Shop].z = 0;

        setView(new Vector3(0,0));
    }

    // Update is called once per frame
    void Update()
    {
        handlePlayerInput();
        moveCamera();
    }

    private void moveCamera()
    {
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, camPositions[viewType], cameraSpeed * Time.deltaTime);
        mainCamera.transform.rotation = Quaternion.RotateTowards(mainCamera.transform.rotation, _targetRotation, turningRate * Time.deltaTime);
    }

    private void handlePlayerInput()
    {
        //check for changed view mode
        bool viewIsChanged = false;
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(viewType < (int)ViewTypes.BoardOnly)
            {
                viewType++;
                viewIsChanged = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (viewType > (int)ViewTypes.CardsOnly)
            {
                viewType--;
                viewIsChanged = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            lastViewType = viewType;
            viewType = (int)ViewTypes.Shop;
            viewIsChanged = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            viewType = lastViewType;
            viewIsChanged = true;
        }


        //check if player moved the camera with mouse
        Vector3 pos = transform.position;
        if (Input.mousePosition.y >= Screen.height - mouseBorder)
        {
            pos.z += mouseCameraSpeed * Time.deltaTime;
            viewIsChanged = true;
        }

        if(Input.mousePosition.y <= mouseBorder)
        {
            pos.z -= mouseCameraSpeed * Time.deltaTime;
            viewIsChanged = true;
        }

        if (Input.mousePosition.x >= Screen.width - mouseBorder)
        {
            pos.x += mouseCameraSpeed * Time.deltaTime;
            viewIsChanged = true;
        }

        if (Input.mousePosition.x <= mouseBorder)
        {
            pos.x -= mouseCameraSpeed * Time.deltaTime;
            viewIsChanged = true;
        }
        transform.position = pos;

        if (viewIsChanged)
        {
            setView(pos);
        }
    }

    private void setView(Vector3 camPosition)
    {
        deckManager.changeView((ViewTypes)viewType);
        //hardcoded values
        switch (viewType)
        {
            case (int)ViewTypes.CardsOnly:
                SetBlendedEulerAngles(new Vector3(50, 0, 0));
                break;
            case (int)ViewTypes.BoardCards:
                SetBlendedEulerAngles(new Vector3(40, 0, 0));
                break;
            case (int)ViewTypes.BoardOnly:
                SetBlendedEulerAngles(new Vector3(40, 0, 0));
                break;
            case (int)ViewTypes.Shop:
                SetBlendedEulerAngles(new Vector3(60, -30, 2));
                break;
        }

        for(int i = 0; i < (int)ViewTypes.NumOfViewTypes && i != (int)ViewTypes.Shop; i++)
        {
            camPositions[i].x = camPosition.x;
            camPositions[i].z = camPosition.z;
        }
    }

    //i have no idea what this means
    public void SetBlendedEulerAngles(Vector3 angles)
    {
        _targetRotation = Quaternion.Euler(angles);
    }
}
