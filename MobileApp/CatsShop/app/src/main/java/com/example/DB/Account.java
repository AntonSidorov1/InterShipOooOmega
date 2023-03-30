package com.example.DB;

public class Account {

    public String login = "";
    public String password = "";

    public int ID = 0;

    @Override
    public String toString() {
        return ID + ": " + login;
    }

    public Account Copy()
    {
        Account account = new Account();
        account.login = login;
        account.ID = ID;
        account.password = password;
        return account;
    }
}
