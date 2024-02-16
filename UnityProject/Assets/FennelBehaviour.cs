using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

interface IMessage {
    public string id { get; }
}

class ResetMessage : IMessage {
    public static string Id = "reset";
    public string id => Id;
}

public class GameObjectApi {
    private readonly GameObject _obj;

    public GameObjectApi(GameObject obj) {
        _obj = obj;
    }
    
    public void position(float x, float y, float z) {
        _obj.transform.position = new Vector3(x, y, z);
    }
}

[Serializable]
public class TestState {
    public float time = 0;
    public float speed = 1;
}

public class FennelBehaviour : MonoBehaviour {
    public Guid Id = Guid.NewGuid();
    public NLuaController Controller;
    public TextAsset script;

    private GameObjectApi _api;

    private object _obj;

    public TestState state = new();

    [Button]
    public void Reset() {
        OnReceive(new ResetMessage());
    }

    void Awake() {
        _api = new GameObjectApi(gameObject);
    }

    private void OnReceive<T>(T msg) where T : IMessage {
        Controller.lua["GameObject"] = _api;
        Controller.lua["deltaTime"] = Time.deltaTime;
        Controller.lua.DoFnlString(script.text);
        Controller.lua["arg1"] = Id.ToString();
        Controller.lua["arg2"] = msg.id;
        Controller.lua["arg3"] = msg;
        Controller.lua["arg4"] = state;
        Controller.lua.DoString("receive(_G.arg1, _G.arg2, _G.arg3, _G.arg4)");
    }
    
    private void Start() {
        Controller.lua["GameObject"] = _api;
        Controller.lua["deltaTime"] = Time.deltaTime;
        Controller.lua.DoFnlString(script.text);
        Controller.lua["arg1"] = Id.ToString();
        Controller.lua.DoString("init(_G.arg1)");
    }

    // Update is called once per frame
    void Update()
    {
        Controller.lua["GameObject"] = _api;
        Controller.lua["deltaTime"] = Time.deltaTime;
        // TODO: this is stupid, need to cache the result
        Controller.lua.DoFnlString(script.text);
        Controller.lua["arg1"] = Id.ToString();
        Controller.lua["arg2"] = state;
        Controller.lua.DoString("update(_G.arg1, _G.arg2)");
    }
}
