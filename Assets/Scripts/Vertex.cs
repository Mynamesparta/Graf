using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Vertex : MonoBehaviour {
	public Text Distance;
	public int Index;
	public List<Edge> EdgeTree;
	public  int TreeIndex;
	private Recorder record;
	private Controller contr;
	private Animator anim;
	private bool isVertexChecked=false;
	private int distance;
	private Vertex closestVertex;
	private Edge lastEdge;
	private Edge nextEdge;
	void Awake()
	{
		anim = GetComponent<Animator> ();
		EdgeTree = new List<Edge> ();
		record = GameObject.FindGameObjectWithTag ("Recorder").GetComponent<Recorder> ();
		contr = GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller>();
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
	public Edge getEdge(int index)
	{
		foreach(Edge edge in EdgeTree)
		{
			if(edge.getIndex(Index)==index)
				return edge;
		}
		return null;
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
	public void AwakeTreeIndex()
	{
		TreeIndex = Index;
	}
	public void setDistance(int d)
	{	string text;
		if (d == int.MaxValue)
			text = "∞";
		else
			text = d.ToString();
		distance = d;
		//print ("setDistance:"+text);
		if(record.isCreateRecord())
		{
			text="("+text+")";
			record.Add (this,text );
		}
		else
		{
			//distance = d;
			Distance.text="("+text+")";
		}
	}
	public void setDistance(string text)
	{
		Distance.text = text;
	}
	public int getDistance()
	{
		return distance;
	}
	public void resetDistanse()
	{
		distance = int.MaxValue;
		Distance.text = "";
	}
	void OnMouseDown()
	{
		return;
		if(contr.getState()==State_of_Controller.Edit)
			if (Input.GetMouseButtonDown (0))
				contr.Add (this);
	}
	public void SetClosestVertex(Vertex hi)
	{
		closestVertex = hi;
	}
	public Vertex GetClosestVertex()
	{
		return closestVertex;
	}
	public void SetLastEdge(Edge edge)
	{
		lastEdge = edge;
		//print("set:"+lastEdge.getVertex(1).Index+"--(-0)->"+lastEdge.getVertex(2).Index);
	}
	public Edge GetLastEdge()
	{
		//print("get:"+lastEdge.getVertex(1).Index+"--(-0)->"+lastEdge.getVertex(2).Index);
		return lastEdge;
	}
	public void SetNextEdge(Edge edge)
	{
		nextEdge = edge;
	}
	public Edge GetNextEdge()
	{
		return nextEdge;
	}

}
