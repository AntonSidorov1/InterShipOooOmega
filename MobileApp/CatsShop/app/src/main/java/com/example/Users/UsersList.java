package com.example.Users;

import org.json.JSONArray;
import org.json.JSONException;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

public class UsersList extends ArrayList<User> {
    public UsersList()
    {

    }

    public UsersList(Collection<? extends User> users)
    {
        addAll(users);
    }

    public UsersList(JSONArray array)
    {
        this();
        SetJson(array);
    }

    public UsersList(String json)
    {
        this();
        SetJson(json);
    }

    public void SetJson(JSONArray array)
    {
        clear();
        for(int i = 0; i < array.length(); i++)
        {
            try {
                add(new User(array.getJSONObject(i)));
            } catch (JSONException e) {
                e.printStackTrace();
            }
        }
    }

    public void SetJson(String json)
    {
        try {
            SetJson(new JSONArray(json));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    public static UsersList GetFromJson(JSONArray json)
    {
        return new UsersList(json);
    }

    public static UsersList GetFromJson(String json)
    {
        return new UsersList(json);
    }
}
