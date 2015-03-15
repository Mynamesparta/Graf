using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Vertex : MonoBehaviour {
	public int Index;
	public List<Edge> EdgeTree;
	private Animator anim;
	// Update is called once per frame
	void Awake()
	{
		anim = GetComponent<Animator> ();
		EdgeTree = new List<Edge> ();
	}
	public void setIndex(int index)
	{
		Index = index;
	}
	public void Destroy()
	{
		foreach(Edge edge in EdgeTree)
		{
			edge.deleteEdge();
		}
		EdgeTree.Clear ();
		Object.Destroy (gameObject);
	}
	
	public void setColor(int i)
	{
		anim.SetInteger ("color", i);
	}
	public void addEdge(Edge edge)
	{
		EdgeTree.Add (edge);
	}
	public void deleteEdge(Edge edge)
	{
		EdgeTree.Remove (edge);
	}

}
