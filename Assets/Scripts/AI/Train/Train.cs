using PathCreation;
using UnityEngine;
using System.Collections;

public class Train : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 25;
    public bool isMoving = false;
    public bool enteringStation = false;
    public bool leavingStation = false;

    private float distanceTravelled;
    private float originalSpeed;
    
    void Start()
    {
        originalSpeed = speed;
    }

    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);        
        
        if (enteringStation == true) 
        {
            speed = Mathf.Lerp(speed, 0, Time.deltaTime);
        }

        if(leavingStation == true) 
        {
            speed = Mathf.Lerp(speed, originalSpeed / 1.75f, Time.deltaTime);
        }

        if (Input.GetKeyDown("space"))
        {
            enteringStation = true;
            //EnterStation();
        }  

        if (speed < 0.3f) {
            ExitStation();
        }

    }

    public void EnterStation() {
        this.enteringStation = true;
        this.leavingStation = false;
    }

    public void ExitStation() {
        this.enteringStation = false;
        this.leavingStation = true;
    }
    // public IEnumerator ExitStation() {
    //     this.enteringStation = false;
    //     speed = 0;
    //     yield return new WaitForSeconds(3);
    //     this.leavingStation = true;
    // }

    private void OnTriggerEnter(Collider other)
    {
        print("in train script" + other.gameObject);
        switch(other.gameObject.tag){
            case "Enter Station Trigger":
                EnterStation();
                break;
            case "Exit Station Trigger":
                ExitStation();
                break;
            default:
                break;
        }
    }

}

// using PathCreation;
// using UnityEngine;
// using System.Collections;

// public class Train : MonoBehaviour
// {
//     public PathCreator pathCreator;
//     public float speed = 25;
//     public bool isMoving = true;
//     public bool enteringStation = false;
//     public bool leavingStation = false;

//     private float distanceTravelled;
//     private float originalSpeed;

//     void Start()
//     {
//         originalSpeed = speed;
//     }

//     void Update()
//     {
//         if (isMoving)
//         {
//             distanceTravelled += speed * Time.deltaTime;
//             transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
//             transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
//         }
//         if (enteringStation == true)
//         {
//             speed = Mathf.Lerp(speed, 0, Time.deltaTime);
//         }

//         if (leavingStation == true)
//         {
//             speed = Mathf.Lerp(0, originalSpeed, Time.deltaTime);
//         }

//         // if (speed <= .5f) 
//         // {
//         //     StartCoroutine(ExitStation());
//         // }



//     }

//     private void LateUpdate()
//     {
//     }

//     public void EnterStation()
//     {
//         this.enteringStation = true;
//         this.leavingStation = false;
//     }

//     public IEnumerator ExitStation()
//     {
//         this.isMoving = false;
//         yield return new WaitForSeconds(2);
//         this.isMoving = true;
//         this.enteringStation = false;
//         this.leavingStation = true;
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         print("in train script" + other.gameObject);
//         switch (other.gameObject.tag)
//         {
//             case "Enter Station Trigger":
//                 EnterStation();
//                 break;
//             case "Exit Station Trigger":
//                 ExitStation();
//                 break;
//             default:
//                 break;
//         }
//     }

// }