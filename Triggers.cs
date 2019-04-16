using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour
{
	public GameObject DObjBox;
	public DialogueBase dialogue;
	public DialogueBase dia_3_yarns_left;
	public DialogueBase dia_2_yarns_left;
	public DialogueBase dia_1_yarn_left;
	public DialogueBase dia_end;
	private float delayTime;
	private bool dialogueOK = true;

	// Triggers the dialogue when called
	public void TriggerDialogue()
	{
		DialogueManager.instance.EnqueueDialogue(dialogue);
	}

	//Displays the next line of dialogue
	public void GetNextLine()
	{
		DialogueManager.instance.DisplayNextLine();
	}
	public void triggerNextDialogue()
	{
		if(LevelManager.instance.amountGiven == 0) {
			DialogueManager.instance.EnqueueDialogue(dia_3_yarns_left);
		}
		else if(LevelManager.instance.amountGiven == 1) {
			DialogueManager.instance.EnqueueDialogue(dia_2_yarns_left);
		}
		else if(LevelManager.instance.amountGiven == 2) {
			DialogueManager.instance.EnqueueDialogue(dia_1_yarn_left);
		}
		else {
			DialogueManager.instance.EnqueueDialogue(dia_end);
		}
	}
	//If a player is within range of an NPC, the z key is pressed and there
	//has been a short delay between dialogue lines, display the next dialogue line
	void OnTriggerStay(Collider c)
	{
		ResetDialogue();

		if (c.tag == "Player" && Input.GetKeyDown(KeyCode.Z) && dialogueOK)
		{
			delayTime = 1f;
			dialogueOK = false;

			//If we are in the middle of a dialogue, display the next line
			if (DObjBox.activeSelf)
			{
				GetNextLine();
			}
			//Else trigger the dialogue
			else if (!DObjBox.activeSelf && !LevelManager.instance.questIsComplete && !LevelManager.instance.questIsActive)
			{
				TriggerDialogue();
			}
			else if (!DObjBox.activeSelf && (LevelManager.instance.questIsComplete ^ LevelManager.instance.questIsActive))
			{
				triggerNextDialogue();
			}

		}
	}

	//Implements wait time between dialogue lines so user can not spam 'z' key
	//Implemented to fix bug where tapping 'z' too fast made dialogue load in too quickly,
	//resulting in out of order typing and duplicate lines
	private void ResetDialogue()
	{
		if(!dialogueOK && delayTime > 0)
		{
			delayTime -= Time.deltaTime;
		}

		if (delayTime < 0)
		{
			delayTime = 0;
			dialogueOK = true;
		}
	}
}
