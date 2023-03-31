package com.example.catsshop;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.TextView;

import com.example.DB.Helper;

public class SignInActivity extends AppCompatActivity {


    TextView url, doing;

    CheckBox showPassword, saveAccount, signInWithAccount;

    Button signIn;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sign_in);

        Intent i = getIntent();

        url = findViewById(R.id.textViewSignInURL);
        doing = findViewById(R.id.textViewSignInDoing);
        signIn = findViewById(R.id.buttonSignIn);

        showPassword = findViewById(R.id.checkBoxShowPassword);
        saveAccount = findViewById(R.id.checkBoxSaveAccount);
        signInWithAccount = findViewById(R.id.checkBoxSignInYes);
        signInWithAccount.setVisibility(View.INVISIBLE);

        doing.setText(i.getStringExtra("doingText"));
        SetButtonText(i.getStringExtra("buttonSignIn"));
        GetDatas();
    }

    public void SignIn_Click(View v)
    {

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