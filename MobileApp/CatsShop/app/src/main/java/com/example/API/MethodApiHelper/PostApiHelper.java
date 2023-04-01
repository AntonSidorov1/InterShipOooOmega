package com.example.API.MethodApiHelper;

import android.app.Activity;

import com.example.API.ApiHelper;
import com.example.API.ConnectConfig;
import com.example.API.ResultOfAPI;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.URL;

public class PostApiHelper extends ApiHelper {
    public PostApiHelper(Activity ctx) {
        super(ctx);
    }


    @Override
    protected ResultOfAPI queary(String address, String body, Boolean authorization) throws Exception
    {
        URL url = new URL(address);
        HttpURLConnection con = (HttpURLConnection) url.openConnection();

        try {

            con.setRequestMethod("POST");
            if (authorization) {
                con.setRequestProperty("accept", "text/plain");


                String token = "Bearer " + ConnectConfig.Token;
                con.setRequestProperty("Authorization", token);

            }

            con.setRequestProperty("Content-Type", "application/json");
            //con.setRequestProperty("Content-Length", String.valueOf(outmsg.length));

            con.setDoOutput(true);
            con.setDoInput(true);

            SetStringToBody(con, body);

            int code = con.getResponseCode();
            String res = GetStringFromBuffer(new BufferedInputStream(con.getInputStream()));

            con.disconnect();

            ResultOfAPI api = new ResultOfAPI();
            api.Code = code;
            api.Body = res;
            api.URL = address;

            return api;

        }
        catch (Exception e)
        {
            con.disconnect();
            throw e;
        }
    }
}
