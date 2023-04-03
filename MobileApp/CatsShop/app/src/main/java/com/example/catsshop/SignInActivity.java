package com.example.catsshop;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.text.method.HideReturnsTransformationMethod;
import android.text.method.PasswordTransformationMethod;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.example.API.ApiClient;
import com.example.API.ApiClientWithMessage;
import com.example.API.ResultOfAPI;
import com.example.Authofication.SignIn;
import com.example.DB.DB;
import com.example.DB.Helper;

public class SignInActivity extends AppCompatActivity {


    TextView url, doing;
    CheckBox showPassword, saveAccount, signInWithAccount;
    Button signIn;
    String doingText;
    EditText login, password;
    TextView loginText, passwordText;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sign_in);

        Intent i = getIntent();

        url = findViewById(R.id.textViewSignInURL);
        doing = findViewById(R.id.textViewSignInDoing);
        signIn = findViewById(R.id.buttonSignIn);

        doingText = i.getStringExtra("Doing");
        showPassword = findViewById(R.id.checkBoxShowPassword);
        CheckBox show = showPassword;
        saveAccount = findViewById(R.id.checkBoxSaveAccount);
        signInWithAccount = findViewById(R.id.checkBoxSignInYes);

        login = findViewById(R.id.editTextLoginInput);
        password = findViewById(R.id.editTextPasswordInput);
        loginText = findViewById(R.id.textViewLoginInputText);
        passwordText = findViewById(R.id.textViewPasswordInputText);

        login.setText("");
        password.setText("");

        password.setTransformationMethod(PasswordTransformationMethod.getInstance());

        show.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean isChecked) {
                if(!isChecked)
                    password.setTransformationMethod(PasswordTransformationMethod.getInstance());
                else
                    password.setTransformationMethod(HideReturnsTransformationMethod.getInstance());
            }
        });

        if(!doingText.equals("input"))
        {
            saveAccount.setVisibility(View.INVISIBLE);
        }

        if(!doingText.equals("client")) {
            signInWithAccount.setVisibility(View.INVISIBLE);
        }
        else
        {
            signInWithAccount.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
                @Override
                public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                    if(!isChecked)
                    {
                        saveAccount.setChecked(false);
                        saveAccount.setVisibility(View.INVISIBLE);
                    }
                    else
                    {
                        saveAccount.setChecked(false);
                        saveAccount.setVisibility(View.VISIBLE);
                    }
                }
            });
        }

        doing.setText(i.getStringExtra("doingText"));
        SetButtonText(i.getStringExtra("buttonSignIn"));

        if(doingText.equals("input"))
        {
            signIn.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    SignIn_Click(v);
                }
            });

            saveAccount.setChecked(DB.GetDB(this).HaveAccount());
            boolean save = saveAccount.isChecked();
            if(save)
            {
                DB.GetDB(this).GetAccount();
                login.setText(Helper.Account.login);
                password.setText(Helper.Account.password);
            }
        }
        else {
            if (doingText.equals("client")) {
                signIn.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        Registrate_Click(v);
                    }
                });
            }
            else if(doingText.equals("admin"))
            {
                signIn.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        AddAdmin_Click(v);
                    }
                });
            }
            else if(doingText.equals("password"))
            {
                login.setVisibility(View.INVISIBLE);
                loginText.setText(i.getStringExtra("login"));
                passwordText.setText("Новый пароль");

                signIn.setOnClickListener(new View.OnClickListener() {

                    @Override
                    public void onClick(View v) {
                        ChangePassword_Click(v);
                    }
                });
            }
        }

        GetDatas();
    }

    public void ChangePassword_Click(View v)
    {
        ApiClientWithMessage api = new ApiClientWithMessage(this)
        {
            @Override
            public void GetResultReady(ResultOfAPI res) {
                finish();
            }
        };
        api.TitleMessage = "Смена пароля";
        api.MessageReady = "Пароль успешно сменен";
        api.MessageFail = "Не удалось зарегистрироваться \n" +
                "   - Возможно вы уже неавторизированы в системе \n" +
                "   - Возможно пароль совпадает с названием одной из ролей \n";
        api.PATCH(Helper.GetURL(this).GetURL()+"/users/change-password", "\""+password.getText().toString()+ "\"", true);

    }

    public void SignIn_Click(View v)
    {
        SignIn signIn = new SignIn(this)
        {
            @Override
            public void EndSend() {
                AfterSignIn();
                finish();
            }
        };

        String login = this.login.getText().toString();
        String password = this.password.getText().toString();

        String address = Helper.URL.GetURL() + "/autotification/sign-in";
        signIn.send(address, login, password);

    }

    void AfterSignIn()
    {
        DB.GetDB(this).ClearAccount();
        boolean save = saveAccount.isChecked();
        if(save)
        {
            String login = this.login.getText().toString();
            String password = this.password.getText().toString();
            DB.GetDB(this).SaveAccount(login, password);
        }
    }

    public void Registrate_Click(View v)
    {
        ApiClientWithMessage api = new ApiClientWithMessage(this)
        {
            @Override
            public void GetResultReady(ResultOfAPI res) {
                AfterReadyRegistrate(v);
            }
        };
        api.TitleMessage = "Регистрация в системе";
        api.MessageReady = "Вы успешно зарегистрировались";
        api.MessageFail = "Не удалось зарегистрироваться \n" +
                "   - Возможно логин уже существует в системе \n" +
                "   - Возможно пароль совпадает с названием одной из ролей \n" +
                "   - Возможно логин пустой (должен быть, хотя бы один символ)";
        api.POST(Helper.URL.GetURL() + "/users/registrate",
                "{" +
                        "\"login\": \"" + login.getText().toString() + "\"," +
                        "\"password\": \"" + password.getText().toString() + "\"" +
                        "}",
                false);
    }

    void AfterReadyRegistrate(View v)
    {
        Boolean signIn = signInWithAccount.isChecked();
        if(!signIn)
        {
            finish();
        }
        else
        {
            SignIn_Click(v);
            return;
        }
    }

    public void AddAdmin_Click(View v)
    {

    }

    void SetButtonText(String text)
    {
        signIn.setText(text);
    }



    public Context GetContext()
    {
        return this;
    }

    public void GetDatas()
    {
        GetURL();
    }

    public void GetURL()
    {
        url.setText(Helper.GetUrlAddress(GetContext()));
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
            exit();
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    void exit()
    {
        finish();
    }

    public void Exit_Click(View v)
    {
        exit();
    }

}