using UnityEngine;
using System.Collections;

public class Canvas : MonoBehaviour {

	public bool editTime=false;
	public GameObject cloneText;
	private Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator> ();
	}
	public void Edit()
	{
		if (editTime) 
		{
			editTime=false;
			anim.SetBool("isActive",false);
		}
		else
		{
			editTime=true;
			anim.SetBool("isActive",true);
		}
	}

}
