using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SBLS;


// This is a simple NPC class designed specifically for the demo.  Don't have time to implement a full dialog system, nor is it in the scope of the project
public class NPC : MonoBehaviour {
	public string characterName;
	public SBLSCharacter pc;
	public SBLSQuestConfiguration questConfig;
	public AudioClip mouseClickClip;
	public GameObject questObject;

	private SBLSQuest quest;

	public void CheckQuest()
	{
		if (quest == null) {
			// Activate the quest
			pc.startQuest (questConfig);
			questObject.SetActive (true);
			// Set the local quest object to the quest
			quest = pc.findQuest(questConfig.quest.getName());
		} else {
			if (quest.started && !quest.complete) {
				if (quest.currentStep == 1) {
					pc.findQuest (questConfig.quest.getName ()).updateQuest ();
				}
			}
		}
	}

	public string GetCrosshairText()
	{
		if (quest != null) {
			if (quest.started) {
				if (!quest.complete) {
					if (quest.currentStep == 0) {
						return "Go visit the glowing thing";
					}

					if (quest.currentStep == 1) {
						return "Complete Quest";
					}
				}
			}

			if (quest.complete) {
				return "You did it!";
			}
		} else {
			return "Get Quest";
		}

		return "Hi, I have nothing for you to do.";
	}
}
