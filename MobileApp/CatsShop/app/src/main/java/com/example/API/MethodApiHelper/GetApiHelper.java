package com.example.API.MethodApiHelper;

import android.app.Activity;

import com.example.API.ApiHelper;
import com.example.API.ConnectConfig;
import com.example.API.ResultOfAPI;

import java.io.BufferedInputStream;
import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.URL;

public class GetApiHelper extends ApiHelper {
    public GetApiHelper(Activity ctx) {
        super(ctx);
    }

    @Override
    protected ResultOfAPI queary(String address, String body, Boolean authorization) throws Exception {


        URL url = new URL(address);
        HttpURLConnection con = (HttpURLConnection) url.openConnection();

        try {

            con.setRequestMethod("GET");
            if (authorization) {
                con.setRequestProperty("accept", "text/plain");


                String token = "Bearer " + ConnectConfig.Token;
                con.setRequestProperty("Authorization", token);

            }



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

    public void SendAutorization(String address)
    {
        send(address, true);
    }

    public void SendNoAutorization(String address)
    {
        send(address, false);
    }

    public void send(String url, Boolean authorization)
    {
        send(url, "", authorization);
    }
}
