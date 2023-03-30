package com.example.URL;

public class URL {
    public String Protocol = "https";

    public String Path = "192.168.0.15:7073/api";

    public String GetURL()
    {
        return Protocol + "://" + Path;
    }

    public void SetURL(URL url)
    {
        SetURL(url.GetURL());
    }

    public void SetURL(String url)
    {
        String[] parts = url.split("://");
        Protocol = parts[0];
        Path = parts[1];
    }

    public URL()
    {

    }

    public URL(String protocol, String path)
    {
        this();
        Protocol = protocol;
        Path = path;
    }

    public URL(URL url)
    {
        this();
        SetURL(url);
    }

    public URL(String url)
    {
        this();
        SetURL(url);
    }

    public URL Copy()
    {
        return new URL(this);
    }
}
