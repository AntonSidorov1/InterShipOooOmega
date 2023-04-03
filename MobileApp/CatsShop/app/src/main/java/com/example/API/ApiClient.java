package com.example.API;

import android.app.Activity;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.URL;

public class ApiClient
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

    public ApiClient(Activity ctx)
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

    public static String ErrorMessage() {
        return "В данный момент существуют проблемы с приложением \n" +
                "   - Проверьте подключение к сети \n" +
                "   - Обратитесь в службу поддержки";
    }

    protected ResultOfAPI queary(String address, String body, String method, Boolean authorization) throws Exception {
        if (method.equals("GET")) {
            return methodGet(address, authorization);
        } else if (method.equals("POST")) {
            return methodPost(address, body, authorization);
        } else if (method.equals("PUT")) {
            return methodPut(address, body, authorization);
        } else if (method.equals("PATCH")) {
            return methodPatch(address, body, authorization);
        } else if (method.equals("DELETE")) {
            return methodDelete(address, authorization);
        } else {
            return new ResultOfAPI();
        }
    }

    public static String GetStringFromBuffer(HttpURLConnection con)
    {
        try
        {
            return GetStringFromBuffer(new BufferedInputStream(con.getInputStream()));
        }
        catch (Exception e)
        {
            return "";
        }
    }

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

    public ApiClient GetAPIHelper()
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
                final ResultOfAPI res = queary(req, payload, method, authorization);

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

    public void SendAutorization(String address, String method, String body)
    {
        send(address, body, method, true);
    }

    public void SendNoAutorization(String address, String method, String body)
    {
        send(address, body, method, false);
    }

    public void send(ApiParameters parameters)
    {
        String address = parameters.GetURL();
        String body = parameters.Body;
        Boolean authorization = parameters.Authorization;
        send(address, body, parameters.Method, authorization);
    }

    public void send(String address, String body, String method, Boolean authorization)
    {
        send(address, body, method, authorization,false);
    }

    public void send(String req, String payload, String method, Boolean authorization, boolean stop)
    {
        NetOp nop = new NetOp();
        nop.req = req;
        nop.payload = payload;
        nop.authorization = authorization;
        nop.method = method;

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

    public void GET(String address, Boolean authorization)
    {
        send(address, "", "GET", authorization);
    }

    public void DELETE(String address, Boolean authorization)
    {
        send(address, "", "DELETE", authorization);
    }

    public void POST(String address, String body, Boolean authorization)
    {
        send(address, body, "POST", authorization);
    }

    public void PUT(String address, String body, Boolean authorization)
    {
        send(address, body, "PUT", authorization);
    }

    public void PATCH(String address, String body, Boolean authorization)
    {
        send(address, body, "PATCH", authorization);
    }

    protected void authorize(HttpURLConnection con, Boolean authorization)
    {
        if (authorization) {
            con.setRequestProperty("accept", "text/plain");


            String token = "Bearer " + ConnectConfig.Token;
            con.setRequestProperty("Authorization", token);

        }
    }

    protected ResultOfAPI methodWithOutBody(String address, String method, Boolean authorization) throws Exception
    {
        URL url = new URL(address);
        HttpURLConnection con = (HttpURLConnection) url.openConnection();

        try {

            con.setRequestMethod(method);

            authorize(con, authorization);

            int code = GetResponseCode(con);
            String res = GetStringFromBuffer(con);

            con.disconnect();
            return GetResult(address, res, code);

        }
        catch (Exception e)
        {
            con.disconnect();
            throw e;
        }
    }

    public static ResultOfAPI GetResult(String url, String body, int code)
    {
        ResultOfAPI api = new ResultOfAPI();
        api.Code = code;
        api.Body = body;
        api.URL = url;
        return api;
    }

    public static int GetResponseCode(HttpURLConnection con) throws Exception {
        return con.getResponseCode();
    }

    protected ResultOfAPI methodGet(String address, Boolean authorization) throws Exception {
        return methodWithOutBody(address, "GET", authorization);
    }

    protected ResultOfAPI methodDelete(String address, Boolean authorization) throws Exception {
        return methodWithOutBody(address, "DELETE", authorization);
    }

    protected ResultOfAPI methodWithBody(String address, String body, String method, Boolean authorization) throws Exception
    {
        URL url = new URL(address);
        HttpURLConnection con = (HttpURLConnection) url.openConnection();

        try {

            con.setRequestMethod(method);

            authorize(con, authorization);

            con.setRequestProperty("Content-Type", "application/json");

            con.setDoOutput(true);
            con.setDoInput(true);

            SetStringToBody(con, body);

            int code = GetResponseCode(con);
            String res = GetStringFromBuffer(con);

            con.disconnect();
            return GetResult(address, res, code);

        }
        catch (Exception e)
        {
            con.disconnect();
            throw e;
        }
    }

    protected ResultOfAPI methodPost(String address, String body, Boolean authorization) throws Exception
    {
        return methodWithBody(address, body, "POST", authorization);
    }

    protected ResultOfAPI methodPut(String address, String body, Boolean authorization) throws Exception
    {
        return methodWithBody(address, body, "PUT", authorization);
    }

    protected ResultOfAPI methodPatch(String address, String body, Boolean authorization) throws Exception
    {
        return methodWithBody(address, body, "PATCH", authorization);
    }


}
