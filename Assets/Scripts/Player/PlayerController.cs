using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Camera cam;
    private Cinemachine.CinemachineBrain brain;
    public Cinemachine.CinemachineFreeLook vcam;

    public GameObject resourceOverlayCanvas;

    private bool freeCamToggle = false;
    private bool showResourceCanvas = true;

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
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            var pawns = GameObject.FindGameObjectsWithTag("Player");

            foreach (var pawn in pawns)
            {
            pawn.GetComponentInChildren<GAgent>().beliefs.ModifyState("isCold", 1);
            };
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            agent.transform.GetComponentInParent<Player>().SitDown();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            agent.transform.GetComponentInParent<Player>().WaveHello();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            agent.transform.GetComponentInParent<Player>().Mining();
        }

        if (Input.GetMouseButton(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && agent.enabled == true)
            {   
                agent.isStopped = true;
                agent.ResetPath();
                agent.SetDestination(hit.point);
                agent.isStopped = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {   
            showResourceCanvas = !showResourceCanvas;    
            freeCamToggle = !freeCamToggle;
        }
    }

    private void LateUpdate() {

        if (showResourceCanvas)
            resourceOverlayCanvas.SetActive(true);
        else
            resourceOverlayCanvas.SetActive(false);

        if (freeCamToggle)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            vcam.m_XAxis.m_InputAxisName = "Mouse X";
            vcam.m_YAxis.m_InputAxisName = "Mouse Y";
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            vcam.m_XAxis.m_InputAxisName = "";
            vcam.m_YAxis.m_InputAxisName = "";
        }
    }
    
}
