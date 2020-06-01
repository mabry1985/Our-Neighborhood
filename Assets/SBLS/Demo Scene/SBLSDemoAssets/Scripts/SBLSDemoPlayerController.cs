using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SBLS;
public class SBLSDemoPlayerController : MonoBehaviour {
	public Camera camera;
	public Text crossHairText;
	public SBLSDemoUIController uiController;

	private RaycastHit hit;
	private NPC npc;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		crossHairText.text = "";
		int itemLayer = 1 << 9; // The NPC layer

		Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
		if (Physics.Raycast (ray, out hit, 10, itemLayer)) {
			checkRay (hit);
		} else {
			npc = null;
		}

		if (npc != null) {
			crossHairText.text = npc.GetCrosshairText ();
			if (Input.GetMouseButtonUp(0)) {
				npc.CheckQuest ();
			}
		}

	}

	void checkRay(RaycastHit h) {
		if (h.collider.name == "NPC") {
			npc = h.collider.GetComponent<NPC>();

		}
	}

	// This is called when a quest is started
	public void questStarted(SBLSQuest quest) {
		uiController.questStarted(quest);
	}

	// This is called when a quest is updated
	void questUpdated(SBLSQuest quest) {
		uiController.questUpdated (quest);
	}

	// This is called when a quest is completed
	void questCompleted(SBLSQuest quest) {
		uiController.questCompleted (quest);
	}
}
