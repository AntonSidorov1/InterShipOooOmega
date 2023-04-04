package com.example.Model;

import org.json.JSONException;
import org.json.JSONObject;

public class Gender {
    public int ID = 0;
    public String Name = "";

    public Gender()
    {

    }

    public Gender(String json)
    {
        this();
        SetJson(json);
    }

    public Gender(JSONObject json)
    {
        this();
        SetJson(json);
    }

    public void SetJson(String json)
    {
        try {
            JSONObject object = new JSONObject(json);
            SetJson(object);
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    public void SetJson(JSONObject object)
    {
        try {
            ID = object.getInt("id");
            Name = object.getString("name");
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public String toString() {
        return Name;
    }
}
