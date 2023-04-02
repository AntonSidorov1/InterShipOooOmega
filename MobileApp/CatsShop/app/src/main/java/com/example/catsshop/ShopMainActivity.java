package com.example.catsshop;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;

import com.example.Configuration.ChangeEvent;
import com.example.Configuration.FormatClass;
import com.example.Model.Cat;
import com.example.Users.Role;
import com.example.Users.UsersHelper;

import java.util.ArrayList;

public class ShopMainActivity extends AppCompatActivity {

    Button addCat;
    ListView listCats;

    ArrayList<Cat> cats = new ArrayList<>();
    ArrayAdapter<Cat> catsAdapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_shop_main);
        addCat = findViewById(R.id.buttonAddCat);
        listCats = findViewById(R.id.listCats);

        GetDatas();
    }

    public Activity GetContext()
    {
        return this;
    }

    void GetDatas()
    {

        ChangeEvent event = new ChangeEvent()
        {
            @Override
            public void Run(Role role) {

                int visible = FormatClass.GetVisibleByBool(UsersHelper.RoleIsAdmin(GetContext()));
                addCat.setVisibility(visible);
            }
        };
        UsersHelper.GetDatas(this, event);
    }

    @Override
    public void startActivityForResult(@NonNull Intent intent, int requestCode) {
        GetDatas();
        super.startActivityForResult(intent, requestCode);
    }

    public void RunSettings(View v)
    {
        Intent i = new Intent(this, MainActivity.class);
        startActivityForResult(i, 200);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
        GetDatas();
        super.onActivityResult(requestCode, resultCode, data);
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

}