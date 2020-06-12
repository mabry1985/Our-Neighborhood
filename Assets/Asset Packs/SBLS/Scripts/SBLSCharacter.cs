using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SBLS {
	[System.Serializable]
	public class SBLSCharacter : MonoBehaviour {
		/// <summary>
		/// Should we use the default SBLS configuration file?
		/// </summary>
		public bool useDefaultConfig = true;

		/// <summary>
		/// Custom SBLS configuration file
		/// </summary>
		public SBLSConfiguration customConfig;

		/// <summary>
		/// Characters experience
		/// </summary>
		public int xp;

		/// <summary>
		/// XP required to get to the next level
		/// </summary>
		public int nextXp;

		/// <summary>
		/// The characters total XP
		/// </summary>
		public int totalXp;

		/// <summary>
		/// The characters current level
		/// </summary>
		public int level = 1;

		/// <summary>
		/// The characters skill set
		/// </summary>
		public List<SBLSSkill> skills = new List<SBLSSkill>();

		/// <summary>
		/// The characters levels if manually set
		/// </summary>
		public List<SBLSLevel> levels = new List<SBLSLevel>();

		/// <summary>
		/// A list of quests the character has been assigned.
		/// </summary>
		public List<SBLSQuest> quests = new List<SBLSQuest>();

		/// <summary>
		/// Use the Unity SendMessage broadcast instead of events (for legacy games)
		/// </summary>
		public bool useSendMessage = false;

		/// <summary>
		/// The xp level adjustment.
		/// </summary>
		[SerializeField]
		private int xpLevelAdjustment;

		/// <summary>
		/// A static reference to the SBLS Config
		/// </summary>
		private static SBLSConfiguration config;

		/// <summary>
		/// For looping
		/// </summary>
		private int i = 0;

		void Awake() {
			// Try to load config
			if (useDefaultConfig) {
				config = SBLSUtils.FindDefault();
			} else {
				config = customConfig;
			}

			// If the config was found, let's process it
			if (config) {
				skills = config.skills;
				levels = config.levels;


			} else {
				// Ooops.... Someone didn't do something correctly
				if (useDefaultConfig) {
					throw new UnityException("SBSL: There is no default configuration set.  Make sure you set one as default");
				} else {
					throw new UnityException ("SBSL: Character is set to use custom configuration, but none is set.");
				}
			}

			if (config.useLevelMultiplier) {
				nextXp = level * config.firstLevelXp;
			} else {
				nextXp = levels[level].xpToReachLevel;
			}
		}

		// Use this for initialization
		void Start () {
			for (i = 0; i < skills.Count; i++) {
				skills[i].setCharacter(this);
				skills[i].setLevel(1);
				skills[i].setNextXp(skills[i].levels[skills[i].level].xpToReachLevel);
			}
		}
		
		// Update is called once per frame
		void Update () {
			// Loop through skills to see if we need to update the time
			for (i = 0; i < skills.Count; i++) {
				if (skills[i].isTimeBased && skills[i].inUse) {
					skills[i].updateTime(Time.deltaTime);
				}
			}
		}

		/// <summary>
		/// Sets the xp.
		/// </summary>
		/// <param name="newXp">New xp.</param>
		public void setXp(int newXp) {
			xp = newXp;
			updateLevel ();
		}

		/// <summary>
		/// Adjusts the xp.
		/// </summary>
		/// <param name="xpAdjustment">Xp adjustment.</param>
		public void adjustXp(int xpAdjustment) {
			xp += xpAdjustment;
			totalXp += xpAdjustment;
			updateLevel ();
		}

		/// <summary>
		/// Gets the xp.
		/// </summary>
		/// <returns>The xp.</returns>
		public int getXp() {
			return xp;
		}

		/// <summary>
		/// Sets the next xp.
		/// </summary>
		/// <param name="newNextXp">New next xp.</param>
		public void setNextXp(int newNextXp) {
			nextXp = newNextXp;
			updateLevel ();
		}

		/// <summary>
		/// Gets the next xp.
		/// </summary>
		/// <returns>The next xp.</returns>
		public int getNextXp() {
			return nextXp;
		}

		/// <summary>
		/// Gets the skill.
		/// </summary>
		/// <returns>The skill.</returns>
		/// <param name="skillNo">Skill no.</param>
		public SBLSSkill getSkill(int skillNo) {
			return skills [skillNo];
		}

		/// <summary>
		/// Updates the skill.
		/// </summary>
		/// <param name="skillNo">Skill no.</param>
		/// <param name="xpAdjustment">Xp adjustment.</param>
		public void updateSkill(int skillNo, int xpAdjustment) {
			skills [skillNo].adjustXp (xpAdjustment);
		}

		/// <summary>
		/// Gets the skill.
		/// </summary>
		/// <returns>The skill.</returns>
		/// <param name="skillName">Skill name.</param>
		public SBLSSkill getSkill(string skillName) {
			return skills.Find (i => i.getName () == skillName);;
		}

		/// <summary>
		/// Updates the skill.
		/// </summary>
		/// <param name="skillName">Skill name.</param>
		/// <param name="xpAdjustment">Xp adjustment.</param>
		public void updateSkill(string skillName, int xpAdjustment) {
			var sk = skills.Find (i => i.getName () == skillName);
			sk.adjustXp (xpAdjustment);
		}

		/// <summary>
		/// Updates the level.
		/// </summary>
		private void updateLevel() {
			if (xp >= nextXp) {
				if (level != config.levelLimit) {
					level++;


					setXp (xp - nextXp);
					if (config.useLevelMultiplier) {
						float next = nextXp * config.multiplier;
						setNextXp((int)next);
					} else {
						setNextXp(levels[getLevel()].xpToReachLevel);
					}
						
					sendEvent("levelUpdated", this);
				}
			}
		}

		/// <summary>
		/// Sets the level.
		/// </summary>
		/// <param name="newLevel">New level.</param>
		public void setLevel(int newLevel) {
			level = newLevel;
		}

		/// <summary>
		/// Gets the level.
		/// </summary>
		/// <returns>The level.</returns>
		public int getLevel() {
			return level;
		}

		/// <summary>
		/// Gets the config.
		/// </summary>
		/// <returns>The config.</returns>
		public SBLSConfiguration getConfig() {
			return config;
		}

		/// <summary>
		/// Sends the skill updated message
		/// </summary>
		/// <param name="sk">Sk.</param>
		public void characterSkillUpdated(SBLSSkill sk) {
			sendEvent ("skillUpdated", sk);
		}

		/// <summary>
		/// Gets the progress.
		/// </summary>
		/// <returns>The progress.</returns>
		/// <param name="width">Width.</param>
		public int getProgress(int width) {
			float progress = (float)getXp () / (float)getNextXp () * width;
			
			return (int)progress;
		}

		/// <summary>
		/// Gets the level progress as a float between 0 and 1.
		/// </summary>
		/// <returns>The progress.</returns>
		public float getProgress() {
			return (float)getXp () / (float)getNextXp ();
		}

		/// <summary>
		/// Reset this instance.
		/// </summary>
		public void reset() {
			xp = 0;
			level = 1;
			if (config.useLevelMultiplier) {
				nextXp = level * config.firstLevelXp;
			} else {
				nextXp = levels[level].xpToReachLevel;
			}


			foreach (SBLSSkill skill in skills) {
				skill.setLevel(1);
				skill.setXp (0);
				skill.setNextXp(skill.levels[skill.level].xpToReachLevel);
				skill.setTimeUsed(0.0f);
				skill.setTotalTimeUsed(0.0f);
			}	
		}


		/// <summary>
		/// Starts the quest.
		/// </summary>
		/// <param name="questConfig">Quest config.</param>
		public void startQuest(SBLSQuestConfiguration questConfig) {
			SBLSQuest quest = questConfig.quest;

			//#if UNITY_EDITOR

				quest.started = false;
				quest.complete = false;
				quest.currentStep = 0;
				
				foreach (SBLSQuestStep qs in quest.steps) {
					qs.completed = false;
					qs.active = false;
				}
			//#endif

			// We don't want to add it if the quest is already there
			if (findQuest (quest.getName ()) == null) {
				quest.activateQuest ();
				quest.character = this;
				quest.currentStep = 0;
				quests.Add (quest);

				sendEvent ("questStarted", quest);
			} else {
				Debug.Log ("Quest \""+ quest.getName() +"\" has already been started");
			}
		}

		/// <summary>
		/// Finds a quest.
		/// </summary>
		/// <returns>The quest.</returns>
		/// <param name="questName">Quest name.</param>
		public SBLSQuest findQuest(string questName) {
				return quests.Find (i => i.getName () == questName);
		}

		/// <summary>
		/// Gets the active quests.
		/// </summary>
		/// <returns>The active quests.</returns>
		public List<SBLSQuest> getActiveQuests() {
			return quests.FindAll(i => !i.complete && i.started);
		}

		/// <summary>
		/// Gets the completed quests.
		/// </summary>
		/// <returns>The completed quests.</returns>
		public List<SBLSQuest> getCompletedQuests() {
			return quests.FindAll ( i => i.complete);
		}

		/// <summary>
		/// Sends the quest step completed event
		/// </summary>
		/// <param name="questName">Quest name.</param>
		public void questStepCompleted(string questName) {
			SBLSQuest q = findQuest (questName);

			if (q != null) {
				q.updateQuest ();
				sendEvent ("questUpdated", q);
			}
		}


		/// <summary>
		/// Completes a quest
		/// </summary>
		/// <param name="q">Q.</param>
		public void questIsDone(SBLSQuest q) {
			sendEvent ("questCompleted", q);
		}

		/// <summary>
		/// Sends the event using SendMessage.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		private void sendEvent(string name, object value = null) {
			if (useSendMessage) {
				SendMessage (name, value, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

}