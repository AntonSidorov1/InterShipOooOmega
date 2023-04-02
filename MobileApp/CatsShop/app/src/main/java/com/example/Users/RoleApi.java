package com.example.Users;

import android.app.Activity;

import com.example.API.ApiClient;
import com.example.API.ResultOfAPI;

import org.json.JSONObject;

public class RoleApi extends ApiClient {
    public RoleApi(Activity ctx) {
        super(ctx);
    }

    public void Get(String url)
    {
        GET(url, true);
    }


    @Override
    public void ready_result(ResultOfAPI res) throws Exception {
        super.ready_result(res);

        if(res.Code != 200)
        {
            throw new Exception();
        }
        if(res.Body.equals("null") || res.Body.equals(""))
        {
            throw new Exception();
        }

        Role role = new Role();

        JSONObject json = new JSONObject(res.Body);
        role.NameRus = json.getString("roleRus");
        role.NameEng = json.getString("roleEng");

        GetRole(role);
    }

    public void GetRole(Role role)
    {
        GetRole(role.NameRus, role.NameEng, role);
    }

    public void GetRole(String nameRus, String nameEng, Role role)
    {

    }

    @Override
    public void on_fail(ResultOfAPI req, String message) {
        on_fail(req.URL);
    }
}
