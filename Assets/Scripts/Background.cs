using UnityEngine;
using System.Collections;
public enum State_of_Background{ Background,Vertex,Edge}
public class Background : MonoBehaviour {
	public State_of_Background state;
	public Vertex _this_vertex;
	public Edge _this_edge;
	private Controller contr;
	void Awake()
	{
		contr = GameObject.FindGameObjectWithTag ("GameController").GetComponent<Controller>();
		switch(state)
		{
		case State_of_Background.Vertex:
		{
			_this_vertex=GetComponentInParent<Vertex> ();
			break;
		}
		case State_of_Background.Edge:
		{
			_this_edge=GetComponentInParent<Edge> ();
			break;
		}
		}
	}
	//
	void OnMouseOver()
	{
		switch(state)
		{
		case State_of_Background.Vertex:
		{
			switch(contr.getState())
			{
			case State_of_Controller.Edit:
			{
				if (Input.GetMouseButtonDown (0))
					contr.Add(_this_vertex);
				if(Input.GetMouseButtonDown(1))
					contr.Delete_vertex(_this_vertex);
				break;
			}
			case State_of_Controller.Pick:
			{
				contr.PickAction(_this_vertex);
				break;
			}
			}
			break;
		}
		case State_of_Background.Background:
		{
			switch(contr.getState())
			{
			case State_of_Controller.Edit:
			{
				if (Input.GetMouseButtonDown (0))
					contr.Add();
				break;
			}
			case State_of_Controller.Pick:
			{
				contr.PickAction();
				break;
			}
			}
			break;
		}
		case State_of_Background.Edge:
		{
			if(Input.GetMouseButtonDown(1))
			{
				_this_edge.deleteEdge();
				Destroy(this.gameObject);
			}
			break;
		}
		}
	}
	//
}
