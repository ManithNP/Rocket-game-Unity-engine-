kkusing System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCntrl : MonoBehaviour
{   
    public Rigidbody rocket;

    public Rigidbody boll1;

    public Rigidbody boll2;

    public Rigidbody boll3;

    public Rigidbody lastCap;

    public float force = 10000f;

    public float rotSpeed = 70f;

    public float fuel = 300f;

    public TMPro.TextMeshProUGUI fuelDisply;

    public float lowFuel = 0.5f;

    public float maxFuel;

    private Vector3 startPosition;
     
    private Vector3 boll1StartPosition;

    private Vector3 boll2StartPosition;

    private Vector3 boll3StartPosition;

    private Quaternion startRotation;

    


    
    void Start()
    {
        startPosition =rocket.position;
        boll1StartPosition = boll1.position;
        boll2StartPosition = boll2.position;
        boll3StartPosition = boll3.position;
        startRotation = rocket.rotation;

        Physics.gravity = new Vector3(0,-2.0f,0);

        
    }

    
     void Update()
     {
       keyControls();
       frameSize();
       fuelControl();
       UpdateFuel();
       gameEnd();
        
    }

    void keyControls()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            rocket.AddForce(transform.up * force *Time.deltaTime );
            fuel -= lowFuel*(Time.deltaTime);
            
        }
        else
        {
            if(rocket.velocity.y > 0)
            {
                rocket.velocity = Vector3.zero;
                rocket.angularVelocity =Vector3.zero;
            }
        }

        if(Input.GetKey(KeyCode.DownArrow))
        {
            rocket.AddForce(-transform.up * force *Time.deltaTime);
            fuel -= lowFuel*(Time.deltaTime);
            
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0,0,rotSpeed *Time.deltaTime,Space.Self);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0,0,-rotSpeed*Time.deltaTime,Space.Self);
        }

        if(Input.GetKey(KeyCode.R)) 
        {
            Reset();
        }




    }

    
        void frameSize()
    {
        if(transform.position.y >= 34f) 
        {
            transform.position = new Vector3(transform.position.x, 34f, transform.position.z);
        }

        if(transform.position.y <= 4f) 
        {
            transform.position = new Vector3(transform.position.x, 4f, transform.position.z);
        }

        if(transform.position.x >= 17f) 
        {
            transform.position = new Vector3(17f, transform.position.y, transform.position.z);
            
        }

        if(transform.position.x <= 0.54f) 
        {
            transform.position = new Vector3(0.54f, transform.position.y, transform.position.z);
            
        }
    }

        void fuelControl()
    {
        if (lastCap.GetComponent<FixedJoint>() != null) 
        {
         
            if (lastCap.GetComponent<FixedJoint>().connectedBody.name == "Boll1")
            {
                lowFuel = 3f; 
            }
            if (lastCap.GetComponent<FixedJoint>().connectedBody.name == "Boll2")
            {
                lowFuel = 5f; 
            }
            if (lastCap.GetComponent<FixedJoint>().connectedBody.name == "Boll3")
            {
                lowFuel = 1f; 
            }
        }
        else 
        {
           lowFuel = 0.5f;
        }

        if(fuel <= 0)
        {
            Reset();
        }


    }


      void UpdateFuel()
    {
        
        // int fuelPercentage = (int) (fuel / maxFuel * 100);
        fuelDisply.text = (fuel.ToString() + " L");
    }

    void gameEnd()
    {
        if (boll1.GetComponent<FixedJoint>() != null && boll2.GetComponent<FixedJoint>() != null && boll3.GetComponent<FixedJoint>() != null)
        {
           UnityEditor.EditorApplication.isPlaying = false;
        }
    }
    
    public void UpdateObstacle()
    {
        fuel -=5;

        if (transform.rotation.eulerAngles.z > 180) 
        {
            transform.position = new Vector3(transform.position.x -1, transform.position.y, transform.position.z);
        }
        else 
        {
            transform.position = new Vector3(transform.position.x +1, transform.position.y, transform.position.z);
        }
        rocket.velocity = new Vector3(0,0,0);
        rocket.angularVelocity = new Vector3(0,0,0);
    }

     void Reset() 
    {
        Destroy(lastCap.GetComponent<FixedJoint>());
        Destroy(boll1.GetComponent<FixedJoint>());
        Destroy(boll2.GetComponent<FixedJoint>());
        Destroy(boll3.GetComponent<FixedJoint>());

        boll1.position =boll1StartPosition;
        boll1.velocity =new Vector3(0,0,0);
        boll1.angularVelocity =new Vector3(0,0,0);

        boll2.position = boll2StartPosition;
        boll2.velocity = Vector3.zero;
        boll2.angularVelocity =new Vector3(0,0,0);

        boll3.position =boll3StartPosition;
        boll3.velocity = Vector3.zero;
        boll3.angularVelocity =new Vector3(0,0,0);

        transform.position = startPosition;
        transform.rotation = startRotation;
        rocket.velocity =new Vector3(0,0,0);
        rocket.angularVelocity =new Vector3(0,0,0);
        fuel = 300f;
        
    }
}
