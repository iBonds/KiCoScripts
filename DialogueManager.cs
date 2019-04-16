using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	//Singleton pattern, ensures only one instance of manager
	public static DialogueManager instance;
	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("Already running instance!");
		}
		else 
		{
			instance = this;
		}
	}

	//Name and text to reference
	public GameObject DObjBox;
	public Text dialogueName;
	public Text dialogueText;
	public Text Continue;
	public float delay = 0.001f;
	public bool inDialogue;

	private bool currentlyTyping;
	private string completeLine;

	//Queue that holds all dialogue information to be displayed
	public Queue<DialogueBase.Info> dialogueInfo; //FIFO Collection
    
	//Intializes queue
	private void Start()
	{
		dialogueInfo = new Queue<DialogueBase.Info>();
	}

	//Starts the dialogue in the Trigger script field
	public void EnqueueDialogue(DialogueBase db)
	{
		if (inDialogue)
		{
			return;
		}
		inDialogue = true;

		//Sets dialogue box active and inDialogue to true
		DObjBox.SetActive(true);

		//Clears queue to ensure there is nothing left over from previous dialogue
		dialogueInfo.Clear();

		//Loop to fill the queue with the information from the dialogue object
		foreach(DialogueBase.Info info in db.dialogueInfo)
		{
			dialogueInfo.Enqueue(info);
		}
		//Displays the next line of dialogue
		DisplayNextLine();
	}

	public void DisplayNextLine()
	{
		//If the dialogue is over, disable dialogue box and return
		if (dialogueInfo.Count == 0)
		{
			EndDialogue();
			return;
		}

		//If this is  the last line of dialogue, should display end of dialogue
		//Else, display continue text
		if (dialogueInfo.Count == 1)
		{
			Continue.text = "End of Dialogue";
		}
		else
		{
			Continue.text = "Press Z to Continue";
		}
		//If currently typing out the text and user presses Z, should fill text box and stop typing coroutine
		if (currentlyTyping)
		{
			CompleteText();
			StopAllCoroutines();
			currentlyTyping = false;
			return;
		}
			
		DialogueBase.Info info = dialogueInfo.Dequeue(); //Get current line and dequeue it from the dialogue queue
		completeLine = info.myText;						//Get complete line and store it in case user wants to skip typing

		//Set dialogue box info to name and text from queue
		dialogueName.text = info.Name;
		dialogueText.text = info.myText;

		//Clears the dialogue text box and starts coroutine to type text
		dialogueText.text = "";
		StartCoroutine(TypeText(info));
	}

	//Coroutine to type text so it appears on screen with animation
	IEnumerator TypeText(DialogueBase.Info info)
	{
		currentlyTyping = true;

		foreach(char c in info.myText.ToCharArray())
		{
			yield return new WaitForSeconds(delay);
			dialogueText.text += c;
		}
		currentlyTyping = false;
	}

	//Sets text to complete line in case user double taps 'z'
	private void CompleteText()
	{
		dialogueText.text = completeLine;
	}

	//Ends the dialogue by setting the state of the box and bool to false
	public void EndDialogue()
	{
		DObjBox.SetActive(false);
		inDialogue = false;
	}
}