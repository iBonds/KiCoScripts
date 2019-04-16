using UnityEngine;
using System.Collections;

public class Yarn : MonoBehaviour, IPickupable, IDamageable {

	public bool isPickedUp;
	public int maxHP;
	public bool inRange;
	public LevelManager lvlScript;
	GameObject kico;
	GameObject yarn;

	void Start() {
		isPickedUp = false;
		maxHP = 1;
		inRange = false;
		kico = GameObject.FindWithTag("mouth");
		yarn = transform.gameObject;
		lvlScript = GameObject.FindWithTag("lvl").GetComponent<LevelManager>();
	}

	void Update() {
		if(Input.GetKeyDown("z") && isPickedUp)
		putDown();
		else
		if (Input.GetKeyDown("z") && inRange)
		{
			if (!isPickedUp)
			pickUp();
		}
		if(maxHP == 0)
		onDeath();
		if(give_to_cat() && maxHP > 0) {
			doDamage();
		}
	}

	public void doDamage() {
		maxHP--;
	}
	public void onDeath() {
		lvlScript.amountGiven = lvlScript.amountGiven + 1;
		Destroy(yarn);
	}
	public void pickUp() {
		isPickedUp = true;
		//yarn.GetComponent<Rigidbody>().isKinematic = true;
		yarn.transform.SetParent(kico.transform);
		yarn.transform.position = kico.transform.position;
	}
	public void putDown() {
		isPickedUp = false;
		//yarn.GetComponent<Rigidbody>().isKinematic = false;
		yarn.transform.SetParent(null);
	}

	//if KiCo stays in contact with the gameobject, make inRange true
	void OnTriggerStay(Collider c) {
		if(c.tag == "Player") {
			if(Vector3.Distance(c.transform.position, yarn.transform.position) < 5) {
				inRange = true;
			}
		}
	}

	//resets inRange to false
	void OnTriggerExit(Collider c)
	{
		inRange = false;
	}

	bool give_to_cat()
	{
		return (Vector3.SqrMagnitude(GameObject.FindWithTag("QuestCat").transform.position - yarn.transform.position) < 7f);
	}

}
