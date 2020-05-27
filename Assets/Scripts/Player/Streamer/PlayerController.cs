using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Camera cam;
    private Cinemachine.CinemachineBrain brain;
    private Cinemachine.CinemachineFreeLook vcam;    

    private float rotSpeed = 20f;

    private bool freeCamToggle = false;

    private void Start() {
        brain = cam.GetComponent<Cinemachine.CinemachineBrain>();
        vcam = brain.GetComponent<Cinemachine.CinemachineFreeLook>();
        agent.updateRotation = false;
    }

    private void Update()
    {
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            var pawn = GameObject.FindGameObjectWithTag("Player");
            pawn.GetComponentInChildren<GAgent>().beliefs.ModifyState("inDanger", 1);
        }

        if (Input.GetMouseButton(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("RMB pressed");

            if (Physics.Raycast(ray, out hit))
            {   agent.isStopped = true;
                agent.ResetPath();
                agent.SetDestination(hit.point);
                agent.isStopped = false;
            }
            else
            {
                Debug.Log("No mesh found");
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            freeCamToggle = !freeCamToggle;
        }
    }

    private void LateUpdate() {
        if (freeCamToggle)
        {
            Cursor.lockState = CursorLockMode.Locked;
            vcam.m_XAxis.m_InputAxisName = "Mouse X";
            vcam.m_YAxis.m_InputAxisName = "Mouse Y";
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            vcam.m_XAxis.m_InputAxisName = "";
            vcam.m_YAxis.m_InputAxisName = "";
        }
    }
    
    private void InstantlyTurn(Vector3 destination)
    {
        //When on target -> dont rotate!
        if ((destination - transform.position).magnitude < 0.1f) return;

        Vector3 direction = (destination - transform.position).normalized;
        Quaternion qDir = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * rotSpeed);
    }
}
