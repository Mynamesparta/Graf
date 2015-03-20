using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Vertex : MonoBehaviour {
	public int Index;
	private Recorder record;
	public List<Edge> EdgeTree;
	private Animator anim;
	//private bool isStartVertex=false;
	private bool isVertexChecked=false;
	// Update is called once per frame
	void Awake()
	{
		anim = GetComponent<Animator> ();
		EdgeTree = new List<Edge> ();
		record = GameObject.FindGameObjectWithTag ("Recorder").GetComponent<Recorder> ();
	}
	public void setIndex(int index)
	{
		Index = index;
	}
	public void Destroy()
	{
		foreach(Edge edge in EdgeTree)
		{
			edge.deleteEdge(this);
		}
		EdgeTree.Clear ();
		Object.Destroy (gameObject);
	}
	
	public void setColor(int i)
	{
		if(record.isCreateRecord())
			record.Add (this, i);
		else
			if(anim!=null)
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
	public void setStartVertex(int b)
	{
		//isStartVertex = b;
		anim.SetInteger ("isStartVertex", b);
	}
	public bool isCheked()
	{
		if(isVertexChecked)
		{
			return true;
		}
		{
			isVertexChecked=true;
			return false;
		}
	}
	public void unCheked()
	{
		isVertexChecked = false;
	}

}
