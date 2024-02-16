using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectApi {
    private readonly GameObject _obj;

    public GameObjectApi(GameObject obj) {
        _obj = obj;
    }
    
    public void position(float x, float y, float z) {
        _obj.transform.position = new Vector3(x, y, z);
    }
}

public class FennelBehaviour : MonoBehaviour {
    public Guid Id = Guid.NewGuid();
    public NLuaController Controller;
    public TextAsset script;

    private GameObjectApi _api;

    private dynamic _state = new { };
    private object _obj;

    void Awake() {
        _api = new GameObjectApi(gameObject);
    }
    
    private void Start() {
        Controller.lua.DoFnlString(script.text);
        _obj = Controller.lua["obj"];
        
        Controller.lua["deltaTime"] = Time.deltaTime;
        Controller.lua["GameObject"] = _api;
        Controller.lua["State"] = _state;
        Controller.lua["obj"] = _obj;
        Controller.Run($"obj.init(\"{Id.ToString()}\")");
        
        _state = Controller.lua["State"];
    }

    // Update is called once per frame
    void Update()
    {
        Controller.lua["deltaTime"] = Time.deltaTime;
        Controller.lua["GameObject"] = _api;
        Controller.lua["State"] = _state;
        Controller.lua["obj"] = _obj;
        Controller.Run($"obj.update(\"{Id.ToString()}\")");
        
        _state = Controller.lua["State"];
    }
}
