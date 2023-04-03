package com.example.Configuration;

import android.view.View;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

public class FormatClass {

    public static int GetVisibleByBool(boolean visible)
    {
        if(visible)
            return View.VISIBLE;
        else
            return View.INVISIBLE;
    }

    public static int GetNoVisibleByBool(boolean noVisible)
    {
        return GetVisibleByBool(!noVisible);
    }


    public static String FormatDate(String date) {
        date = date.replace("T", " ");
        date = date.replace("Т", " ");
        date = date.replace('.', '_');
        date = date.split("_")[0];

        SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        try {
            Date time = format.parse(date);
            date = new SimpleDateFormat("dd-MM-yyyy HH:mm").format(time);
        } catch (ParseException e) {
            e.printStackTrace();
        }

        try
        {
            return date.replace("-", ".");
        }
        catch(Exception e)
        {
            return date;
        }
    }

}
