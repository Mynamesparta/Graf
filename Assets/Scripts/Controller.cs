using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum State_of_Controller {Play,Edit,Normal,Pick,_choseStartVertex};
public class Controller : MonoBehaviour {

	public GameObject clone_of_Vertex;
	public GameObject clone_of_Edge;
	public GameObject evengameobject;
	public GameObject canvas;
	public GameObject button_Chose;
	public Recorder recorder;
	public nameAlgorithm algorightm;
	public Camera maincamera;
	//public Transform center;
	public uint maxNumberOfVertex=10;
	public float radius_of_Arey=1;
	public float pixelH;
	public float pixelW;
	
	public Vertex startVertex;
	public Vertex endVertex;
	public Vector2 CreateZone;
	public int Active_State=1;
	public int Pick_State=1;

	private State_of_Controller state=State_of_Controller.Normal;
	private Queue<int> currendIndex;
	private List<Vertex> vertexs;
	private List<Vertex> itemsformove;
	private int nextIndex;
	private Vertex first_vertex;
	private Vector3 LastPositionMouse;
	private Event even;
	private bool IsPick=false;
	private bool choseEndVertex = false;
	private nameAlgorithm currentAlgorithm;
	private Canvas _canvas;
	//=====================private=function======================================
	void Awake()
	{
		currendIndex=new Queue<int>(); 
		nextIndex = 1;
		vertexs = new List<Vertex> ();
		itemsformove = new List<Vertex> ();
		//even = this.GetComponent<Event_System> ();
		//button_Chose.SetActive(false);
		_canvas = canvas.GetComponent<Canvas> ();

	}


	public void Edit()
	{
		switch (state) {
		case State_of_Controller.Edit:
			{
				state = State_of_Controller.Normal;
				//button_Chose.SetActive(false);
				_canvas.inScene(0,false);
				_canvas.inScene(2,false);
				_canvas.inScene(3,true);
				GameObject[] edge= GameObject.FindGameObjectsWithTag("Edge");
				for(int i=0;i<edge.Length;i++)
				{
					if(edge[i]!=null)
						if(edge[i].GetComponent<Edge>()!=null)
							if(edge[i].GetComponent<Edge>().weight!=null)
								edge[i].GetComponent<Edge>().weight.setEdit(false);
				}
					break;
				}
		case State_of_Controller.Normal:
			{
				state = State_of_Controller.Edit;
				//button_Chose.SetActive(true);
				_canvas.inScene(0,true);
				_canvas.inScene(2,true);
				_canvas.inScene(3,false);
				GameObject[] edge = GameObject.FindGameObjectsWithTag("Edge");
				for(int i=0;i<edge.Length;i++)
				{
					if(edge[i]!=null)
						if(edge[i].GetComponent<Edge>()!=null)
							if(edge[i].GetComponent<Edge>().weight!=null)
								edge[i].GetComponent<Edge>().weight.setEdit(true);
				}
				break;
			}
		case State_of_Controller.Pick:
			{
				foreach (Vertex _vertex in itemsformove)
					_vertex.setColor (0);
				itemsformove.Clear ();
				//print ("PickAction removes:" + itemsformove.Count);
				state = State_of_Controller.Normal;
				break;
			}
		case State_of_Controller.Play:
		{
			state=State_of_Controller.Edit;
			recorder.toBegin();
			_canvas.inScene(0,true);
			_canvas.inScene(4,false);
			_canvas.inScene(2,true);
			_canvas.inScene(3,false);
			GameObject[] edge= GameObject.FindGameObjectsWithTag("Edge");
			for(int i=0;i<edge.Length;i++)
			{
				if(edge[i]!=null)
					if(edge[i].GetComponent<Edge>()!=null)
						if(edge[i].GetComponent<Edge>().weight!=null)
							edge[i].GetComponent<Edge>().weight.setEdit(true);
			}
			_canvas.TimeToRecorder ();
			break;
		}
		}
	}
	//===================================Update=fuction=====================
	void Update()
	{
		switch(state)
		{
		case State_of_Controller.Edit://|State_of_Controller.Pick:
		{
			if(Input.GetButtonDown("Pick"))
			{
				//print("Start Pick Action");
				state=State_of_Controller.Pick;
			}
			break;
		}
		}
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
				//Add();
			}
			else
				if(Input.GetMouseButtonDown(1))
				{
					//Delete_vertex();
				}

