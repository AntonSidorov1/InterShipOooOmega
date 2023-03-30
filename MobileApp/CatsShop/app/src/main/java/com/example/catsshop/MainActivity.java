package com.example.catsshop;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
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

        return super.onContextItemSelected(item);
    }

}