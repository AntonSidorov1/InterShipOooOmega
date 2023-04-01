package com.example.catsshop;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.example.API.ConnectConfig;
import com.example.DB.DB;
import com.example.DB.Helper;
import com.example.Users.LoginAPI;
import com.example.Users.RoleApi;
import com.example.Users.UsersHelper;

public class MainActivity extends AppCompatActivity {

    TextView url, login, roleRus, roleEng;

    Button signIn, signOut, registarte;

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
        UsersHelper.GetDatas(url, login, roleRus, roleEng, this);
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

    public void SignOut_Click(View v)
    {
        if(!DB.GetDB(this).HaveToken())
        {

            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.setTitle("Выход из аккаунта (Ошибка!!!)");

            AlertDialog dialog = builder.create();
            dialog.setMessage("Вы не авторизированы в системе");
            dialog.show();
            Toast.makeText(this, "Вы не авторизированы в системе", Toast.LENGTH_SHORT).show();
            return;
        }

        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle("Выход из аккаунта");

        builder.setPositiveButton("Да", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                DB.GetDB(GetContext()).TokenClear();
                GetDatas();
            }
        });

        builder.setNegativeButton("Нет", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                GetDatas();
            }
        });

        AlertDialog dialog = builder.create();
        dialog.setCancelable(false);
        dialog.setMessage("Вы действительно хотите выйти из аккаунта?");
        dialog.show();

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