using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public struct Matrix{
	public int weight;
	public Edge edge;
}
public enum _NameAlgorithm{Depth_first_search,Breadth_first_search,Kruskal,Prim,Bellman_Ford,Dijkstra,Floyd_Warshall,
	Johnson,Ford_Fulkerson,Edmonds_Karp};

public class nameAlgorithm : MonoBehaviour {
	public Controller contr;
	public _NameAlgorithm state=_NameAlgorithm.Depth_first_search;
	public Text text_name;
	public float PauseTime=2f;
	public int Deep=0;
	public int Active_color=2;
	public int DisActive_color=1;
	private Recorder record;
	void Awake()
	{
		record = GameObject.FindGameObjectWithTag ("Recorder").GetComponent<Recorder> ();

	}
	/*/
	public void setAlgorithm_to_this()
	{
		contr.setAlgorithm (this);
	}
	/*/
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
			Kruskal();
			break;
		}
		case _NameAlgorithm.Prim:
		{
			if(contr.startVertex==null)
				return;
			Prim();
			break;
		}
		case _NameAlgorithm.Bellman_Ford:
		{
			if(contr.startVertex==null)
				return;
			Bellman_Ford();
			break;
		}
		case _NameAlgorithm.Dijkstra:
		{
			if(contr.startVertex==null)
				return;
			Dijkstra();
			break;
		}
		case _NameAlgorithm.Floyd_Warshall:
		{
			Floyd_Warshall();
			break;
		}
		case _NameAlgorithm.Johnson:
		{
			break;
		}
		case _NameAlgorithm.Ford_Fulkerson:
		{
			if(contr.startVertex==null||contr.endVertex==null)
				return;
			Ford_Fulkerson();
			break;
		}
		case _NameAlgorithm.Edmonds_Karp:
		{
			break;
		}
		}
		record.StartPlay();
		//record.Play();
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
			state=_NameAlgorithm.Prim;
			text_name.text="Prim";
			return;
		}
		if(index<0.5f)
		{
			state=_NameAlgorithm.Bellman_Ford;
			text_name.text="Bellman Ford";
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
	//===========================Kruskal============================================
	void Kruskal_Tree_index(Vertex vertex,int index)
	{
		Queue<Vertex> que = new Queue<Vertex> ();
		que.Enqueue (vertex);
		int begin_index = vertex.TreeIndex;
		//vertex.TreeIndex = index;
		Vertex current_vertex,search_vertex;
		while(que.Count>0)
		{
			current_vertex=que.Dequeue();
			if(current_vertex.TreeIndex==index)
				continue;
			current_vertex.TreeIndex=index;
			foreach(Edge edge in current_vertex.EdgeTree)
			{
				search_vertex=edge.getVertex(current_vertex);
				if(search_vertex.TreeIndex==begin_index)
				{
					que.Enqueue(search_vertex);
				}
			}
		}
	}
	public Vertex Kruskal()
	{
		GameObject[] gameObj_edges = GameObject.FindGameObjectsWithTag ("Edge") ;
		Edge[] edges=new Edge[gameObj_edges.Length];
		for (int i=0; i<gameObj_edges.Length; i++) 
		{
			edges[i]=(gameObj_edges [i].GetComponent<Edge>());
		}
		bool isExit;
		Edge _edge;//=new Edge;
		for(int i = 0; i < edges.Length - 1; i++)
		{
			isExit=true;
			for(int j = 0; j < edges.Length - i - 1; j++)
			{
				if(edges[j]==null||edges[j+1]==null)
				{
					print ("Somithng strange");
					continue;
				}
				if(edges[j].weight.value > edges[j + 1].weight.value)
				{
					//print("after swap "+edges[j].weight.weight+" "+edges[j + 1].weight.weight);
					isExit=false;
					_edge=edges[j];
					edges[j]=edges[j+1];
					edges[j+1]=_edge;
					//print("before swap "+edges[j].weight.weight+" "+edges[j + 1].weight.weight);
				}
			}
			if(isExit)
				break;
		}
		Vertex ver_1, ver_2;
		bool ver_1_cheked, ver_2_cheked;
		for(int i=0;i<edges.Length;i++)
		{
			ver_1=edges[i].getVertex(1);
			ver_2=edges[i].getVertex(2);
			ver_1_cheked=ver_1.isCheked();
			ver_2_cheked=ver_2.isCheked();

			if(ver_1_cheked&&ver_2_cheked)
			{
				//print ("1:"+ver_1.TreeIndex+" "+ver_2.TreeIndex);
				if(ver_1.TreeIndex==ver_2.TreeIndex)
				{
					//print("nice");
					continue;
				}
				Kruskal_Tree_index(ver_2,ver_1.TreeIndex);
				ver_1.setColor(Active_color);
				ver_2.setColor(Active_color);
				edges[i].setColor(Active_color,1);
				continue;
			}
			if(!ver_1_cheked&&ver_2_cheked)
			{
				//print ("2:"+ver_1.TreeIndex+" "+ver_2.TreeIndex);
				ver_1.TreeIndex=ver_2.TreeIndex;
				ver_1.setColor(Active_color);
				ver_2.setColor(Active_color);
				edges[i].setColor(Active_color,1);
				continue;
			}
			if(ver_1_cheked&&!ver_2_cheked)
			{
				//print ("3:"+ver_1.TreeIndex+" "+ver_2.TreeIndex);
				ver_2.TreeIndex=ver_1.TreeIndex;
				ver_1.setColor(Active_color);
				ver_2.setColor(Active_color);
				edges[i].setColor(Active_color,1);
				continue;
			}
			if(!ver_1_cheked&&!ver_2_cheked)
			{
				//print ("4:"+ver_1.TreeIndex+" "+ver_2.TreeIndex);
				ver_2.TreeIndex=ver_1.TreeIndex;
				ver_1.setColor(Active_color);
				ver_2.setColor(Active_color);
				edges[i].setColor(Active_color,1);
				continue;
			}
		}
		return new Vertex();
	}
	//=========================================================================================
	public void Prim()
	{
		GameObject[] gameObj_edges = GameObject.FindGameObjectsWithTag ("Edge") ;
		Edge[] _edges=new Edge[(gameObj_edges.Length)];
		for (int i=0; i<gameObj_edges.Length; i++) 
		{
			_edges[i]=(gameObj_edges [i].GetComponent<Edge>());
		}
		bool isExit;
		Edge _edge;
		for(int i = 0; i < _edges.Length - 1; i++)
		{
			isExit=true;
			for(int j = 0; j < _edges.Length - i - 1; j++)
			{
				if(_edges[j]==null||_edges[j+1]==null)
				{
					print ("Somithng strange");
					continue;
				}
				if(_edges[j].weight.value > _edges[j + 1].weight.value)
				{
					//print("after swap "+edges[j].weight.weight+" "+edges[j + 1].weight.weight);
					isExit=false;
					_edge=_edges[j];
					_edges[j]=_edges[j+1];
					_edges[j+1]=_edge;
					//print("before swap "+edges[j].weight.weight+" "+edges[j + 1].weight.weight);
				}
			}
			if(isExit)
				break;
		}
		List<Edge> edges=new List<Edge>();
		for(int i=0;i<_edges.Length;i++)
		{
			edges.Add(_edges[i]);
		}
		Vertex ver_1, ver_2;
		bool ver_1_cheked, ver_2_cheked;
		contr.startVertex.isCheked ();
		contr.startVertex.setColor (Active_color);
		bool isExitTime = false;
		while(!isExitTime)
		{
			isExitTime=true;
			foreach(Edge edge in edges)
			{
				if(edge.isCheked)
					continue;
				isExitTime=false;
				ver_1=edge.getVertex(1);
				ver_2=edge.getVertex(2);
				ver_1_cheked=ver_1.isCheked();
				ver_2_cheked=ver_2.isCheked();
				if(!ver_1_cheked&&!ver_2_cheked)
				{
					ver_1.unCheked();
					ver_2.unCheked();
					continue;
				}
				edge.isCheked=true;
				if(ver_1_cheked&&ver_2_cheked)
				{
					edge.setColor(DisActive_color,1);
					break;
				}
				if(!ver_1_cheked&&ver_2_cheked)
				{
					edge.setColor(Active_color,1);
					ver_1.setColor(Active_color);
					break;
				}
				if(ver_1_cheked&&!ver_2_cheked)
				{
					
					edge.setColor(Active_color,1);
					ver_2.setColor(Active_color);
					break;
				}
			}
		}

	}
	//==========================================Bellman=Ford========================
	public void Bellman_Ford()
	{
		List<Vertex> vertexs = contr.getVertexs ();
		GameObject[] gameObj_edges = GameObject.FindGameObjectsWithTag ("Edge") ;
		Edge[] _edges=new Edge[(gameObj_edges.Length)];
		for (int i=0; i<gameObj_edges.Length; i++) 
		{
			_edges[i]=(gameObj_edges [i].GetComponent<Edge>());
		}
		contr.startVertex.setDistance (0);
		Vertex ver_1, ver_2;
		Vertex closest_vertex;
		for (int i=1; i<vertexs.Count; i++) 
		{
			for(int j=0;j<_edges.Length;j++)
			{
				ver_1=_edges[j].getVertex(1);
				ver_2=_edges[j].getVertex(2);
				//print(ver_1.Index+" "+ver_2.Index);
				if(ver_2.getDistance()!=int.MaxValue&&ver_1.getDistance()>ver_2.getDistance()+_edges[j].weight.value)
				{
					print("1:"+ver_1.getDistance()+" 2:"+ver_2.getDistance()+" wei:"+_edges[j].weight.value);
					//ver_1.setColor(Active_color);
					closest_vertex=ver_1.GetClosestVertex();
					if(closest_vertex!=null)
						ver_1.getEdge(closest_vertex.Index).setColor(0,0);
					_edges[j].setColor(Active_color,1);
					ver_1.setDistance(ver_2.getDistance()+_edges[j].weight.value);
					ver_1.SetClosestVertex(ver_2);
				}
				else
				{
					print("1:"+ver_1.getDistance()+" 2:"+ver_2.getDistance()+" wei:"+_edges[j].weight.value);
					if(ver_1.getDistance()!=int.MaxValue&&ver_2.getDistance()>ver_1.getDistance()+_edges[j].weight.value)
					{
						closest_vertex=ver_2.GetClosestVertex();
						if(closest_vertex!=null)
							ver_2.getEdge(closest_vertex.Index).setColor(0,0);
						_edges[j].setColor(Active_color,1);
						ver_2.setDistance(ver_1.getDistance()+_edges[j].weight.value);
						ver_2.SetClosestVertex(ver_1);
					}
				}
			}
		}
	}
	public void Dijkstra()
	{
		List<Vertex> vertexs = contr.getVertexs();
		vertexs.Remove (contr.startVertex);
		vertexs.Insert (0,contr.startVertex);
		Vertex ver,ver1,closest_vertex;
		int i;
		contr.startVertex.setDistance(0);
		while(vertexs.Count>0)
		{
			ver=vertexs[0];
			ver.setColor(Active_color);
			vertexs.RemoveAt(0);
			foreach(Edge edge in ver.EdgeTree)
			{
				ver1=edge.getVertex(ver);
				if(ver1.getDistance()>ver.getDistance()+edge.weight.value)
				{
					closest_vertex=ver1.GetClosestVertex();
					if(closest_vertex!=null)
						ver1.getEdge(closest_vertex.Index).setColor(0,0);
					edge.setColor(Active_color,1);
					ver1.setDistance(ver.getDistance()+edge.weight.value);
					ver1.SetClosestVertex(ver);
					vertexs.Remove(ver1);
					for(i=0;i<vertexs.Count;i++)
					{
						if(vertexs[i].getDistance()>ver1.getDistance())
							break;
					}
					vertexs.Insert(i,ver1);
				}
			}
		}
	}
	public void Floyd_Warshall()
	{
		List<Vertex> vertexs = contr.getVertexs ();
		Matrix[][] matrix = new Matrix[vertexs.Count][];
		int i, j, k;
		//
		for (i=0; i<matrix.Length; i++) 
		{
			matrix[i]=new Matrix[vertexs.Count];
			for (j=0; j<matrix.Length; j++)
				if(i!=j)
					matrix [i] [j].weight = int.MaxValue;
				else
					matrix[i][j].weight=0;
		}
		//
		for(i=0;i<vertexs.Count;i++)
		{
			vertexs[i].setDistance(i);
			foreach(Edge edge in vertexs[i].EdgeTree)
			{
				j=vertexs.IndexOf(edge.getVertex(vertexs[i]));
				matrix[i][j].weight=edge.weight.value;
				matrix[i][j].edge=edge;
			}
		}
		for(k=0;k<matrix.Length;k++)
			for(i=0;i<matrix.Length;i++)
				for(j=0;j<matrix.Length;j++)
				{
					//print(k+":["+i+"]"+"["+j+"]:"+matrix[i][j].weight+","+matrix[i][k].weight+","+matrix[k][j].weight);
					if(matrix[i][k].weight!=int.MaxValue&&matrix[k][j].weight!=int.MaxValue&&
					   matrix[i][j].weight>matrix[i][k].weight+matrix[k][j].weight)
					{
						if(i!=j)
						{
							matrix[i][j].weight=matrix[i][k].weight+matrix[k][j].weight;
							//print(k+":["+i+"]"+"["+j+"]:"+matrix[i][j].weight);
						}
						else
							matrix[i][i].weight=0;
					}
			}
		//print ("|");
		for (i=0; i<matrix.Length; i++)
			for (j=0; j<matrix.Length; j++)
				print("["+i+"]"+"["+j+"]:"+matrix[i][j].weight);
	}
	public void Johnson()
	{
	}
	//===============================Ford=Fulkerson=================================
	public int Ford_Fulkerson_Depth_first_search(Vertex current,int min_delta=int.MaxValue)
	{
		current.setColor (Active_color);
		if (current.Index == contr.endVertex.Index) 
		{
			print("end Depth search");
			current.setColor(0);
			return min_delta;
		}
		Vertex _vertex;
		int delta;
		//print ("Lenght Edge:" + start.EdgeTree.Count);
		foreach (Edge edge in current.EdgeTree) 
		{
			_vertex=edge.getVertex(current);
			if(_vertex.isCheked())
				continue;
			delta=edge.stream_get(current);
			//print("delta:"+delta);
			if(delta==0)
			{
				_vertex.unCheked();
				continue;
			}
			edge.setColor(Active_color,_vertex);
			min_delta=(min_delta>delta?delta:min_delta);
			//print("min_delta:"+min_delta);
			delta=Ford_Fulkerson_Depth_first_search(_vertex,min_delta);
			if(delta!=-1)
			{
				//print(delta+" UP!!!");
				edge.stream_set(current,delta);
				current.setColor (0);
				return delta;
			}
			edge.setColor(0,0);

		}
		current.setColor (0);
		current.unCheked ();
		return  -1;
	}
	public void Ford_Fulkerson()
	{
		List<Vertex> vertexs = contr.getVertexs ();
		contr.startVertex.isCheked();
		while (Ford_Fulkerson_Depth_first_search(contr.startVertex)!=-1) {
			foreach(Vertex vertex in vertexs)
			{
				vertex.unCheked();
			}
			contr.startVertex.isCheked();
			print("next");
		}
	}
	//==============================================================================
}
