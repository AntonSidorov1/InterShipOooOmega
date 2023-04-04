package com.example.Users;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.Set;

public class Role {

    public String NameEng = "";
    public String NameRus = "";

    public Role()
    {

    }

    public Role(JSONObject object)
    {
        this();
        SetJson(object);
    }

    public Role(String object)
    {
        this();
        SetJson(object);
    }

    public void SetJson(JSONObject json)
    {
        try {
            JSONObject object = json;
            NameEng = object.getString("roleEng");
            NameRus = object.getString("roleRus");
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    public void SetJson(String json)
    {
        try {
            SetJson(new JSONObject(json));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

}
