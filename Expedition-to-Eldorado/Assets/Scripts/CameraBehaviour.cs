using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralEnumerations;

public class CameraBehaviour : MonoBehaviour
{
    int viewIndex;
    List<int> views;
    Vector3[] camPositions = new Vector3[4];

    // Start is called before the first frame update
    void Start()
    {
        views.Add((int)ViewTypes.BoardOnly);
        views.Add((int)ViewTypes.CardsOnly);
        views.Add((int)ViewTypes.BoardCards);
        views.Add((int)ViewTypes.Shop);

        camPositions[0].x = 0;
        camPositions[0].y = 0;
        camPositions[0].z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        handlePlayerInput();


    }

    private void handlePlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(viewIndex < views.Count)
            {
                viewIndex++;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (viewIndex > 0)
            {
                viewIndex--;
            }
        }
    }
}
