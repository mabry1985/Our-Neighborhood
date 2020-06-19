using UnityEngine;
using UnityEngine.AI;

public class StreamerController : MonoBehaviour
{
    private CursorManager cursorManager;
    private NavMeshAgent navAgent;
    public Camera cam;
    private Cinemachine.CinemachineBrain brain;
    public Cinemachine.CinemachineFreeLook vcam;

    public GameObject resourceOverlayCanvas;

    Health health;
    private bool freeCamToggle = false;
    private bool showResourceCanvas = true;
    private bool isSprinting = false;

    private void Start()
    {
        health = GetComponent<Health>();
        navAgent = GetComponent<NavMeshAgent>();
        brain = cam.GetComponent<Cinemachine.CinemachineBrain>();
        vcam = brain.GetComponent<Cinemachine.CinemachineFreeLook>();

        cursorManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>().cursorManager;
    }

    private void Update()
    {
        if (isSprinting)
        {
            navAgent.speed = 20f;
        }
        else
        {
            navAgent.speed = 10f;
        }

        if (health.IsDead())
        {
            
            return;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            ThanosSnap();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            MakePlayersScared();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            navAgent.transform.GetComponentInParent<AnimController>().SitDown();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            navAgent.transform.GetComponentInParent<AnimController>().WaveHello();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            navAgent.transform.GetComponentInParent<AnimController>().Mining();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetMouseButtonDown(2))
        {
            ToggleFreeCam();
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = !isSprinting;
        }

        if (CombatInteraction()) return;
        if (MovementInteraction()) return;

    }

    private void LateUpdate()
    {
        if (showResourceCanvas)
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

    private void ToggleFreeCam()
    {
        cursorManager.cursorVisible = freeCamToggle;
        showResourceCanvas = !showResourceCanvas;
        freeCamToggle = !freeCamToggle;
    }

    private void MakePlayersScared()
    {
        var pawns = GameObject.FindGameObjectsWithTag("Player");

        foreach (var pawn in pawns)
        {
            if(pawn.gameObject != this.gameObject) 
                pawn.GetComponent<ChatPlayerController>().InDanger();
        };
    }

    private static void ThanosSnap()
    {
        var pawns = GameObject.FindGameObjectsWithTag("Player");

        foreach (var pawn in pawns)
        {
            var n = Random.Range(0, 100);
            var health = pawn.GetComponent<Health>();
            if (pawn != null && !health.IsDead() && n > 50)
                health.Die();
        };
    }

    private bool CombatInteraction()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        foreach (RaycastHit hit in hits)
        {
            CombatTarget target = hit.transform.GetComponent<CombatTarget>();
            if (target == null) continue;

            if (!GetComponent<Fighter>().CanAttack(target.gameObject))
            {
                continue;
            }

            if (Input.GetMouseButton(0))
            {
                GetComponent<Fighter>().Attack(target.gameObject);
            }

            return true;
        }

        return false;
    }

    private bool MovementInteraction()
    {
        Ray ray = GetMouseRay();
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);

        if (hasHit && navAgent.enabled == true)
        {
            if (Input.GetMouseButton(1))
            {
                this.MoveTo(hit.point);
            }
            return true;
        }

        return false;
    }

    private Ray GetMouseRay()
    {
        return cam.ScreenPointToRay(Input.mousePosition);
    }

    public void MoveTo(Vector3 destination)
    {
        navAgent.isStopped = false;
        navAgent.SetDestination(destination);
        //navAgent.u
    }


}