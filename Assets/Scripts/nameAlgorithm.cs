using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public enum _NameAlgorithm{Depth_first_search,Breadth_first_search,Kruskal,Prim,Bellman_Ford,Dijkstra,Floyd_Warshall,
	Johnson,Ford_Fulkerson,Edmonds_Karp};
public class nameAlgorithm : MonoBehaviour {
	public Controller contr;
	public _NameAlgorithm state=_NameAlgorithm.Depth_first_search;
	public Text text_name;
	public float PauseTime=2f;
	public int Deep=0;
	public int Active_color=2;
	private Recorder record;
	void Awake()
	{
		record = GameObject.FindGameObjectWithTag ("Recorder").GetComponent<Recorder> ();
	}
	public void setAlgorithm_to_this()
	{
		contr.setAlgorithm (this);
	}
	public void  Start_Algoritghm()//Vertex StartVertex)
	{
		switch(state)
		{
		case _NameAlgorithm.Depth_first_search:
		{
			if(contr.startVertex==null||contr.endVertex==null)
				return;
			Vertex StartVertex= contr.startVertex;
			StartVertex.isCheked();
			StartVertex=Depth_first_search(StartVertex,contr.endVertex.Index);
			break;
		}
		case _NameAlgorithm.Breadth_first_search:
		{
			if(contr.startVertex==null||contr.endVertex==null)
				return;
			Breadth_first_search(contr.startVertex,contr.endVertex.Index);
			break;
		}
		case _NameAlgorithm.Kruskal:
		{
			break;
		}
		case _NameAlgorithm.Prim:
		{
			break;
		}
		case _NameAlgorithm.Bellman_Ford:
		{
			break;
		}
		case _NameAlgorithm.Dijkstra:
		{
			break;
		}
		case _NameAlgorithm.Floyd_Warshall:
		{
			break;
		}
		case _NameAlgorithm.Johnson:
		{
			break;
		}
		case _NameAlgorithm.Ford_Fulkerson:
		{
			break;
		}
		case _NameAlgorithm.Edmonds_Karp:
		{
			break;
		}
		}
		record.StartPlay();
		record.Play();
	}
	static void _Test()
	{
	}
	public void Set()
	{
		float index = GetComponent<UnityEngine.UI.Scrollbar> ().value;
		if (index < 0.1f) 
		{
			state=_NameAlgorithm.Depth_first_search;
			//text_name.text=state.ToString();//("Depth first search").ToString();
			//string name=;
			text_name.text="Depth first search";
			return;
		}
		if(index<0.2f)
		{
			state=_NameAlgorithm.Breadth_first_search;
			text_name.text="Breadth first search";
			return;
		}
		if(index<0.3f)
		{
			state=_NameAlgorithm.Kruskal;
			text_name.text="Kruskal";
			//text_name.text=new string();
			return;
		}
		if(index<0.4f)
		{
			state=_NameAlgorithm.Bellman_Ford;
			text_name.text="Bellman Ford";
			return;
		}
		if(index<0.5f)
		{
			state=_NameAlgorithm.Prim;
			text_name.text="Prim";
			return;
		}
		if(index<0.6f)
		{
			state=_NameAlgorithm.Dijkstra;
			text_name.text="Dijkstra";
			return;
		}
		if(index<0.7f)
		{
			state=_NameAlgorithm.Floyd_Warshall;
			text_name.text="Floyd Warshall";
			return;
		}
		if(index<0.8f)
		{
			state=_NameAlgorithm.Johnson;
			text_name.text="Johnson";
			return;
		}
		if(index<0.9f)
		{
			state=_NameAlgorithm.Ford_Fulkerson;
			text_name.text="Ford Fulkerson";
			return;
		}
		state=_NameAlgorithm.Edmonds_Karp;
		text_name.text="Edmonds Karp";

	}
	//
	Vertex  Depth_first_search(Vertex start,int index)
	{
		start.setColor (Active_color);
		if (start.Index == index)
		{
			return start;
		}
		Vertex _vertex;
		//print ("Lenght Edge:" + start.EdgeTree.Count);
		foreach (Edge edge in start.EdgeTree) 
		{
			_vertex=edge.getVertex(start);
			//print ("vertex index:" + _vertex.Index);
			if(_vertex.isCheked())
				continue;
			edge.setColor(Active_color,_vertex);
			_vertex=Depth_first_search(_vertex,index);
			if(_vertex!=null)
			{
				//print ("Deep go UP:" + Deep);
				return _vertex;
			}

			edge.setColor(Active_color,0);
		}
		//print ("Deep: dont find :(" + Deep);
		start.setColor (0);
		return  null;
	}
	public Vertex Breadth_first_search(Vertex start,int index)
	{
		Queue<Vertex> que = new Queue<Vertex> ();
		start.setColor (Active_color);
		que.Enqueue (start);
		if(start.Index==index)
		{
			start.setColor(Active_color);
			return start;
		}
		start.isCheked ();
		Vertex current_vertex,search_vertex;
		while(que.Count>0)
		{
			current_vertex=que.Dequeue();
			foreach(Edge edge in current_vertex.EdgeTree)
			{
				search_vertex=edge.getVertex(current_vertex);
				if(search_vertex.isCheked())
					continue;
				search_vertex.setColor(Active_color);
				edge.setColor(Active_color,search_vertex);
				if(search_vertex.Index==index)
					return search_vertex;
				que.Enqueue(search_vertex);
			}
		}
		return null;
	}
}
