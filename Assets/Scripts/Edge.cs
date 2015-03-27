using UnityEngine;
using System.Collections;

public class Edge : MonoBehaviour {

	public Transform LeftConer=null;
	public Transform RightConer;

	public Vector3 rotation;
	public float DistanceToScele;
	public GameObject clone_of_Weight_Canvas;
	public GameObject clone_of_Stream_Canvas;
	public int Stream_state=4;
	public Canvas_Text_Field weight;
	public Canvas_Text_Field stream;
	public bool isCheked = false;
	private bool BlockStreamAnimation=false;
	private Recorder record;
	private Animator anim;
	private int _rightleft=0;
	private static int Index=1;
	// Update is called once per frame
	void Awake()
	{
		anim = GetComponent<Animator> ();
		record = GameObject.FindGameObjectWithTag ("Recorder").GetComponent<Recorder> ();
		weight = (Object.Instantiate (clone_of_Weight_Canvas,
		                              new Vector3(-10000,-10000,-10000),
		                              Quaternion.Euler(0,0,90f)
		                              )as GameObject).GetComponent<Canvas_Text_Field> ();
		weight.setValue (Index);
		stream = (Object.Instantiate (clone_of_Stream_Canvas,
		                              new Vector3(-10000,-10000,-10000),
		                              Quaternion.Euler(0,0,90f)
		                              )as GameObject).GetComponent<Canvas_Text_Field> ();

		Index++;
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
		//print (i + " ver");
		int mov;
		if (ver.Index == LeftConer.gameObject.GetComponent<Vertex> ().Index)
			mov = -2;
		else
			mov = 2;
		//print (i + " ver "+mov);
		if (record.isCreateRecord ()) 
		{
			if(BlockStreamAnimation)
			{
				return;
			}
			print(i+" "+mov+" "+BlockStreamAnimation.ToString());
			record.Add (this, i, mov);
		}
		else
		{
			//print("Edge:"+i+","+mov);
			anim.SetInteger ("color", i);
			anim.SetInteger ("Left_Right", mov);
		}
	}
	public void setColor(int i,int mov)
	{
		if (record.isCreateRecord ()) 
		{
			if(BlockStreamAnimation)
			{
				return;
			}
			print(i+" "+mov+" "+BlockStreamAnimation.ToString());
			record.Add (this, i, mov);
		}
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
		stream.LeftConer = LeftConer;
		stream.RightConer = RightConer;
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
	public int getIndex(int index)
	{
		if (index == LeftConer.gameObject.GetComponent<Vertex> ().Index)
			return RightConer.gameObject.GetComponent<Vertex> ().Index;
		else
			return LeftConer.gameObject.GetComponent<Vertex> ().Index;
	}
	public void unCheked()
	{
		isCheked = false;
	}
	public void setStream(int s)
	{
		stream.value = s;
		if(record.isCreateRecord())
		{
			string text=s.ToString();
			record.Add (this,text );
		}
		else
		{
			//distance = d;
			stream.inputField.text=s.ToString();
		}
	}
	public void setStream(string text)
	{
		stream.inputField.text = text;
	}
	public void hideStreamField()
	{
		stream.value = 0;
		stream.inputField.text = "";
		BlockStreamAnimation = false;
	}
	public void showStreamField()
	{
		stream.value = 0;
		stream.inputField.text = "0";
	}
	private bool isRightStream;
	public int stream_get(Vertex begin_ver)
	{
		if (stream.value == 0)
			return weight.value;
		if (begin_ver.Index == LeftConer.gameObject.GetComponent<Vertex> ().Index)
		{
			if(isRightStream)
			{
				return weight.value-stream.value;
			}
			else
			{
				return stream.value;
			}
		}
		else
		{
			if(!isRightStream)
			{
				return weight.value-stream.value;
			}
			else
			{
				return stream.value;
			}
		}
	}
	public void stream_set(Vertex begin_ver,int s)
	{
		if(s==0)
		{
			print ("Something Strange:"+s);
			//return;
		}
		if(stream.value==0)
		{
			if(s==0)
				return;
			if (begin_ver.Index == LeftConer.gameObject.GetComponent<Vertex> ().Index)
			{
				isRightStream=true;
			}
			else
			{
				isRightStream=false;
			}
			if(s>0&&s<=this.weight.value)
			{
				setStream(s);
				//print("0+"+s);
			}
			else
			{
				print ("Something Strange ... "+s); 
			}
			setColor(Stream_state,begin_ver);
			BlockStreamAnimation=true;
			return;
		}
		if (begin_ver.Index == LeftConer.gameObject.GetComponent<Vertex> ().Index)
		{
			//print (stream.value+(isRightStream? "+":"-")+s);
			s=stream.value+(isRightStream? s:-s);
		}
		else
		{
			//print (stream.value+(isRightStream? "-":"+")+s);
			s=stream.value+(isRightStream?-s: s);
		}
		if(s==0)
		{
			setStream(s);
			//print("stream:"+s);
			BlockStreamAnimation=false;
			this.setColor(0,0);
			return;
		}
		if(s>0&&s<=this.weight.value)
		{
			setStream(s);
		}
		else
		{
			print ("Something Strange ... "+s); 
		}
	}
	//
}
