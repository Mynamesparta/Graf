using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public enum _NameAlgorithm{Depth_first_search,Breadth_first_search,Kruskal,Prim,Bellman_Ford,Dijkstra,Floyd_Warshall,
	Johnson,Ford_Fulkerson,Edmonds_Karp};
public class nameAlgorithm : MonoBehaviour {
	public Controller contr;
	public _NameAlgorithm state=_NameAlgorithm.Depth_first_search;
	public Text text_name;
	public void setAlgorithm_to_this()
	{
		contr.setAlgorithm (this);
	}
	public void Start()
	{

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
}
