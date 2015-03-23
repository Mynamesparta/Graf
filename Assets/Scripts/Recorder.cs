using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ListofScenario
{
	//----------------------vertex-----
	public Vertex _vertex;
	public int _color_of_Vertex=0;
	//----------------------edge-------
	public Edge _edge;
	public int _color_of_Edge=0;
	public int _Left_Right=0;
	//---------------------------------
	public void Play()
	{
		if(_vertex!=null)
		{
			_vertex.setColor(_color_of_Vertex);
		}
		if(_edge!=null)
		{
			_edge.setColor(_color_of_Edge,_Left_Right);
		}
	}
	public void backPlay()
	{
		if(_vertex!=null)
		{
			_vertex.setColor(0);
		}
		if(_edge!=null)
		{
			_edge.setColor(0,0);
		}
	}
}
enum State_of_Recorder{Create,Play,Block};
public class Recorder : MonoBehaviour {
	public float PauseTime;

	private State_of_Recorder state = State_of_Recorder.Play;

	private List<ListofScenario> Scenario;
	private ListofScenario current_list;
	private int Iteration=0;
	private bool isTimetoPlay=false;

	void Awake()
	{
		Scenario = new List<ListofScenario> ();
	}
	IEnumerator  _Block(float time)
	{
		State_of_Recorder _state = new State_of_Recorder();
		_state = state;
		state = State_of_Recorder.Block;
		yield return new WaitForSeconds (time);
		state = _state;
	}
	IEnumerator _Play()
	{
		//print ("Test");
		for(;Iteration<Scenario.Count;Iteration++)
		{
			Scenario[Iteration].Play();
			yield return new WaitForSeconds(PauseTime);
			while(!isTimetoPlay)
				yield return new WaitForSeconds(0.1f);
		}
		//isTimetoPlay = false;
	}
	public void StartCreate()
	{
		if(Scenario!=null)
			toBegin ();
		if(Scenario==null)
			Scenario=new List<ListofScenario>();
		else
			Scenario.Clear ();
		state = State_of_Recorder.Create;
	}
	public void StartPlay()
	{
		if(current_list!=null)
			Scenario.Insert(Scenario.Count,current_list);
		current_list = null;
		isTimetoPlay = true;
		state = State_of_Recorder.Play;
		Iteration = 0;
	}
	public void Block(float time)
	{
		StartCoroutine(_Block(time));
	}
	public void Add(Vertex vertex,int color)
	{
		if (state != State_of_Recorder.Create)
			return;
		if(current_list==null)
		{
			current_list=new ListofScenario();
			current_list._vertex=vertex;
			current_list._color_of_Vertex=color;
		}
		else
		{
			if(current_list._edge==null)
			{
				Scenario.Insert(Scenario.Count,current_list);
				current_list=new ListofScenario();
				current_list._vertex=vertex;
				current_list._color_of_Vertex=color;
			}
			else
			{
				current_list._vertex=vertex;
				current_list._color_of_Vertex=color;
				Scenario.Insert(Scenario.Count,current_list);
				current_list=null;
			}
		}
	}
	public void Add(Edge edge,int color,int right_left)
	{
		if (state != State_of_Recorder.Create)
			return;
		if(current_list==null)
		{
			current_list=new ListofScenario();
			current_list._edge=edge;
			current_list._color_of_Edge=color;
			current_list._Left_Right=right_left;
		}
		else
		{
			if(current_list._vertex==null)
			{
				Scenario.Insert(Scenario.Count,current_list);
				current_list=new ListofScenario();
				current_list._edge=edge;
				current_list._color_of_Edge=color;
				current_list._Left_Right=right_left;
			}
			else
			{
				current_list._edge=edge;
				current_list._color_of_Edge=color;
				current_list._Left_Right=right_left;
				Scenario.Insert(Scenario.Count,current_list);
				current_list=null;
			}
		}

	}
	public void Play()
	{
		if (state != State_of_Recorder.Play)
			return;
		//print ("Scenario:" + Scenario.Count);
		if (Iteration >= Scenario.Count)
			return;
		if (Iteration <= -1)
			Iteration = 0;
		if(isTimetoPlay)
			StartCoroutine ("_Play");
		else
		{
			isTimetoPlay=true;
		}
	}
	public void Pause()
	{
		isTimetoPlay = false;
	}
	public bool isCreateRecord()
	{
		if (state == State_of_Recorder.Create)
			return true;
		else 
			return false;
	}
	public bool is_inPlaying_state ()
	{
		if (state == State_of_Recorder.Play)
			return true;
		else
			return false;
	}
	public bool isPlaying()
	{
		if (state==State_of_Recorder.Play&&isTimetoPlay&&Iteration < Scenario.Count) 
		{
			return true;
		}
		else
			return false;
	}
	public void toBegin()
	{
		if (Iteration <= -1)
			return;
		if (Iteration >= Scenario.Count)
			Iteration = Scenario.Count - 1;
		if(Iteration==Scenario.Count)
		{
			Iteration=Scenario.Count-1; 
		}
		for(;Iteration>=0;Iteration--)
		{
			Scenario[Iteration].backPlay();
		}

	}
	
	public void Rewind()
	{
		if (Iteration <= -1)
			return;
		if (Iteration >= Scenario.Count)
			Iteration = Scenario.Count - 1;
		Scenario [Iteration].backPlay ();
		Iteration--;
	}
	public void Foward()
	{
		if (Iteration >= Scenario.Count)
			return;
		if (Iteration <= -1)
			Iteration = 0;
		Scenario [Iteration].Play ();
		Iteration++;
	}



}
