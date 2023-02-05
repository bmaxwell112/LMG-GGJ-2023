using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour
{
    public static Transform[] GetTopLevelChildren (Transform Parent) {
		Transform[] Children = new Transform[Parent.childCount];
		for (int ID = 0; ID < Parent.childCount; ID++) {
			Children[ID] = Parent.GetChild (ID);
		}
		return Children;
	}
}