			break;
		}
		case State_of_Controller.Pick:
		{
			//PickAction();
			break;
		}
		case State_of_Controller._choseStartVertex:
		{
			//searchStartVertex();
			break;
		}
		}
	}
	//==========================================Create_of_Destroy=function===============
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
		//print ("newIndex:"+index);
		return index;
	}
	public Vector3 getMousePosition()
	{
		Vector3 position = Input.mousePosition;
		position = maincamera.ScreenToWorldPoint (position);
		position.z = 0;
		/*/
		Vector3 position = new Vector3(Input.mousePosition.x*pixelW,Input.mousePosition.y*pixelH);
		//print (position.y / Camera.main.pixelHeight);
		position = position - new Vector3 (0.5f*Camera.main.pixelWidth*pixelW,0.5f*Camera.main.pixelHeight*pixelH,0f );
		/*/
		return position;
	}
	public Vector3 getDeltaMousePosition()
	{
		//return new Vector3 ();
		//
		Vector3 position;
		if(LastPositionMouse==null||Input.GetMouseButtonDown(0))
		{
			position=new Vector3();
			LastPositionMouse=getMousePosition();
		}
		else
		{
			position=(getMousePosition()-LastPositionMouse);
			LastPositionMouse=getMousePosition();
		}
		//print (position);
		position = new Vector3(position.x,position.y);
		return position;
		//
		//print (even.delta);
		//return position;
	}
	public void Add(Vertex _vertex)
	{
		if(first_vertex==null)
		{
			first_vertex=_vertex;
			first_vertex.setColor(Active_State);
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
				edge.setVertexs(first_vertex.transform,_vertex.transform);
				first_vertex.addEdge(edge);
				_vertex.addEdge(edge);
				first_vertex.setColor(0);
				first_vertex=null;
			}
		}

	}
	public void Add()
	{
		/*/
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
		/*/
		Vector3 position = getMousePosition ();
		Vertex _vertex;
		int index = newIndex ();
		if (index == -1)
			return;
		_vertex = (Object.Instantiate (clone_of_Vertex, position, Quaternion.Euler (0, 0, 0))as GameObject).GetComponent<Vertex> ();
		_vertex.setIndex (index);
		vertexs.Add (_vertex);
	}
	public void Delete_vertex(Vertex _vertex)
	{
		/*/
		Vector3 position = getMousePosition ();
		bool Area_Empty=true;
		foreach (Vertex vertex in vertexs) 
		{
			if(Vector3.Distance(vertex.gameObject.transform.position,position)<radius_of_Arey)
			{
				Area_Empty=false;
				_vertex=vertex;
				//print("_vertex:"+_vertex.Index);
				currendIndex.Enqueue(_vertex.Index);
				vertexs.Remove(_vertex);
				_vertex.Destroy();
				break;
			}
		}
		/*/
		print ("Delete");
		Vector3 position = getMousePosition ();
		currendIndex.Enqueue(_vertex.Index);
		vertexs.Remove(_vertex);
		_vertex.Destroy();
	}
	public void PickAction()
	{
		if(!Input.GetButton("Pick"))
			if(Input.GetMouseButtonUp(0))
		{
			foreach(Vertex _vertex in itemsformove)
				_vertex.setColor(0);
			itemsformove.Clear();
			//print("PickAction removes:"+itemsformove.Count);
			state=State_of_Controller.Edit;
		}
		else
		{
			if(IsPick)
			{
				Vector3 dpos=getDeltaMousePosition();
				//print(dpos);
				foreach(Vertex _vertex in itemsformove)
				{
					_vertex.transform.position=_vertex.transform.position+dpos;
				}
			}
		}

	}
	public void PickAction(Vertex _vertex)
	{
		//Vector3 position= getMousePosition ();
		/*/
		foreach (Vertex vertex in vertexs) 
		{
			if(Vector3.Distance(vertex.gameObject.transform.position,position)<radius_of_Arey)
			{
				//Area_Empty=false;
		/*/
				if(Input.GetButton("Pick"))
				{
					if(Input.GetMouseButtonDown(0))
					{
						if(itemsformove.IndexOf(_vertex)==-1)
						{
							itemsformove.Add(_vertex);
							_vertex.setColor(Pick_State);
							//print("PickAction add:"+itemsformove.Count);
						}
					}
					else
					{
						if(Input.GetMouseButtonDown(1))
						{
							itemsformove.Remove(_vertex);
							_vertex.setColor(0);
							//print("PickAction remove:"+itemsformove.Count);
						}
					}
				}
				else
				{
					if(Input.GetMouseButton(0))
					{
						if(itemsformove.IndexOf(_vertex)==-1)
							return;
						IsPick=true;
						Vector3 dpos=getDeltaMousePosition();
						//print(dpos);
						foreach(Vertex vertex in itemsformove)
						{
							vertex.transform.position=vertex.transform.position+dpos;
						}
					}
					else
					{
						IsPick=false;
						if(Input.GetMouseButtonDown(1))
						{
							foreach(Vertex vertex in itemsformove)
							{
								vertexs.Remove(vertex);
								vertex.Destroy();
							}
							itemsformove.Clear();
							state=State_of_Controller.Edit;
							//print("PickAction remove:"+itemsformove.Count);
						}
					}
				}
				return;
		/*/
			}
		}
		//
		if(!Input.GetButton("Pick"))
			if(Input.GetMouseButtonUp(0))
			{
				foreach(Vertex _vertex in itemsformove)
					_vertex.setColor(0);
				itemsformove.Clear();
				//print("PickAction removes:"+itemsformove.Count);
				state=State_of_Controller.Edit;
			}
			else
			{
				if(IsPick)
				{
					Vector3 dpos=getDeltaMousePosition();
					//print(dpos);
					foreach(Vertex _vertex in itemsformove)
					{
						_vertex.transform.position=_vertex.transform.position+dpos;
					}
				}
			}
		/*/
	}
	/*/
	public void setAlgorithm(nameAlgorithm na)
	{
		currentAlgorithm = na;
	}
	/*/
	public void choseStartVertex()
	{
		if (state == State_of_Controller.Edit) 
		{
			state = State_of_Controller._choseStartVertex;
			choseEndVertex=false;
		}
		else
			state = State_of_Controller.Edit;
	}
	public void searchStartVertex(Vertex vertex)
	{
		//Vector3 position= getMousePosition ();
		/*/
		foreach (Vertex vertex in vertexs) 
		{
			if(Vector3.Distance(vertex.gameObject.transform.position,position)<radius_of_Arey)
			{
		/*/
				if(Input.GetMouseButtonDown(0))
				{
					if(!choseEndVertex)
					{
						//print("heelo");
						if(startVertex!=null)
						{
							startVertex.setStartVertex(0);
						}
						startVertex=vertex;
						vertex.setStartVertex(1);
						choseEndVertex=true;
					}
					else
					{
						if(startVertex==vertex)
							return;
						if(endVertex!=null)
						{
							endVertex.setStartVertex(0);
						}
						endVertex=vertex;
						endVertex.setStartVertex(2);
						choseStartVertex();
						_canvas.Edit(2);
						//_canvas.inScene(2,false);
					}
				}
				return;
		/*/
			}
		}
		/*/
	}
	public void Play()
	{
		if (state != State_of_Controller.Normal)
			return;
		foreach (Vertex vertex in vertexs) 
		{
			vertex.unCheked();
			vertex.AwakeTreeIndex();
		}
		recorder.StartCreate ();
		state = State_of_Controller.Play;
		_canvas.Edit (4, true);
		_canvas.inScene(4,true);
		_canvas.TimeToRecorder ();
		algorightm.Start_Algoritghm ();
		GameObject[] edge= GameObject.FindGameObjectsWithTag("Edge");
		for(int i=0;i<edge.Length;i++)
		{
			if(edge[i]!=null)
				if(edge[i].GetComponent<Edge>()!=null)
					edge[i].GetComponent<Edge>().weight.setEdit(false);
		}
	}
	public State_of_Controller getState()
	{
		State_of_Controller _state = new State_of_Controller ();
		_state = state;
		return _state;
	}
	
}
