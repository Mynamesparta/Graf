using UnityEngine;
using System.Collections;

public class Edge : MonoBehaviour {

	public Transform LeftConer;
	public Transform RightConer;
	public Vector3 rotation;
	public float DistanceToScele;
	private Animator anim;
	// Update is called once per frame
	void Awake()
	{
		anim = GetComponent<Animator> ();
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
	public void setColor(int i)
	{
		anim.SetInteger ("color", i);
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
			if(ignore!=null)
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
		}
		Object.Destroy (this.gameObject);
	}
	public void setVertexs(Transform first,Transform second)
	{
		LeftConer = first;
		RightConer = second;
	}
}
