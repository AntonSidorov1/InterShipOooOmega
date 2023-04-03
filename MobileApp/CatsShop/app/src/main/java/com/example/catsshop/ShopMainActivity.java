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
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.example.API.ApiClient;
import com.example.API.ResultOfAPI;
import com.example.Configuration.ChangeEvent;
import com.example.Configuration.FormatClass;
import com.example.DB.DB;
import com.example.DB.Helper;
import com.example.Model.Cat;
import com.example.Users.Role;
import com.example.Users.UsersHelper;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

public class ShopMainActivity extends AppCompatActivity {

    Button addCat;
    ListView listCats;

    boolean run = true, run1 = true;
    boolean runAccount = true, runCat = true;

    @Override
    public void finish() {
        run = false;
        run1 = false;
        super.finish();
    }

    ArrayList<Cat> cats = new ArrayList<>();
    ArrayAdapter<Cat> catsAdapter;

    void ListChange()
    {
        catsAdapter.clear();
        catsAdapter.addAll(cats);
        catsAdapter.notifyDataSetChanged();
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_shop_main);
        addCat = findViewById(R.id.buttonAddCat);
        listCats = findViewById(R.id.listCats);

        catsAdapter = new ArrayAdapter<>(this, android.R.layout.simple_list_item_1);
        listCats.setAdapter(catsAdapter);

        GetDatas();
        RunGetCatsFromApi();

        listCats.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                int idCat = catsAdapter.getItem(position).ID;
                Intent i = new Intent(GetContext(), CatShowActivity.class);
                i.putExtra("IdCat", idCat);
                startActivityForResult(i, 200);
            }
        });
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
                addCat.setVisibility(visible);
            }
        };
        UsersHelper.GetDatas(this, event);
        runAccount = true;
    }

    public void GetGats(String res)
    {
        runCat = false;

        try {
            cats.clear();
            JSONArray json = new JSONArray(res);
            for(int i = 0; i < json.length(); i++)
            {
                JSONObject object = json.getJSONObject(i);
                Cat cat = new Cat();
                cat.ID = object.getInt("id");
                cat.Age = object.getInt("age");
                cat.Price = object.getDouble("price");
                cat.Color = object.getString("color");
                cat.Species = object.getString("species");
                cat.Gender = object.getString("gender");
                cat.DateAdded = object.getString("dateAdded");
                cat.DateUpdated = object.getString("dateUpdated");
                cats.add(cat);

            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        ListChange();
        runCat = true;
    }


    public void GetCatsFromAPI()
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
                                    GetGats(res.Body);
                                }

                                @Override
                                public void on_fail(ResultOfAPI res, String message) {
                                    on_fail(message);
                                }

                                @Override
                                public void on_fail(String req) {
                                    cats.clear();

                                    Toast.makeText(GetContext(),
                                            ErrorMessage(),
                                            Toast.LENGTH_SHORT);

                                    ListChange();
                                }
                            };
                            api.GET(Helper.GetURL(GetContext()).GetURL() + "/cats", false);
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
                GetCatsFromAPI();
            }
        };
        Thread thread = new Thread(run);
        thread.start();
    }

    @Override
    public void startActivityForResult(@NonNull Intent intent, int requestCode) {
        run1 = false;
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
        run1 = true;
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