package com.example.DB;

import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import androidx.annotation.Nullable;

import com.example.API.ConnectConfig;
import com.example.URL.URL;

public class DB extends SQLiteOpenHelper {

    public DB(@Nullable Context context) {
        super(context, "storage.db", null, 1);
    }

    public static DB GetDB(Context context)
    {
        return new DB(context);
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        String sql = "Create table Account (login TEXT, password TEXT);";
        db.execSQL(sql);
        sql = "Create table Url (protocol TEXT, path TEXT);";
        db.execSQL(sql);

        sql = "Create table Token (tokenKey TEXT)";
        db.execSQL(sql);
    }

    public void TokenClear()
    {
        SQLiteDatabase db = getWritableDatabase();
        String sql = "Delete From Token";
        db.execSQL(sql);
    }

    public void PutToken(String token)
    {
        TokenClear();
        if(token.length() < 1)
            return;
        SQLiteDatabase db = getWritableDatabase();
        String sql = "Insert Into Token (tokenKey) Values ('"+token+"')";
        db.execSQL(sql);
    }
    
    public String GetTokenKey() throws Exception {
        String sql = "Select tokenKey From Token;";
        SQLiteDatabase db = getReadableDatabase();
        Cursor curr = db.rawQuery(sql,null);
        if(curr.moveToFirst() == true)
        {
            String token = curr.getString(0);
            curr.close();
            return token;
        }
        else
        {
            curr.close();
            throw new Exception();
        }
    }

    public String GetToken()
    {
        try {
            return GetTokenKey();
        } catch (Exception e) {
            e.printStackTrace();

            return "";
        }
    }

    public boolean HaveToken()
    {
        return GetToken().length() > 0;
    }

    public void GetTokenToConfig()
    {
        ConnectConfig.Token = GetToken();
    }


    @Override
    public void onUpgrade(SQLiteDatabase db, int i, int i1) {

    }


    public void ClearAccount()
    {
        try {
            String sql = "Delete From Account;";
            SQLiteDatabase db = getWritableDatabase();
            db.execSQL(sql);
        }
        catch(Exception e)
        {

        }
    }

    public void ClearUrl()
    {
        try {
            String sql = "Delete From Url;";
            SQLiteDatabase db = getWritableDatabase();
            db.execSQL(sql);
        }
        catch(Exception e)
        {

        }
    }


    public void SaveAccount(String login, String password)
    {
        ClearAccount();
        String sql = "Insert Into Account (login, password) Values ('"+login+"', '"+password+"');";
        SQLiteDatabase db = getWritableDatabase();
        db.execSQL(sql);
    }


    public void SaveUrl(URL url)
    {

        ClearUrl();
        String sql = "Insert Into Url (protocol, path) Values ('"+url.Protocol+"', '"+url.Path+"');";
        SQLiteDatabase db = getWritableDatabase();
        db.execSQL(sql);
    }

    public  void SaveAccount(Account account)
    {
        SaveAccount(account.login, account.password);
    }

    public void SaveAccount()
    {
        SaveAccount(Helper.Account);
    }

    public void SaveUrl()
    {
        SaveUrl(Helper.URL);
    }

    public Account ToAccount() throws Exception {
        Account account = new Account();
        String sql = "Select login, password From Account;";
        SQLiteDatabase db = getReadableDatabase();
        Cursor curr = db.rawQuery(sql,null);
        if(curr.moveToFirst() == true)
        {
            account.login = curr.getString(0);
            account.password = curr.getString(1);
            curr.close();
        }
        else {
            curr.close();
            throw new Exception();
        }


        return  account;
    }

    public boolean HaveAccount()
    {
        try {
            ToAccount();
            return true;
        } catch (Exception e) {
            e.printStackTrace();
            return false;
        }
    }

    public void GetAccount()
    {
        try
        {
            Helper.Account = ToAccount();
        }
        catch(Exception ex)
        {
           Helper.Account = new Account();

        }
    }


    public URL ToUrl() throws Exception {

        String sql = "Select protocol, path From Url;";
        SQLiteDatabase db = getReadableDatabase();
        Cursor curr = db.rawQuery(sql,null);
        if(curr.moveToFirst() == true)
        {
            URL url = new URL();
            url.Protocol = curr.getString(0);
            url.Path = curr.getString(1);
            curr.close();
            return url;
        }
        else {
            curr.close();
            throw new Exception();
        }

    }

    public void  GetUrl()

    {
        try {
            Helper.URL = ToUrl();
        }
        catch(Exception ex)
        {
            SaveUrl();
            GetUrl();

        }
    }
}
