using UnityEngine;
using System.Collections;

public class wqLocalSingletonBehaviour<T> : MonoBehaviour where T : wqLocalSingletonBehaviour<T>
{
	private static T _instance;
    protected bool awaken;

    public static bool Inited
    {
        get { return _instance != null; }
    }
 
	//---------------------------------------------------
    public static T Instance {
		get { return wqLocalSingletonBehaviour<T>._instance != null ? wqLocalSingletonBehaviour<T>._instance : wqLocalSingletonBehaviour<T>.Load(); }
		set { wqLocalSingletonBehaviour<T>._instance = value; }
    }
	
 	//---------------------------------------------------
	public static T I {
		get { return Instance; }
	}
	
	//---------------------------------------------------
	public static void Init() {
		if ( wqLocalSingletonBehaviour<T>._instance == null )
			wqLocalSingletonBehaviour<T>.Load();
	}
	
	//---------------------------------------------------
    private static T Load() {
        var instance = (T)FindObjectOfType(typeof(T));
        if ( instance == null )
        {
            var obj = new GameObject(typeof(T).Name);
            instance = obj.AddComponent<T>();
        }

        instance.Awake();
        return instance;
    }
 
	//---------------------------------------------------
    protected virtual void Awake() {
        if (this.awaken)
            return;

        this.awaken = true;
 
		if ( wqLocalSingletonBehaviour<T>._instance != null && wqLocalSingletonBehaviour<T>._instance != this ) {
            Object.Destroy(this.gameObject);
            return;
        }
 
		wqLocalSingletonBehaviour<T>._instance = (T)this;
        this.DoAwake();
    }
 
	//---------------------------------------------------
	public virtual void OnDestroy() {
		if ( wqLocalSingletonBehaviour<T>._instance == this ) {
			wqLocalSingletonBehaviour<T>._instance = null;
            this.DoDestroy();
        }
    }
 
	//---------------------------------------------------
    public virtual void DoAwake() {
    }
 
	//---------------------------------------------------
    public virtual void DoDestroy() {
    }
}