using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Camera cam;
    private Cinemachine.CinemachineBrain brain;
    public Cinemachine.CinemachineFreeLook vcam;    

    public GameObject resourceOverlayCanvas;

    private float rotSpeed = 20f;

    private bool freeCamToggle = false;
    private bool showResourceCanvase = true;

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
            var pawn = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            if (pawn != null && !pawn.isDead)
                pawn.OnDeath();
            //pawn.GetComponentInChildren<GAgent>().beliefs.ModifyState("inDanger", 1);
        }

        if (Input.GetMouseButton(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

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
            showResourceCanvase = !showResourceCanvase;    
            freeCamToggle = !freeCamToggle;
        }
    }

    private void LateUpdate() {

        if (showResourceCanvase)
            resourceOverlayCanvas.SetActive(true);
        else
            resourceOverlayCanvas.SetActive(false);

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
    
}
