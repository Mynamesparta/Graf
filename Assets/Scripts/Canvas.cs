using UnityEngine;
using System.Collections;

public class Canvas : MonoBehaviour {

	public Recorder recorder;
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
		}
	}
	public void Edit(int index,bool b)
	{
		editTime[index]=b;
		anim[index].SetBool("isActive",b);
	}
	public void inScene(int index,bool set)
	{
		anim [index].SetBool ("inScene", set);//!anim [index].GetBool ("inScene"));
	}
	public void TimeToRecorder()
	{
		bool b = !anim [7].GetBool ("inScene");
		for (int i =5; i<=7; i++) 
		{
			anim[i].SetBool("inScene",b);
		}
	}
	void LateUpdate()
	{
		if(recorder.is_inPlaying_state()&&!!recorder.isPlaying())
		{
			Edit(3,false);
			Edit(4,true);
		}
	}

}
