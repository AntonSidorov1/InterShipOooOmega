package com.example.DB;

import java.util.ArrayList;

public class AccountList extends ArrayList<Account> {


    public Account GetFromID(int id)
    {
        for(int i =0; i < size(); i++)
        {
            if(get(i).ID == id)
                return get(i);
        }
        return new Account();
    }
}
