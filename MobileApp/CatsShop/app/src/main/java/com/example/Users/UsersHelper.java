package com.example.Users;

import android.app.Activity;
import android.content.Context;
import android.widget.TextView;

import com.example.API.ConnectConfig;
import com.example.DB.DB;
import com.example.DB.Helper;

public class UsersHelper {


    public static void GetDatas(TextView ulr, TextView login, TextView roleRus, TextView roleEng, Activity context)
    {
        GetURL(ulr, context);
        GetLogin(login, context);
        GetRole(roleRus, roleEng, login, context);
    }


    public static void GetURL(TextView url, Activity context)
    {
        url.setText(Helper.GetUrlAddress(context));
    }


    public static void GetLogin(TextView login, Activity context)
    {
        login.setText("");
        TextView textViewLogin = login;
        String token = ConnectConfig.GetToken(context);
        if(token.length() > 0)
        {
            LoginAPI loginAPI = new LoginAPI(context)
            {
                @Override
                public void on_fail(String req) {
                    DB.GetDB(context).TokenClear();
                }

                @Override
                public void GetLogin(String login) {
                    textViewLogin.setText(login);
                }
            };
            loginAPI.Get(Helper.GetUrlAddress(context) + "/users/get-login");
        }
    }

    public static void GetRole(TextView roleEng, TextView roleRus, TextView login, Activity context)
    {
        roleEng.setText("");
        roleRus.setText("");


        String token = ConnectConfig.GetToken(context);
        if(token.length() > 0) {
            RoleApi roleAPI = new RoleApi(context) {
                @Override
                public void on_fail(String req) {
                    DB.GetDB(context).TokenClear();
                    GetLogin(login, context);
                }

                @Override
                public void GetRole(String nameRus, String nameEng) {
                    roleRus.setText(nameRus);
                    roleEng.setText(nameEng);
                }
            };
            roleAPI.Get(Helper.GetUrlAddress(context) + "/users/get-role");
        }
    }


}
