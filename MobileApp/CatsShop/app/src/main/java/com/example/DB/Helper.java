package com.example.DB;

import android.app.Activity;
import android.content.Context;

import com.example.URL.URL;
import com.example.API.ConnectConfig;
import com.example.API.Session;
import com.example.URL.URL;


public class Helper {


    public static AccountList accounts;

    public static Account Account = new Account();

    public static URL URL = new URL("https://192.168.0.15:7073/api");

    public static URL GetURL(Context context)
    {
        DB.GetDB(context).GetUrl();
        return URL;
    }

    public static String GetUrlAddress(Context context)
    {
        return GetURL(context).GetURL();
    }

    public static String Session = "";

    public static Boolean NullText(String text)
    {
        try {
            return text == "" || text.equals("") || text == null;
        }
        catch(Exception ex)
        {
            return true;
        }
    }

    public static Boolean NullSession()
    {
        return NullText(Session);
    }

    public static DB GetDB(Context context)
    {
        DB db= DB.GetDB(context);
        db.GetUrl();
        db.GetAccount();
        return db;
    }

    /*
    public static String GetSession(Activity ctx, com.example.API.Session session)
    {
        ConnectConfig.LogIn(ctx, session);
        try {
            if(NullText(Session))

                    throw new Exception();
            return Session;
        }
        catch (Exception e)
        {
            Session = "";
            return Session;
        }
    }
*/

    public static Account AccountBuffer;

}
