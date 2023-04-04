package com.example.catsshop;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

import android.app.Activity;
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

import com.example.API.ApiClientWithMessage;
import com.example.API.ConnectConfig;
import com.example.API.ResultOfAPI;
import com.example.Configuration.ChangeEvent;
import com.example.Configuration.FormatClass;
import com.example.DB.DB;
import com.example.DB.Helper;
import com.example.Users.LoginAPI;
import com.example.Users.Role;
import com.example.Users.RoleApi;
import com.example.Users.UsersHelper;

public class MainActivity extends AppCompatActivity {

    TextView url, login, roleRus, roleEng;

    Button signIn, signOut, registarte, urlEdit, changeProfile, changePassword, dropAccount;

    boolean run = true, run1 = true;

    boolean visibleButton = false;

    @Override
    public void finish() {
        run = false;
        super.finish();
    }

    public void Exit(View v)
    {
        finish();
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        url = findViewById(R.id.textViewURL);

        login = findViewById(R.id.textViewLogin);
        login.setText("");
        roleRus = findViewById(R.id.textViewRoleRus);
        roleRus.setText("");
        roleEng = findViewById(R.id.textViewRoleEng);
        roleEng.setText("");

        signIn = findViewById(R.id.buttonLogIn);
        signOut = findViewById(R.id.buttonLogOut);
        registarte = findViewById(R.id.buttonRegistrate);
        urlEdit = findViewById(R.id.buttonUrlEdit);
        changeProfile = findViewById(R.id.buttonChangeProfile);
        changePassword = findViewById(R.id.buttonCahngePassword);
        dropAccount = findViewById(R.id.buttonDropAccount);

        GetDatas();
        //RunTokenUpdate();
    }

    public Activity GetContext()
    {
        return this;
    }

    public void GetDatas()
    {
        run1 = true;

        ChangeEvent event = new ChangeEvent()
        {
            @Override
            public void Run() {
                boolean visibleButton = DB.GetDB(GetContext()).HaveToken();
                int visible = FormatClass.GetVisibleByBool(visibleButton);
                int noVisible = FormatClass.GetNoVisibleByBool(visibleButton);
                signIn.setVisibility(noVisible);
                registarte.setVisibility(noVisible);
                urlEdit.setVisibility(noVisible);
                signOut.setVisibility(visible);
                changeProfile.setVisibility(visible);
                dropAccount.setVisibility(visible);
                changePassword.setVisibility(visible);
            }
        };

        UsersHelper.GetDatas(url, login, roleRus, roleEng, this, event);
        event.Run();
    }

    @Override
    public void startActivityForResult(@NonNull Intent intent, int requestCode) {
        GetDatas();
        super.startActivityForResult(intent, requestCode);
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

    public void ChangeProfile_Click(View v)
    {
        SignOut_Click(v, true);
    }

    public void SignOut_Click(View v)
    {
        SignOut_Click(v, false);
    }
    
    public void ChangeProfile(View v)
    {

        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle("Смена аккаунта");

        builder.setNegativeButton("Зарегистрироваться", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                DB.GetDB(GetContext()).TokenClear();
                GetDatas();
                Registrate_Click(v);
            }
        });

        builder.setPositiveButton("Войти", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                GetDatas();
                SignIn_Click(v);
            }

        });

        builder.setNeutralButton("Отмена", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                GetDatas();
            }

        });

        AlertDialog dialog = builder.create();
        dialog.setCancelable(false);
        dialog.setMessage("Каким образом вы хотите сменить аккаунт?");
        dialog.show();
    }

    public void SignOut_Click(View v, boolean change)
    {
        if(!DB.GetDB(this).HaveToken())
        {
            GetDatas();
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.setTitle("Выход из аккаунта (Ошибка!!!)");

            AlertDialog dialog = builder.create();
            dialog.setMessage("Вы не авторизированы в системе");
            dialog.show();
            Toast.makeText(this, "Вы не авторизированы в системе", Toast.LENGTH_SHORT).show();
            if(change)
            {
                ChangeProfile(v);
            }
            return;
        }

        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle("Выход из аккаунта");
        builder.setCancelable(false);

        builder.setPositiveButton("Да", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                DB.GetDB(GetContext()).TokenClear();
                GetDatas();
                if(change)
                {
                    ChangeProfile(v);
                }
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

    public void ChangePassword_Click(View v)
    {
        Intent i = new Intent(this, SignInActivity.class);
        i.putExtra("Doing", "password");
        i.putExtra("doingText", "Смена пароля");
        i.putExtra("buttonSignIn", "Сменить пароль");
        startActivityForResult(i, 200);
    }

    public void DropAccount_Click(View v)
    {
        if(!DB.GetDB(this).HaveToken())
        {
            GetDatas();
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.setTitle("Удаление аккаунта (Ошибка!!!)");

            AlertDialog dialog = builder.create();
            dialog.setMessage("Вы не авторизированы в системе");
            dialog.show();
            Toast.makeText(this, "Вы не авторизированы в системе", Toast.LENGTH_SHORT).show();
            return;
        }


        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle("Удаление аккаунта");
        builder.setCancelable(false);

        builder.setPositiveButton("Да", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                ApiClientWithMessage api = new ApiClientWithMessage(GetContext())
                {
                    @Override
                    public void GetResult(ResultOfAPI res) {
                        DB.GetDB(GetContext()).TokenClear();
                        DB.GetDB(GetContext()).ClearAccount();
                        GetDatas();
                    }
                };
                api.TitleMessage = "Удаление аккаунта";
                api.MessageReady = "Аккаунт успешно удалён";
                api.MessageFail = "Не удалось удалить аккаунт";
                api.DELETE(Helper.GetURL(GetContext()).GetURL() + "/users", true);
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
        dialog.setMessage("Вы действительно хотите удалить аккаунт?");
        dialog.show();
    }

    public void ProgramInfo_Click(View v)
    {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle("О приложении");
        builder.setCancelable(false);

        builder.setPositiveButton("ОК", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                GetDatas();
            }
        });

        AlertDialog dialog = builder.create();
        dialog.setCancelable(false);
        dialog.setMessage("Название приложения - CatsShop \n" +
                "Назначение - Магазин котиков \n" +
                "Автор - Сидоров Антон Дмитриевич \n" +
                "URL-адрес сервера - " + Helper.GetUrlAddress(GetContext()));
        dialog.show();
    }
}