using UnityEngine;
using System.Collections;

public class Canvas : MonoBehaviour {

	public Animator[] anim;
	
	private bool[] editTime;

	void Awake()
	{
		anim = GetComponentsInChildren<Animator> ();
		editTime=new bool[anim.Length];
		for(int i=0;i<editTime.Length;i++)
		{
			editTime[i]=false;
		}
		inScene (1, true);
		inScene (3, true);
	}
	public void Edit(int index)
	{
		if (editTime[index]) 
		{
			editTime[index]=false;
			anim[index].SetBool("isActive",false);
		}
		else
		{
			editTime[index]=true;
			anim[index].SetBool("isActive",true);
			print (anim[index].GetBool("isActive"));
		}
	}
	public void inScene(int index,bool set)
	{
		anim [index].SetBool ("inScene", set);//!anim [index].GetBool ("inScene"));
	}
	public void TimeToRecorder()
	{
		bool b = !anim [4].GetBool ("inScene");
		for (int i =4; i<=7; i++) 
		{
			anim[i].SetBool("inScene",b);
		}
	}

}
