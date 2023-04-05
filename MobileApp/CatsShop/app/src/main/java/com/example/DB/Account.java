package com.example.DB;

import org.json.JSONException;
import org.json.JSONObject;

public class Account {

    public String login = "";
    public String password = "";

    public int ID = 0;

    @Override
    public String toString() {
        return login;
    }

    public Account Copy()
    {
        Account account = new Account();
        account.login = login;
        account.ID = ID;
        account.password = password;
        return account;
    }

    public Account()
    {

    }


    public Account(JSONObject object)
    {
        this();
        SetJson(object);
    }

    public Account(String object)
    {
        this();
        SetJson(object);
    }

    public void SetJson(JSONObject json)
    {
        try {
            JSONObject object = json;
            login = object.getString("login");
            password = object.getString("password");
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

    public String GetJson()
    {
        try {
            JSONObject object = new JSONObject();
            object.put("login", login);
            object.put("password", password);
            return object.toString();
        }
        catch (Exception e)
        {
            return "";
        }
    }


}
