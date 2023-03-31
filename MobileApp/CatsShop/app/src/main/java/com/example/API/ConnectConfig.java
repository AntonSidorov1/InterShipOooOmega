package com.example.API;
import android.app.Activity;
import android.content.Context;
import android.util.Log;

import androidx.appcompat.app.AlertDialog;

import com.example.DB.Account;
import com.example.DB.DB;
import com.example.DB.Helper;
import com.example.API.Session;

public class ConnectConfig {

    public static Account account;

    public static String Key;

    public static boolean finish = false;

    public static String api_endpoint = "http://v1.fxnode.ru:8084";

    public static boolean input = false;

    public static String Token;

    public static String SetToken(Context context, String token)
    {
        DB.GetDB(context).PutToken(token);
        return GetToken(context);
    }

    public static String GetToken(Context context)
    {
        DB.GetDB(context).GetTokenToConfig();
        return Token;
    }


    public static String LogIn;
    public static String Password;

    //public static Note note;


    public static boolean logOut = false;

    public static boolean saveAccount = false;

    /*
    public static void LogIn(Activity context, Session session)
    {
        api_endpoint = Helper.URL;
        if(LogIn(Helper.Account, context, session))
            Helper.Session = Key;
    }

    public static boolean LogIn(Account account, Activity ctx, Session session)
    {
        api_endpoint = Helper.URL;
        return LogIn(account.login, account.password, ctx, session);
    }

    public static boolean LogIn(String user, String password, Activity context, Session session)
    {
        final boolean[] result = {true};
        com.example.API.ApiHelper abc = new com.example.API.ApiHelper(context)
        {

            public void on_ready(String res) {
                String res1 = "";
                String key = "";
                if (res.equals("null"))
                {
                    res1 = "null";
                }
                else
                {
                    key = res.replace("\"", "");
                    Log.i("result", "key - " + key);
                    res1 = ""+res+"";
                }
                Log.e("result", res);

                String finalRes = res1;
                ConnectConfig.Key = key;

                //ConnectConfig.Key = abc.res;

                String message = "";
                String title = "";
                if(finalRes == null || finalRes == "null" || Helper.NullText(finalRes))
                {
                    result[0] = false;
                    session.SetErrorSession();
                }
                else if(res == null || res == "null" || Helper.NullText(res))
                {

                    result[0] = false;
                    session.SetErrorSession();
                }
                else
                {

                    session.SetSession(key);
                }



            }

            @Override
            public void on_fail() {
                session.SetErrorSession();
            }
        };
        abc.send(api_endpoint +"/rpc/sign_in",
                "{ \"name\": \"" + user + "\", \"secret\": \"" + password + "\"}",       true);
        return result[0];
    }
*/
}
