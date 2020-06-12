using UnityEngine;
using System.Collections;
using SBLS;

public class GlowingQuestController : MonoBehaviour {
	public SBLSQuestConfiguration questConfig;
	public SBLSCharacter player;

	void OnTriggerEnter(Collider col) {
		if (col.name == "Player") {
			player = col.gameObject.GetComponent<SBLSCharacter>();

			// We only want to update the quest if it has started, and we're on the first step
			if (player.findQuest(questConfig.quest.getName()).started && player.findQuest(questConfig.quest.getName()).currentStep == 0) {
				player.questStepCompleted(questConfig.quest.getName ());
				gameObject.SetActive (false);
			}
		}
	}
}
