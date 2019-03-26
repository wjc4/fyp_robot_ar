using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelHandler : MonoBehaviour
{
    LogitechGSDK.LogiControllerPropertiesData properties;

    public float steeringAxes, gasAxes, brakeAxes;
    public bool simple;
    public bool wheel;
    public int steeringStatus, gasStatus, brakeStatus;

    private string actualState;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SteeringInit:" + LogitechGSDK.LogiSteeringInitialize(false));

        // LogitechGSDK.LogiPlaySpringForce(0, 50, 50, 50);
    }

    void OnApplicationQuit()
    {
        Debug.Log("SteeringShutdown:" + LogitechGSDK.LogiSteeringShutdown());
    }

    // Update is called once per frame
    void Update()
    {
        if (wheel)
        {
            WheelUpdate();
        }
        else
        {
            KeyboardUpdate();
        }
            

    }

    void WheelUpdate()
    {
        //All the test functions are called on the first device plugged in(index = 0)
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);
            steeringAxes = rec.lX;
            gasAxes = rec.lY;
            brakeAxes = rec.lRz;

            if (simple)
            {
                SimpleCalc();
            }
            else
            {
                ComplexCalc();
            }

            //Spring Force -> S
            if (Input.GetKeyUp(KeyCode.S))
            {
                if (LogitechGSDK.LogiIsPlaying(0, LogitechGSDK.LOGI_FORCE_SPRING))
                {
                    LogitechGSDK.LogiStopSpringForce(0);

                }
                else
                {
                    LogitechGSDK.LogiPlaySpringForce(0, 0, 24, 90);

                }
            }

        }
        else if (!LogitechGSDK.LogiIsConnected(0))
        {
            actualState = "PLEASE PLUG IN A STEERING WHEEL OR A FORCE FEEDBACK CONTROLLER";
        }
        else
        {
            actualState = "THIS WINDOW NEEDS TO BE IN FOREGROUND IN ORDER FOR THE SDK TO WORK PROPERLY";
        }
    }

    void KeyboardUpdate()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            steeringStatus = 0;
            gasStatus = 0;
            brakeStatus = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            steeringStatus = -1;
            gasStatus = 0;
            brakeStatus = 0;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            steeringStatus = 1;
            gasStatus = 0;
            brakeStatus = 0;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            steeringStatus = 0;
            gasStatus = 1;
            brakeStatus = 0;
        }
        else
        {
            steeringStatus = 0;
            gasStatus = 0;
            brakeStatus = 0;
        }
    }

    void SimpleCalc()
    {
        if (steeringAxes < -2000)
        {
            steeringStatus = -1;
        }
        else if (steeringAxes > 2000)
        {
            steeringStatus = 1;
        }
        else
        {
            steeringStatus = 0;
        }

        if (gasAxes < 30000)
        {
            gasStatus = 1;
        }
        else
        {
            gasStatus = 0;
        }

        if (brakeAxes < 30000)
        {
            brakeStatus = 1;
        }
        else
        {
            brakeStatus = 0;
        }
    }

    void ComplexCalc()
    {
        //1000-15000 trigger
        if (steeringAxes < 1000 && steeringAxes > 1000)
        {
            steeringStatus = 0;
        }
        else if (steeringAxes > 2000)
        {
            steeringStatus = 1;
        }
        else
        {
            steeringStatus = 0;
        }

        if (brakeAxes < 30000)
        {
            brakeStatus = 1;
        }
        else
        {
            brakeStatus = 0;
        }
    }
}
