package com.example.catsshop;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.os.Bundle;
import android.widget.TextView;

import com.example.DB.Helper;

public class MainActivity extends AppCompatActivity {

    TextView url;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        url = findViewById(R.id.textViewURL);

        GetURL();
    }

    public Context GetContext()
    {
        return this;
    }

    public void GetURL()
    {
        url.setText(Helper.GetUrlAddress(GetContext()));
    }
}