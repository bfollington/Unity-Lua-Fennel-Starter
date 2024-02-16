using NLua;
using UnityEngine;

public class Bridge
{
    public Lua lua;

    public Game game;

    public Bridge(Lua lua)
    {
        this.lua = lua;
        InitModules();
        SetupLuaInstance();
    }

    void InitModules()
    {
        game = new Game();
    }

    void SetupLuaInstance()
    {
        lua["Game"] = game;
        lua["GameObject"] = new object();
        lua["State"] = new object();
        lua["deltaTime"] = Time.deltaTime;
        lua["obj"] = new object();
    }
}
