using UnityEngine;
using System.Collections;
using System.Collections.Generic;
enum State_of_Controller {Play,Edit,Normal};
public class Controller : MonoBehaviour {

	public GameObject clone_of_Vertex;
	public GameObject clone_of_Edge;
	//public Transform center;
	public uint maxNumberOfVertex=10;
	public float radius_of_Arey=1;
	public float pixelH;

	private State_of_Controller state=State_of_Controller.Normal;
	private Queue<int> currendIndex;
	private List<Vertex> vertexs;
	private int nextIndex;
	private Vertex first_vertex;

	void Awake()
	{
		currendIndex=new Queue<int>(); 
		nextIndex = 1;
		vertexs = new List<Vertex> ();
	}

	public void Edit()
	{
		switch (state) {
		case State_of_Controller.Edit:
			{
				state = State_of_Controller.Normal;
				break;
			}
		case State_of_Controller.Normal:
			{
				state = State_of_Controller.Edit;
				break;
			}
		}
		print (state);
	}
	void LateUpdate()
	{
		switch(state)
		{
		case State_of_Controller.Play:
		{
			break;
		}
		case State_of_Controller.Normal:
		{
			break;
		}
		case State_of_Controller.Edit:
		{
			if(Input.GetMouseButtonDown(0))
			{
				Add();
			}
			else
				if(Input.GetMouseButtonDown(1))
				{
					Delete();
				}

			break;
		}
		}
	}
	int newIndex()
	{
		int index;
		if(currendIndex.Count>0)
		{
			index= currendIndex.Dequeue();
		}
		else
		{
			index=nextIndex;
			nextIndex++;
		}
		if(index>maxNumberOfVertex)
		{
			return -1;
		}
		print ("newIndex:"+index);
		return index;
	}
	public Vector3 getMousePosition()
	{
		Vector3 position = Input.mousePosition*pixelH;
		print (position.y / Camera.main.pixelHeight);
		position = position - new Vector3 (0.5f*Camera.main.pixelWidth*pixelH,0.5f*Camera.main.pixelHeight*pixelH,0f );
		return position;
	}
	void Add()
	{
		Vector3 position = getMousePosition ();
		bool Area_Empty=true;
		Vertex _vertex;
		foreach (Vertex vertex in vertexs) 
		{
			if(Vector3.Distance(vertex.gameObject.transform.position,position)<radius_of_Arey)
			{
				Area_Empty=false;
				_vertex=vertex;
				if(first_vertex==null)
				{
					first_vertex=_vertex;
					first_vertex.setColor(1);
				}
				else
				{
					if(first_vertex==_vertex)
					{
						first_vertex.setColor(0);
						first_vertex=null;
					}
					else
					{
						Edge edge=(Object.Instantiate(clone_of_Edge)as GameObject).GetComponent<Edge>();
						edge.setVertexs(first_vertex.transform,vertex.transform);
						first_vertex.addEdge(edge);
						vertex.addEdge(edge);
						first_vertex.setColor(0);
						first_vertex=null;
					}
				}
				break;
			}
		}
		if (Area_Empty) {
			int index = newIndex ();
			if (index == -1)
				return;
			_vertex = (Object.Instantiate (clone_of_Vertex, position, Quaternion.Euler (0, 0, 0))as GameObject).GetComponent<Vertex> ();
			_vertex.setIndex (index);
			vertexs.Add (_vertex);

		} 
	}
	void Delete()
	{
		Vector3 position = getMousePosition ();
		bool Area_Empty=true;
		Vertex _vertex;
		foreach (Vertex vertex in vertexs) 
		{
			if(Vector3.Distance(vertex.gameObject.transform.position,position)<radius_of_Arey)
			{
				Area_Empty=false;
				_vertex=vertex;
				print("_vertex:"+_vertex.Index);
				currendIndex.Enqueue(_vertex.Index);
				vertexs.Remove(_vertex);
				_vertex.Destroy();
				break;
			}
		}
	}
	
}
