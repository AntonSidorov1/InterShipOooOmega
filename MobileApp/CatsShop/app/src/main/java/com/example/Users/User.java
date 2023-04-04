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

    public Role Role;

    @Override
    public void SetJson(JSONObject json) {
        try
        {
            JSONObject object = json;
            login = object.getString("login");
            password = object.getString("password");
            this.Role = new Role();
            this.Role.NameEng = object.getString("roleEng");
            this.Role.NameRus = object.getString("roleRus");
        }
        catch (Exception e)
        {
            super.SetJson(json);
            this.Role = new Role();
        }
    }

    @Override
    public String toString() {
        return super.toString() + " - "
                + GetRoleRus() + " (" +
                GetRoleEng() +")";
    }

    public String GetInfo()
    {
        return "Логин - " + login + "\n" +
                "Пароль - " + password + "\n" +
                "Роль - " + GetRoleRus() + " (" + GetRoleEng() + ")";
    }

    public String GetRoleRus()
    {
        return this.Role.NameRus;
    }

    public String GetRoleEng()
    {
        return this.Role.NameEng;
    }
}
