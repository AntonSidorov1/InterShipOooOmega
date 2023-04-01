package com.example.API;

import android.app.Activity;
import android.util.Log;

import com.example.API.ResultOfAPI;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.HashMap;
import java.util.Map;

import  com.example.API.ConnectConfig;

public abstract class ApiHelper
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

    public void on_fail(String req)
    {
    }
/*
    String http_get(String reg, String payload, , String method) throws IOException {
        return http_get(reg, payload, "POST");
    }
    */

    public String ErrorMessage() {
        return "В данный момент существуют проблемы с приложением \n" +
                "   - Проверьте подключение к сети \n" +
                "   - Обратитесь в службу поддержки";
    }

    protected abstract ResultOfAPI queary(String address, String body, Boolean authorization) throws Exception;
   /* {
        String method = "GET";
        String req = address;
        String payload = body;
        URL url = new URL(req);
		// url.given(); - Возможный вариант для авторизации и Headers
        HttpURLConnection con = (HttpURLConnection) url.openConnection();

        try {
            byte[] outmsg = payload.getBytes("utf-8");

            if (authorization) {
                con.setRequestProperty("accept", "text/plain");


                String token = "Bearer " + ConnectConfig.Token;
                con.setRequestProperty("Authorization", token);

            }

            con.setRequestMethod(method);
                con.setRequestProperty("Content-Type", "application/json");
            con.setRequestProperty("Content-Length", String.valueOf(outmsg.length));

            con.setDoOutput(true);
            con.setDoInput(true);

            BufferedOutputStream out = new BufferedOutputStream(con.getOutputStream());
            if (!method.equals("GET") && !method.equals("DELETE")) {
                out.write(outmsg);
            }

            out.flush();

            int code = con.getResponseCode();
            String res = "";

            try {
                BufferedInputStream inp = new BufferedInputStream(con.getInputStream());

                byte[] buf = new byte[512];

                while (true) {
                    int num = inp.read(buf);
                    if (num < 0) break;

                    res += new String(buf, 0, num);
                }
            } catch (Exception e) {
                res = e.getMessage();
            }

            con.disconnect();

            ResultOfAPI api = new ResultOfAPI();
            api.Code = code;
            api.Body = res;
            api.URL = req;

            return api;
        }
        catch(Exception e)
        {
            con.disconnect();
            throw e;
        }
    }
    */

    public static String GetStringFromBuffer(BufferedInputStream inp)
    {
        String res = "";
        try {

            byte[] buf = new byte[512];

            while (true) {
                int num = inp.read(buf);
                if (num < 0) break;

                res += new String(buf, 0, num);
            }
        } catch (Exception e) {
            res = e.getMessage();
        }
        return res;
    }

    public static void SetStringToBody(HttpURLConnection con, String text) throws Exception {
        byte[] outmsg = text.getBytes("utf-8");
        con.setRequestProperty("Content-Length", String.valueOf(outmsg.length));
        BufferedOutputStream out = new BufferedOutputStream(con.getOutputStream());
        out.write(outmsg);
        out.flush();
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
                final ResultOfAPI res = queary(req, payload, authorization);

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
                        on_fail(req);
                    }
                });
            }
        }
    }

    public void SendAutorization(String address, String body)
    {
        send(address, body, true);
    }

    public void SendNoAutorization(String address, String body)
    {
        send(address, body, false);
    }

    public void send(ApiParameters parameters)
    {
        String address = parameters.GetURL();
        String body = parameters.Body;
        Boolean authorization = parameters.Authorization;
        send(address, body, authorization);
    }

    public void send(String address, String body, Boolean authorization)
    {
        send(address, body, authorization, false);
    }

    public void send(String req, String payload, Boolean authorization, boolean stop)
    {
        NetOp nop = new NetOp();
        nop.req = req;
        nop.payload = payload;
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
