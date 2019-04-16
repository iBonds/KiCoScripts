using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class Range : MonoBehaviour
{
    public string[] tags;

    private Dictionary<string, bool> tag_exist = new Dictionary<string, bool>();
    private Dictionary<string, Vector3> tag_pos = new Dictionary<string, Vector3>();
    private Dictionary<string, GameObject> tag_object = new Dictionary<string, GameObject>();

    private void Start()
    {
        foreach (string tag in tags)
        {
            tag_exist[tag] = false;
            tag_pos[tag] = Vector3.zero;
            tag_object[tag] = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string collision_tag = other.gameObject.tag;

        if (tag_exist.ContainsKey(collision_tag))
        {
            tag_exist[collision_tag] = true;
            tag_pos[collision_tag] = other.transform.position;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        string collision_tag = other.gameObject.tag;
        if (tag_exist.ContainsKey(collision_tag))
        {
            tag_exist[collision_tag] = true;
            tag_pos[collision_tag] = other.transform.position;
            tag_object[collision_tag] = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string collision_tag = other.gameObject.tag;
        if (tag_exist.ContainsKey(collision_tag))
        {
            tag_exist[collision_tag] = false;
        }
    }

    public bool InRange(string thing)
    {
        if (tag_exist.ContainsKey(thing))
            return tag_exist[thing];
        else
        {
            tag_exist[thing] = false;
            tag_pos[thing] = Vector3.zero;
            return false;
        }
    }

    public Vector3 Position(string thing)
    {
        Vector3 result = tag_pos[thing];
        //Debug.Log("Position of " + thing + ": " + result);
        return result;
    }

    public GameObject Item(string thing)
    {
        return tag_object[thing];
    }
}