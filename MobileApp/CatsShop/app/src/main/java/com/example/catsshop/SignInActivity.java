package com.example.catsshop;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
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

import com.example.Authofication.SignIn;
import com.example.DB.Helper;

public class SignInActivity extends AppCompatActivity {


    TextView url, doing;
    CheckBox showPassword, saveAccount, signInWithAccount;
    Button signIn;
    String doingText;
    EditText login, password;

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
        }

        GetDatas();
    }

    public void SignIn_Click(View v)
    {
        SignIn signIn = new SignIn(this)
        {
            @Override
            public void EndSend() {
                finish();
            }
        };

        String login = this.login.getText().toString();
        String password = this.password.getText().toString();

        String address = Helper.URL.GetURL() + "/autotification";
        signIn.send(address, login, password);

    }

    public void Registrate_Click(View v)
    {

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