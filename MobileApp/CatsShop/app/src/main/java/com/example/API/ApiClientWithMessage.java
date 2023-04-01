package com.example.API;

import android.app.Activity;
import android.content.DialogInterface;
import android.widget.Toast;

import androidx.appcompat.app.AlertDialog;

public class ApiClientWithMessage extends ApiClient{
    public ApiClientWithMessage(Activity ctx) {
        super(ctx);
    }

    public String TitleMessage = "";
    public String MessageReady = "";
    public String MessageFail = "";


    @Override
    public void ready_result(ResultOfAPI res) throws Exception {
        int code = res.Code;
        if(code != 200)
        {
            if(code == 500)
            {
                on_fail(res.URL);
                return;
            }
            else
            {
                res.Body = MessageFail;
                throw new Exception(TitleMessage);
            }
        }

        AlertDialog.Builder dialog = new AlertDialog.Builder(GetActivity());
        dialog.setTitle(TitleMessage);
        dialog.setPositiveButton("ОК", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                GetResultReady1(res);
            }
        });
        AlertDialog dlg = dialog.create();

        dlg.setMessage(MessageReady);
        dlg.setCancelable(false);
        dlg.show();

        Toast.makeText(GetActivity(), MessageReady, Toast.LENGTH_SHORT);
    }

    public void GetResult(ResultOfAPI res)
    {

    }

    public void GetResultReady(ResultOfAPI res)
    {

    }

    public void GetResultReady1(ResultOfAPI res)
    {
        GetResultReady(res);
        GetResult(res);
    }

    public void GetResultFail(ResultOfAPI res)
    {

    }

    public void GetResultFail1(ResultOfAPI res)
    {
        GetResultFail(res);
        GetResult(res);
    }

    @Override
    public void on_fail(String req) {
        String message = TitleMessage;
        ResultOfAPI res = new ResultOfAPI();
        res.Body = ErrorMessage();
        res.URL = req;
        on_fail(res, message);

    }

    @Override
    public void on_fail(ResultOfAPI res, String message) {

        AlertDialog.Builder dialog = new AlertDialog.Builder(GetActivity());
        dialog.setTitle(message);
        dialog.setCancelable(false);
        dialog.setPositiveButton("OK", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                GetResultFail1(res);
            }
        });
        AlertDialog dlg = dialog.create();
        if(res.Body != null) {
            dlg.setMessage(res.Body);
        }
        dlg.show();

        Toast.makeText(GetActivity(), res.Body, Toast.LENGTH_SHORT);
    }
}
