using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests")]
public class QuestBase : ScriptableObject
{
    //[System.Serializable]
    //public class Info
    //{
    public string questName;
    public GameObject obj;
    public int numToCollect;
    private int numCollected = 0;
    public bool isActive = false;
    public bool isCompleted = false;
    //}

    //[Header("Insert Quest Information below")]
    //public Info[] questInfo;

    /*public bool GetIsActive()
	{
		return isActive;
	}

	public void SetIsActive()
	{
		isActive = !isActive;
	}*/

    private void OnEnable()
    {
        isActive = false;
        isCompleted = false;
    }

    private void Awake()
    {
        isActive = false;
        isCompleted = false;
    }
}

