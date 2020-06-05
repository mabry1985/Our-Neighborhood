using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent navAgent;
    public Camera cam;
    private Cinemachine.CinemachineBrain brain;
    public Cinemachine.CinemachineFreeLook vcam;

    public GameObject resourceOverlayCanvas;

    Health health;
    private bool freeCamToggle = false;
    private bool showResourceCanvas = true;

    private void Start()
    {
        health = GetComponent<Health>();
        navAgent = GetComponent<NavMeshAgent>();
        brain = cam.GetComponent<Cinemachine.CinemachineBrain>();
        vcam = brain.GetComponent<Cinemachine.CinemachineFreeLook>();
    }

    private void Update()
    {
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
            MakePlayersCold();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            navAgent.transform.GetComponentInParent<Player>().SitDown();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            navAgent.transform.GetComponentInParent<Player>().WaveHello();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            navAgent.transform.GetComponentInParent<Player>().Mining();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToggleFreeCam();
        }

        if (CombatInteraction()) return;
        if (MovementInteraction()) return;

    }

    private void ToggleFreeCam()
    {
        showResourceCanvas = !showResourceCanvas;
        freeCamToggle = !freeCamToggle;
    }

    private static void MakePlayersCold()
    {
        var pawns = GameObject.FindGameObjectsWithTag("Player");

        foreach (var pawn in pawns)
        {
            pawn.GetComponentInChildren<GAgent>().beliefs.ModifyState("isCold", 1);
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

            if (Input.GetMouseButton(1))
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
                this.GetComponent<Mover>().StartMoveAction(hit.point);
            }
            return true;
        }

        return false;
    }

    private Ray GetMouseRay()
    {
        return cam.ScreenPointToRay(Input.mousePosition);
    }

    private void LateUpdate()
    {
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