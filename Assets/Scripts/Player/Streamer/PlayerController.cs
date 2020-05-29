using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Camera cam;
    private Cinemachine.CinemachineBrain brain;
    public Cinemachine.CinemachineFreeLook vcam;

    Animator animator;

    public GameObject resourceOverlayCanvas;

    private float rotSpeed = 20f;

    private bool freeCamToggle = false;
    private bool showResourceCanvase = true;

    private bool isStanding = true;

    private void Start() {
        brain = cam.GetComponent<Cinemachine.CinemachineBrain>();
        vcam = brain.GetComponent<Cinemachine.CinemachineFreeLook>();
        animator = agent.GetComponentInChildren<Animator>();
        agent.updateRotation = false;
    }

    private void Update()
    {
        
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            var pawns = GameObject.FindGameObjectsWithTag("Player");
            
            foreach (var pawn in pawns)
            {
                var n = Random.Range(0, 100);
                var player = pawn.GetComponent<Player>();
                if (pawn != null && !player.isDead)
                    player.OnDeath(); 
            };
            //pawn.GetComponentInChildren<GAgent>().beliefs.ModifyState("inDanger", 1);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {   
            if(isStanding)
            {
                //animator.SetFloat("speedPercent", 0.0f);
                animator.SetBool("isSittingGround", true);
                animator.SetBool("isStanding", false);
                agent.enabled = false;
                isStanding = false;
            }
            else
            {
                animator.SetBool("isSittingGround", false);
                animator.SetBool("isStanding", true);
                isStanding = true;
            }

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
