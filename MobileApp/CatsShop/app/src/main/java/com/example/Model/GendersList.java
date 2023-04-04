package com.example.Model;

import org.json.JSONArray;
import org.json.JSONException;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

public class GendersList extends ArrayList<Gender> {

    public GendersList()
    {

    }

    public GendersList(List<Gender> genders)
    {
        this();
        addAll(genders);
    }

    public GendersList(String json)
    {
        this();
        SetJson(json);
    }

    public void SetJson(String json)
    {
        try {
            JSONArray array = new JSONArray(json);
            for(int i = 0; i < array.length(); i++)
            {
                add(new Gender(array.getJSONObject(i)));
            }
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    public int indexOf(String genderName)
    {

        for(int i =0; i < size(); i++)
        {
            if(get(i).Name.equals(genderName))
                return i;
        }
        return -1;
    }

    public int indexOf(int genderID)
    {
        for(int i =0; i < size(); i++)
        {
            if(get(i).ID == genderID)
                return i;
        }
        return -1;
    }

    public Gender get(int id, boolean isGenderID)
    {
        if(!isGenderID)
            return get(id);
        else
            return  get(indexOf(id));
    }

    public Gender get(String genderName)
    {
        return get(indexOf(genderName));
    }

}

