using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creates asset for dialogues that can be carried between projects
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues")]
public class DialogueBase : ScriptableObject
{
	[System.Serializable]
	public class Info
	{
		public string Name;
		[TextArea(4, 8)]
		public string myText;	
	}

	[Header("Insert Dialogue Information below")]
	public Info[] dialogueInfo;
}
