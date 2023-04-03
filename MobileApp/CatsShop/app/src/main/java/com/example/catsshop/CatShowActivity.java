package com.example.catsshop;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.example.API.ApiClient;
import com.example.API.ResultOfAPI;
import com.example.Configuration.ChangeEvent;
import com.example.Configuration.FormatClass;
import com.example.DB.Helper;
import com.example.Model.Cat;
import com.example.Users.Role;
import com.example.Users.UsersHelper;

import org.json.JSONArray;
import org.json.JSONObject;

public class CatShowActivity extends AppCompatActivity {


    boolean run = true, run1 = true;
    boolean runAccount = true, runCat = true;
    int idCat;
    Cat cat;

    Button deleteCat, updateCat, buyCat;

    TextView color, species, gender, age, dateAdded, dateUpdated, price;

    @Override
    public void finish() {
        run = false;
        run1 = false;
        super.finish();
    }

    @Override
    public void startActivityForResult(@NonNull Intent intent, int requestCode) {
        run1 = false;
        super.startActivityForResult(intent, requestCode);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
        run1 = true;
        super.onActivityResult(requestCode, resultCode, data);

        GetDatas();
    }

    public void Exit_Click(View v)
    {
        finish();
    }

    void ListChange()
    {
        color.setText(String.valueOf(cat.Color));
        species.setText(String.valueOf(cat.Species));
        gender.setText(String.valueOf(cat.Gender));
        age.setText(String.valueOf(cat.Age));
        price.setText(String.valueOf(cat.GetPrice()));
        dateAdded.setText(String.valueOf(cat.DateAdded));
        dateUpdated.setText(String.valueOf(cat.DateUpdated));
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_cat_show);
        idCat = getIntent().getIntExtra("IdCat", 0);

        deleteCat = findViewById(R.id.buttonDeleteCat);
        updateCat = findViewById(R.id.buttonUpdateCat);
        buyCat = findViewById(R.id.buttonBuyCat);

        color = findViewById(R.id.textViewColor);
        color.setText("");
        species = findViewById(R.id.textViewSpecies);
        species.setText("");
        gender = findViewById(R.id.textViewGender);
        gender.setText("");
        age = findViewById(R.id.textViewAge);
        age.setText("");
        price = findViewById(R.id.textViewPrice);
        price.setText("");
        dateAdded = findViewById(R.id.textViewDateAdded);
        dateAdded.setText("");
        dateUpdated = findViewById(R.id.textViewDateUpdated);
        dateUpdated.setText("");

        GetDatas();
        RunGetCatsFromApi();
    }

    public Activity GetContext()
    {
        return this;
    }

    void GetDatas()
    {
        runAccount = false;
        ChangeEvent event = new ChangeEvent()
        {
            @Override
            public void Run(Role role) {

                int visible = FormatClass.GetVisibleByBool(UsersHelper.RoleIsAdmin(GetContext()));
                int inVisible = FormatClass.GetVisibleByBool(UsersHelper.RoleIsClient(GetContext()));
                try {
                    deleteCat.setVisibility(visible);
                }
                catch (Exception e)
                {

                }
                try {
                    updateCat.setVisibility(visible);
                }
                catch (Exception e)
                {

                }
                try {
                    buyCat.setVisibility(inVisible);
                }
                catch (Exception e)
                {

                }
            }
        };
        UsersHelper.GetDatas(this, event);
        runAccount = true;
    }


    public void GetCat(String res) {
        runCat = false;

        try {
            JSONObject object = new JSONObject(res);
            cat = new Cat();
            cat.ID = object.getInt("id");
            cat.Age = object.getInt("age");
            cat.Price = object.getDouble("price");
            cat.Color = object.getString("color");
            cat.Species = object.getString("species");
            cat.Gender = object.getString("gender");
            cat.DateAdded = object.getString("dateAdded");
            cat.DateUpdated = object.getString("dateUpdated");
            cat.DatesChangeFormat();
        } catch (Exception e) {
            e.printStackTrace();
        }

        ListChange();
        runCat = true;
    }


    public void GetCatFromAPI()
    {
        while(run)
        {
            if(run1)
            {
                if(runAccount) {
                    GetContext().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            GetDatas();
                        }
                    });
                }
                if(runCat) {
                    GetContext().runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            ApiClient api = new ApiClient(GetContext()) {
                                @Override
                                public void ready_result(ResultOfAPI res) throws Exception {
                                    if (res.Code != 200)
                                        throw new Exception();
                                    GetCat(res.Body);
                                }

                                @Override
                                public void on_fail(ResultOfAPI res, String message) {
                                    on_fail(message);
                                }

                                @Override
                                public void on_fail(String req) {
                                    finish();
                                }
                            };
                            int[] cats = new int[]{idCat};
                            api.GET(Helper.GetURL(GetContext()).GetURL() + "/cats/" + cats[0], false);
                        }
                    });
                }

                try {
                    Thread.sleep(2000);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }

            }
        }
    }

    public void RunGetCatsFromApi()
    {
        Runnable run = new Runnable() {
            @Override
            public void run() {
                GetCatFromAPI();
            }
        };
        Thread thread = new Thread(run);
        thread.start();
    }

}