package com.example.API;

import android.app.Activity;

import com.example.API.ResultOfAPI;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.URL;
import  com.example.API.ConnectConfig;

public class ApiHelper
{
    public Activity ctx;

    public Activity GetActivity()
    {
        return ctx;
    }

    public void SetActivity(Activity ctx)
    {
        this.ctx = ctx;
    }

    public ApiHelper(Activity ctx)
    {
        this.ctx = ctx;
    }

    public void on_ready(ResultOfAPI res)
    {
        try {
            ready_result(res);
        } catch (Exception e) {
            e.printStackTrace();
            on_fail(res, e.getMessage());
        }
    }

    public void ready_result(ResultOfAPI res) throws Exception
    {

    }

    public void on_fail(ResultOfAPI res, String message)
    {

    }

    public void on_fail()
    {
    }
/*
    String http_get(String reg, String payload, , String method) throws IOException {
        return http_get(reg, payload, "POST");
    }
    */

    ResultOfAPI http_get(String req, String payload, String method, Boolean authorization) throws IOException
    {
        URL url = new URL(req);
        HttpURLConnection con = (HttpURLConnection) url.openConnection();

        byte[] outmsg = payload.getBytes("utf-8");

        con.setRequestMethod(method);
        con.setRequestProperty("Content-Type", "application/json");
        con.setRequestProperty("Content-Length", String.valueOf(outmsg.length));

        if(authorization)
        {
            String token = "Bearer "+ ConnectConfig.Token;
            con.setRequestProperty("Authorization", token);
        }

        con.setDoOutput(true);
        con.setDoInput(true);

        BufferedOutputStream out = new BufferedOutputStream(con.getOutputStream());
        if(!method.equals("GET")) {
            out.write(outmsg);
        }
        out.flush();

        int code = con.getResponseCode();

        BufferedInputStream inp = new BufferedInputStream(con.getInputStream());

        byte[] buf = new byte[512];
        String res = "";

        while (true)
        {
            int num = inp.read(buf);
            if (num < 0) break;

            res += new String(buf, 0, num);
        }

        con.disconnect();

        ResultOfAPI api = new ResultOfAPI();
        api.Code = code;
        api.Body = res;

        return api;
    }


    public ResultOfAPI res;

    public ApiHelper GetAPIHelper()
    {
        return  this;
    }

    public class NetOp implements Runnable
    {
        public String req, payload, method;
        public Boolean authorization;

        public void run()
        {
            try
            {
                final ResultOfAPI res = http_get(req, payload, method, authorization);

                ctx.runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        on_ready(res);
                    }
                });
                GetAPIHelper().res = res;
            }
            catch (Exception ex)
            {
                ctx.runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        on_fail();
                    }
                });
            }
        }
    }

    public void SendAutorization(String req, String payload, String method)
    {
        send(req, payload, method, true);
    }

    public void SendNoAutorization(String req, String payload, String method)
    {
        send(req, payload, method, false);
    }

    public void send(ApiParameters parameters)
    {
        String method = parameters.Method;
        String address = parameters.GetURL();
        String body = parameters.Body;
        Boolean authorization = parameters.Authorization;
        send(address, body, method, authorization);
    }

    public void send(String req, String payload, String method, Boolean authorization)
    {
        send(req, payload, method, authorization, false);
    }

    public void send(String req, String payload, String method, Boolean authorization, boolean stop)
    {
        NetOp nop = new NetOp();
        nop.req = req;
        nop.payload = payload;
        nop.method = method;
        nop.authorization = authorization;

        Thread th = new Thread(nop);
        th.start();
        if(stop) {
            try {
                th.join();
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }

}
