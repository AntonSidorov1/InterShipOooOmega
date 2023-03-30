package com.example.API;


import com.example.DB.Helper;

public class Session
{
    public void GetSession(String session)
    {

    }

    public final void SetSession(String session) {
        Helper.Session = session;
        TrySession(session);
        GetSession(session);

    }

    public void TrySession(String session)
    {

    }

    public void ErrorSession()
    {

    }

    public final void SetErrorSession()
    {
        Helper.Session = "";
           ErrorSession();
           GetSession(Helper.Session);
    }
}
