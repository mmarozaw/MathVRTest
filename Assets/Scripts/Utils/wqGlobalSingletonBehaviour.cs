using UnityEngine;
using System.Collections;

public class wqGlobalSingletonBehaviour<T> : MonoBehaviour where T : wqGlobalSingletonBehaviour<T>
{
    private static bool _destroyed;
    private static T _instance;
    protected bool awaken;
 
	public static bool Inited
	{
		get { return _instance != null; }
	}

	//---------------------------------------------------
    public static T Instance {
        get	{ return wqGlobalSingletonBehaviour<T>._instance != null || _destroyed ? wqGlobalSingletonBehaviour<T>._instance : wqGlobalSingletonBehaviour<T>.Load(); }
        set { wqGlobalSingletonBehaviour<T>._instance = value; }
    }
	
 	//---------------------------------------------------
	public static T I {
		get { return Instance; }
	}
 
	//---------------------------------------------------
	public static void Init() {
		if ( wqGlobalSingletonBehaviour<T>._instance == null )
			wqGlobalSingletonBehaviour<T>.Load();
	}
	
	//---------------------------------------------------
    private static T Load() {
        var inst = (T)FindObjectOfType(typeof(T));
        if ( inst == null ) {
            var obj = new GameObject(typeof(T).Name);
            inst = obj.AddComponent<T>();
        }

        inst.Awake();
        return inst;
    }
 	
	//---------------------------------------------------
    public virtual void Awake()
    {
        if (this.awaken)
            return;

		this.awaken = true;
 
        if ( wqGlobalSingletonBehaviour<T>._instance != null && wqGlobalSingletonBehaviour<T>._instance != this ) {
            Object.Destroy(this.gameObject);
            return;
        }
 
        wqGlobalSingletonBehaviour<T>._instance = (T)this;
        Object.DontDestroyOnLoad(this.gameObject);
        this.DoAwake();
    }
 
	//---------------------------------------------------
    public void OnDestroy() {
        if ( wqGlobalSingletonBehaviour<T>._instance == this ) {
            _destroyed = true;
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