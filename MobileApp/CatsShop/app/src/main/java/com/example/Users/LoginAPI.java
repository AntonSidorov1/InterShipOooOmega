package com.example.Users;

import android.app.Activity;

import com.example.API.ApiClient;
import com.example.API.ResultOfAPI;

public class LoginAPI extends ApiClient {
    public LoginAPI(Activity ctx) {
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
        GetLogin(res.Body);
    }

    public void GetLogin(String login)
    {

    }

    @Override
    public void on_fail(ResultOfAPI req, String message) {
        on_fail(req.URL);
    }
}
