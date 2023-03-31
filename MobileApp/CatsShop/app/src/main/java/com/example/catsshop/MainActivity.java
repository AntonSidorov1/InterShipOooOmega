package com.example.catsshop;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.TextView;

import com.example.API.ConnectConfig;
import com.example.DB.DB;
import com.example.DB.Helper;
import com.example.Users.LoginAPI;

public class MainActivity extends AppCompatActivity {

    TextView url, login, roleRus, roleEng;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        url = findViewById(R.id.textViewURL);

        login = findViewById(R.id.textViewLogin);
        roleRus = findViewById(R.id.textViewRoleRus);
        roleEng = findViewById(R.id.textViewRoleEng);

        GetDatas();
    }

    public Context GetContext()
    {
        return this;
    }

    public void GetDatas()
    {
        GetURL();
        GetLogin();
    }

    public void GetURL()
    {
        url.setText(Helper.GetUrlAddress(GetContext()));
    }

    public void GetLogin()
    {
        login.setText("");
        TextView textViewLogin = login;
        String token = ConnectConfig.GetToken(this);
        if(token.length() > 0)
        {
            LoginAPI loginAPI = new LoginAPI(this)
            {
                @Override
                public void on_fail(String req) {
                    DB.GetDB(GetContext()).TokenClear();
                }

                @Override
                public void GetLogin(String login) {
                    textViewLogin.setText(login);
                }
            };
            loginAPI.send(Helper.GetUrlAddress(this) + "/users/get-login");
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.main_manu, menu);
        return super.onCreateOptionsMenu(menu);
    }

    @Override
    public boolean onOptionsItemSelected(@NonNull MenuItem item) {
        if(item.getItemId() == R.id.exitItem)
        {
            finish();
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {

        GetDatas();
        super.onActivityResult(requestCode, resultCode, data);
    }

    public void UrlEdit_Click(View v)
    {
        Intent i = new Intent(this, UrlEditActivity.class);
        startActivityForResult(i, 200);
    }


    public void SignIn_Click(View v)
    {
        Intent i = new Intent(this, SignInActivity.class);
        i.putExtra("Doing", "input");
        i.putExtra("doingText", "Авторизация");
        i.putExtra("buttonSignIn", "Войти");
        startActivityForResult(i, 200);
    }


    public void Registrate_Click(View v)
    {
        Intent i = new Intent(this, SignInActivity.class);
        i.putExtra("Doing", "client");
        i.putExtra("doingText", "Регистрация");
        i.putExtra("buttonSignIn", "Зарегистрироваться");
        startActivityForResult(i, 200);
    }
}