package com.example.Authofication;

import android.app.Activity;
import android.content.DialogInterface;
import android.util.Log;
import android.widget.Toast;

import androidx.appcompat.app.AlertDialog;

import com.example.API.ApiHelper;
import com.example.API.ConnectConfig;
import com.example.API.ResultOfAPI;

import org.json.JSONObject;

public class SignIn extends ApiHelper {

    public SignIn(Activity ctx) {
        super(ctx);
    }

    public void EndSend()
    {

    }

    public void send(String url, String login, String password)
    {
        String body = "{" +
                "\"login\": \""+login+"\", " +
                "\"password\": \""+password+"\"" +
                "}";
        SendNoAutorization(url, body, "POST");
    }

    @Override
    public void on_fail(String req) {
        String message = "Неудалось войти в систему";
        ResultOfAPI res = new ResultOfAPI();
        res.Body = ErrorMessage();
        res.URL = req;
        on_fail(res, message);

    }

    @Override
    public void ready_result(ResultOfAPI res) throws Exception {
        try {
            if(res.Code == 500)
            {
                res.Body = ErrorMessage();
                on_fail(res, "Неудалось войти в систему");
                return;
            }

            if(res.Code != 200 || res.equals("null"))
            {
                throw new Exception();
            }

            if (res == null) {
                throw new Exception();
            }

            JSONObject json = new JSONObject(res.Body);
            String token = json.getString("authToken");
            Log.e("result", token);

            ConnectConfig.SetToken(GetActivity(), token);

            AlertDialog.Builder dialog = new AlertDialog.Builder(GetActivity());
            dialog.setTitle("Вы успешно вошли в систему");
            dialog.setPositiveButton("ОК", new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    EndSend();
                }
            });
            AlertDialog dlg = dialog.create();

                dlg.setMessage("Вы успешно вошли в систему");
            dlg.setCancelable(false);
            dlg.show();

            Toast.makeText(GetActivity(), "Вы успешно вошли в систему", Toast.LENGTH_SHORT);
        }
        catch(Exception e)
        {
            res.Body = "Неверный логин или пароль";
            throw new Exception("Неудалось авторизироваться");
        }



    }

    @Override
    public void on_fail(ResultOfAPI res, String message) {

        AlertDialog.Builder dialog = new AlertDialog.Builder(GetActivity());
        dialog.setTitle(message);
        AlertDialog dlg = dialog.create();
        if(res.Body != null) {
            dlg.setMessage(res.Body);
        }
        dlg.show();

        Toast.makeText(GetActivity(), message, Toast.LENGTH_SHORT);
    }
}
