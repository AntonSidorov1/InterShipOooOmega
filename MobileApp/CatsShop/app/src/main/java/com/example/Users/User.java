package com.example.Users;

import com.example.DB.Account;

import org.json.JSONObject;

public class User extends Account {

    public User(JSONObject object) {
        super(object);
    }

    public User(String object) {
        super(object);
    }

    public User()
    {
        super();
    }

    public Role Role = new Role();

    @Override
    public void SetJson(JSONObject json) {
        try
        {
            super.SetJson(json);
            Role = new Role(json.getJSONObject("role"));
        }
        catch (Exception e)
        {
            super.SetJson(json);
            Role = new Role();
        }
    }

    @Override
    public String toString() {
        return super.toString() + " - "
                + Role.NameRus + " (" +
                Role.NameEng +")";
    }

    public String GetInfo()
    {
        return "Логин - " + login + "\n" +
                "Пароль - " + password + "\n" +
                "Роль - " + GetRoleRus() + " (" + GetRoleEng() + ")";
    }

    public String GetRoleRus()
    {
        return Role.NameRus;
    }

    public String GetRoleEng()
    {
        return Role.NameEng;
    }
}
