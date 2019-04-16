using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
	public GameObject ObjBox;
	public DialogueBase dialogue;
	public QuestBase quest;

	// Triggers the dialogue when called
	public void TriggerQuest()
	{
		if (DialogueManager.instance.inDialogue == true)
		{
			quest.isActive = true;	
		}
	}
}