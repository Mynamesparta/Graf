using UnityEngine;
using System.Collections;

public class Edge : MonoBehaviour {

	public Transform LeftConer=null;
	public Transform RightConer;

	public Vector3 rotation;
	public float DistanceToScele;
	public GameObject clone_of_Weight_Canvas;
	public Weights_Canvas weight;
	private Recorder record;
	private Animator anim;
	private int _rightleft=0;
	// Update is called once per frame
	void Awake()
	{
		anim = GetComponent<Animator> ();
		record = GameObject.FindGameObjectWithTag ("Recorder").GetComponent<Recorder> ();
		weight = (Object.Instantiate (clone_of_Weight_Canvas,
		                              new Vector3(-10000,-10000,-10000),
		                              Quaternion.Euler(0,0,90f)
		                              )as GameObject).GetComponent<Weights_Canvas> ();
	}
	void LateUpdate () 
	{
		if(LeftConer!=null&&RightConer!=null)
		{
			transform.position = LeftConer.position + (RightConer.position - LeftConer.position) / 2;
			transform.rotation=	Quaternion.FromToRotation(rotation,RightConer.position-LeftConer.position);
			//transform.rotation= Quaternion.Euler(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y,90f);
			transform.localScale=new Vector3(transform.localScale.x,
			                                 Vector3.Distance(LeftConer.position,RightConer.position)*DistanceToScele,
			                                 transform.localScale.z);
		}
	}
	//
	public void setColor(int i,Vertex ver)
	{
		int mov;
		if (ver.Index == LeftConer.gameObject.GetComponent<Vertex> ().Index)
			mov = -2;
		else
			mov = 2;
		if(record.isCreateRecord())
			record.Add (this, i, mov);
		else
		{
			print("Edge:"+i+","+mov);
			anim.SetInteger ("color", i);
			anim.SetInteger ("Left_Right", mov);
		}
	}
	public void setColor(int i,int mov)
	{
		if(record.isCreateRecord())
			record.Add (this, i, mov);
		else
		{
			anim.SetInteger ("color", i);
			anim.SetInteger ("Left_Right", mov);
		}

	}
	public void deleteEdge(Vertex ignore=null)
	{
		if(ignore!=null)
		{
			if(LeftConer!=null&&LeftConer.gameObject!=ignore.gameObject)
			{
				LeftConer.gameObject.GetComponent<Vertex>().deleteEdge(this);
			}
			if(RightConer!=null&&RightConer.gameObject!=ignore.gameObject)
			{
				RightConer.gameObject.GetComponent<Vertex>().deleteEdge(this);
			}
		}
		else
		{
				if(LeftConer!=null)
				{
					LeftConer.gameObject.GetComponent<Vertex>().deleteEdge(this);
				}
				if(RightConer!=null)
				{
					RightConer.gameObject.GetComponent<Vertex>().deleteEdge(this);
				}
		}
		Object.Destroy (this.weight.gameObject);
		Object.Destroy (this.gameObject);
	}
	public void setVertexs(Transform first,Transform second)
	{
		LeftConer = first;
		RightConer = second;
		weight.LeftConer = LeftConer;
		weight.RightConer = RightConer;
	}
	public Vertex getVertex(Vertex ver)
	{
		if (ver.Index == LeftConer.gameObject.GetComponent<Vertex> ().Index)
			return RightConer.gameObject.GetComponent<Vertex> ();
		else
			return LeftConer.gameObject.GetComponent<Vertex> ();
	}
	public Vertex getVertex(short i)
	{
		if(i==1)
		{
			return LeftConer.gameObject.GetComponent<Vertex>();
		}
		else
		{
			return RightConer.gameObject.GetComponent<Vertex>();
		}
	}
	//
}
