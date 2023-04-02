package com.example.Users;

import android.app.Activity;
import android.content.Context;
import android.widget.EditText;
import android.widget.TextView;

import com.example.API.ConnectConfig;
import com.example.DB.DB;
import com.example.DB.Helper;

public class UsersHelper {

    public static void GetDatas(Activity context)
    {
        TextView view = new EditText(context);
        GetDatas(view, view, view, view, context);
    }

    public static void GetDatas(TextView ulr, TextView login, TextView roleRus, TextView roleEng, Activity context)
    {
        GetURL(ulr, context);

        if(!DB.GetDB(context).HaveToken())
        {
            login.setText("");
            roleRus.setText("");
            roleEng.setText("");
        }
        else
        {
            GetLogin(login, context);
            GetRole(roleRus, roleEng, login, context);

        }
    }


    public static void GetURL(TextView url, Activity context)
    {
        url.setText(Helper.GetUrlAddress(context));
    }


    public static void GetLogin(TextView login, Activity context)
    {

        TextView textViewLogin = login;
        String token = ConnectConfig.GetToken(context);
        if(token.length() > 0)
        {
            LoginAPI loginAPI = new LoginAPI(context)
            {
                @Override
                public void on_fail(String req) {
                    login.setText("");
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


        String token = ConnectConfig.GetToken(context);
        if(token.length() > 0) {
            RoleApi roleAPI = new RoleApi(context) {
                @Override
                public void on_fail(String req) {

                    roleEng.setText("");
                    roleRus.setText("");
                    DB.GetDB(context).TokenClear();
                    GetLogin(login, context);
                }

                @Override
                public void GetRole(String nameRus, String nameEng, Role role) {
                    roleRus.setText(nameRus);
                    roleEng.setText(nameEng);
                    Role = role;
                }
            };
            roleAPI.Get(Helper.GetUrlAddress(context) + "/users/get-role");
        }
    }

    public static Role Role;

    public static boolean RoleIsAdmin(Context context)
    {
        if(!DB.GetDB(context).HaveToken())
        {
            return false;
        }
        try
        {
            String role = Role.NameEng;
            return role.equals("admin") || role.equals("Admin");
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public static boolean RoleIsClient(Context context)
    {
        if(!DB.GetDB(context).HaveToken())
        {
            return false;
        }
        try
        {
            String role = Role.NameEng;
            return role.equals("client") || role.equals("Client");
        }
        catch (Exception e)
        {
            return true;
        }
    }

}
