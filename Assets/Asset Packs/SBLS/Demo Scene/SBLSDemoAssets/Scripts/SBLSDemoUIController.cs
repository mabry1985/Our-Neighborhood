using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SBLS;


public class SBLSDemoUIController : MonoBehaviour {
	[Header("Jumping Skill")]
	public Image jumpingProgress;
	public Text jumpingProgressCount;

	[Header("Running Skill")]
	public Image runningProgress;
	public Text runningProgressCount;

	[Header("Overall Level")]
	public Image overallProgress;
	public Text overallProgressCount;

	[Header("SBLS Character")]
	public SBLSCharacter player;

	[Header("Quest UI")]
	public Image questHolder;
	public Sprite questStartedImage;
	public Sprite questUpdatedImage;
	public Sprite questCompleteImage;
	public Text questText;

	private SBLSSkill jumpingSkill;
	private SBLSSkill runningSkill;
	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		// This resets the character back to zero
		player.reset ();

		// We get the skills here so we don't have to keep calling getSkill
		jumpingSkill = player.getSkill ("Jumping");
		runningSkill = player.getSkill ("Running");
	}
	
	// Update is called once per frame
	void Update () {
		if (jumpingProgress.fillAmount <= jumpingSkill.getProgress ()) {
			jumpingProgress.fillAmount += 0.01f;
		} else if (jumpingProgress.fillAmount > jumpingSkill.getProgress()) {
			jumpingProgress.fillAmount = jumpingSkill.getProgress();
		}

		jumpingProgressCount.text = "Level "+ jumpingSkill.getLevel() + " (" + jumpingSkill.getXp () + "/" + jumpingSkill.getNextXp () + ")";

		if (runningProgress.fillAmount <= runningSkill.getProgress ()) {
			runningProgress.fillAmount += 0.01f;
		} else if (runningProgress.fillAmount > runningSkill.getProgress()) {
			runningProgress.fillAmount = runningSkill.getProgress();
		}

		runningProgressCount.text = "Level "+ runningSkill.getLevel() + " (" + runningSkill.getXp () + "/" + runningSkill.getNextXp () + ")";

		if (overallProgress.fillAmount <= player.getProgress ()) {
			overallProgress.fillAmount += 0.01f;
		} else if (overallProgress.fillAmount > player.getProgress()) {
			overallProgress.fillAmount = player.getProgress();
		}

		overallProgressCount.text = "Level "+ player.getLevel() + " (" + player.getXp () + "/" + player.getNextXp () + ")";

		foreach (SBLSQuest q in player.getActiveQuests()) {
			questText.text = q.getName() +"\n\t<color=#00ff00>"+ q.getCurrentStep().stepName +"</color>";
		}

	}

	// This is called when a quest is started
	public void questStarted(SBLSQuest quest) {
		questHolder.sprite = questStartedImage;
		questHolder.transform.localPosition = new Vector3 (0, 362, 0);

		CancelInvoke ("hideQuestHolder");
		Invoke ("hideQuestHolder", 3.0f);
	}

	// This is called when a quest is updated
	public void questUpdated(SBLSQuest quest) {
		Debug.Log ("Quest Updated: " + quest.getName ());
		questHolder.sprite = questUpdatedImage;
		questHolder.transform.localPosition = new Vector3 (0, 362, 0);

		CancelInvoke ("hideQuestHolder");
		Invoke ("hideQuestHolder", 3.0f);
	}

	// This is called when a quest is completed
	public void questCompleted(SBLSQuest quest) {

		questHolder.sprite = questCompleteImage;
		questHolder.transform.localPosition = new Vector3 (0, 362, 0);

		CancelInvoke ("hideQuestHolder");
		Invoke ("hideQuestHolder", 3.0f);
	}

	void hideQuestHolder() {
		questHolder.transform.localPosition = new Vector3 (0, 600, 0);
	}

}
