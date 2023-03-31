package com.example.API;

import com.example.URL.URL;

public class ApiParameters {

    public String Method = "GET";
    public URL URL = new URL();

    public void SetURL(String address)
    {
        URL = new URL(address);
    }

    public String GetURL()
    {
        return URL.GetURL();
    }

    public String Body = "";

    public Boolean Authorization = false;
}
